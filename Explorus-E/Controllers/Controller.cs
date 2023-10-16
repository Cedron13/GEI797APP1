using ExplorusE.Controllers.States;
using ExplorusE.Models;
using ExplorusE.Observer;
using ExplorusE.Views;
using ExplorusE.Constants;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ExplorusE.Threads;
using System.Threading;
using ExplorusE.Models.Sprites;
using System.Linq;

/* EXPLORUS-E
 * Alexis BLATRIX (blaa1406)
 * Cédric CHARRON (chac0902)
 * Audric DAVID (dava1302)
 * Matthieu JEHANNE (jehm1701)
 * Cloé LEGLISE (legc1001)
 */

namespace ExplorusE.Controllers
{
    internal class Controller : IControllerView, IControllerModel, IResizeEventPublisher
    {
        private GameEngine engine;
        private GameView view;
        private GameModel model;
        private IState currentState;
        private List<IResizeEventSubscriber> resizeSubscribers;
        private double transitionTime = 0;
        private double transitionTimeBubble = 0;
        private double stopTime = 0;
        private bool isPaused = false;
        private bool waitLoadBubble = false;
        private bool isInvincible = false;
        private double invincibleTimer=0;

        private RenderThread oRenderThread;
        private PhysicsThread oPhysicsThread;
        private Thread renderThread;
        private Thread physicsThread; 
        private List<Wall> walls;

        public bool IsPaused
        {
            get { return isPaused; }
            set { isPaused = value; }
        }

        public bool GetWaitLoadBubble()
        {
            return waitLoadBubble;
        }

        public void SetIsInvincible(bool b)
        {
            isInvincible = b;
        }

        public void SetInvincibleTimer(double time)
        {
            invincibleTimer = time;
        }

        private List<Keys> inputList;

        public Controller()
        {
            oRenderThread = new RenderThread(); //TODO: Look for which object needs an access to oRenderThread
            model = new GameModel(this, oRenderThread);
            oPhysicsThread = new PhysicsThread("Collision Thread", model);
            inputList = new List<Keys>();
            resizeSubscribers = new List<IResizeEventSubscriber>();
            view = new GameView(this, oRenderThread);
            currentState = new PlayState(this);
            InitGame();

            renderThread = new Thread(new ThreadStart(oRenderThread.Run));
            renderThread.Name = "Render Thread";
            renderThread.Start();

            physicsThread = new Thread(new ThreadStart(oPhysicsThread.Run));
            physicsThread.Name = "Collision Thread";
            physicsThread.Start();

            engine = new GameEngine(this);
            //Order is very important due to dependencies between each object, this order works 👍
        }
        
        public void ViewCloseEvent()
        {
            engine.KillEngine(); //Works 👍
            view.Close();
            oRenderThread.Stop();
            oPhysicsThread.Stop();
            renderThread.Join();
            physicsThread.Join();

        }
        public void ModelCloseEvent()
        {
            view.Close();
        }

        public void ViewKeyPressedEvent(PreviewKeyDownEventArgs e)
        {
            //To avoid similar inputs between two frames
            if(!inputList.Contains(e.KeyCode)){
                if (!inputList.Contains(Keys.Down) && !inputList.Contains(Keys.Up) && !inputList.Contains(Keys.Right) && !inputList.Contains(Keys.Left) && !inputList.Contains(Keys.P) && !inputList.Contains(Keys.R)) //Checking if a directional input is already registred
                {
                    inputList.Add(e.KeyCode);
                }
            }
        }
        public void EngineRenderEvent()
        {
            view.Render();
        }

        public void EngineUpdateEvent(double lag)
        {
            model.Update(lag);
            view.SetLives(model.GetPlayerLives()); //Vie du joueur
            if (currentState is ResumeState)
            {
                transitionTime += lag;
                if (transitionTime > 3000)
                {
                    currentState.PrepareNextState();
                    currentState.GetNextState();
                }
            }
            else if (currentState is PlayState)
            {
                if (isInvincible)
                {
                    Console.WriteLine("je suis invincible");
                    invincibleTimer += lag;
                    if (invincibleTimer > 3000)
                    {
                        isInvincible = false;
                        Console.WriteLine("je suis plus invincible");
                        model.GetPlayer().SetTimeDone(true);
                        //AUTRE CHOSE ICI POUR PLAYER
                    }
                }
                if (currentState is PlayState && (waitLoadBubble == true))
                {
                    transitionTimeBubble += lag;
                    view.SetReloadTime(view.GetReloadTime() + lag);
                    if (transitionTimeBubble > 1200)
                    {
                        view.SetIsReloading(false);
                        waitLoadBubble = false;
                        Console.WriteLine("c'est okok");
                    }
                }
                    
            }
            else if (currentState is StopState)
            {
                stopTime += lag;
                if (stopTime > 3000)
                {
                    ModelCloseEvent();
                }
            }
        }

        public void AddSubscriber(IResizeEventSubscriber sub)
        {
            resizeSubscribers.Add(sub);
        }

        public void PositionUpdate()
        {
            oRenderThread.ResetPermanentItems(); //Permament item have their size changed
            foreach (IResizeEventSubscriber s in resizeSubscribers)
            {
                s.NotifyResize(view.GetTopMargin(), view.GetLeftMargin(), view.GetBrickSize());
            }

            foreach (Wall w in walls) oRenderThread.AskForNewItem(w, RenderItemType.Permanent); //Re-adding all walls in the render list
        }

        public void EngineProcessInputEvent()
        {

            currentState.ProcessInput(inputList);
            currentState = currentState.GetNextState();
            inputList.Clear(); //For next frame
        }

        public void InitGame()
        {
            int top = view.GetTopMargin();
            int left = view.GetLeftMargin();
            int brick = view.GetBrickSize();
            walls = new List<Wall>();
            for (int i = 0; i < model.GetLabyrinth().GetLength(0); i++)
            {
                for (int j = 0; j < model.GetLabyrinth().GetLength(1); j++)
                {
                    if (model.GetLabyrinth()[i, j] == 3)
                    {
                        model.SetGridPosX(j);
                        model.SetGridPosY(i);
                    }
                    else if (model.GetLabyrinth()[i, j] == 1)
                    {
                        Wall w = new Wall(new coord()
                        {
                            x = j,
                            y = i
                        }, top, left, brick);
                        walls.Add(w);
                        AddSubscriber(w);
                        oRenderThread.AskForNewItem(w, RenderItemType.Permanent);
                    }
                }
            }

            PlayerSprite player = new PlayerSprite(
                new coord()
                {
                    x = model.GetGridPosX(), //Place holder coordinates
                    y = model.GetGridPosY()
                }, top, left, brick);
            model.InitPlayer(player);
            AddSubscriber(model.GetPlayer());

            List<coord> toxicCoords = new List<coord>();
            coord toxic1 = new coord() {
                x = 1,
                y = 3
            };
            toxicCoords.Add(toxic1 );
            
            coord toxic2 = new coord()
            {
                x = 1,
                y = 11
            };
            toxicCoords.Add(toxic2);
            coord toxic3 = new coord()
            {
                x = 8,
                y = 1
            };
            toxicCoords.Add(toxic3);
            coord toxic4 = new coord()
            {
                x = 8,
                y = 5
            };
            toxicCoords.Add(toxic4);
            coord toxic5 = new coord()
            {
                x = 15,
                y = 3
            };
            toxicCoords.Add(toxic5);
            coord toxic6 = new coord()
            {
                x = 15,
                y = 11
            };
            toxicCoords.Add(toxic6);

            for(int i = 0;i<toxicCoords.Count; i++)
            {
                ToxicSprite tox = new ToxicSprite(toxicCoords.ElementAt(i), view.GetTopMargin(), view.GetLeftMargin(), view.GetBrickSize());
                AddSubscriber(tox);
                tox.StartMovement(toxicCoords.ElementAt(i), Direction.DOWN);
                model.InitToxicSlime(tox, "Toxic" + i);
            }
        }

        public void SetGemCounter(int i)
        {
            view.SetGemCounter(i);
        }
        

        public int[,] GetLabyrinth() => model.GetLabyrinth();

        public void ProcessMinimize()
        {
            isPaused = true;
            currentState.PrepareNextState(Constants.GameStates.PAUSE);
            currentState = currentState.GetNextState();
        }

        public void EndGameReached()
        {
            currentState.PrepareNextState(Constants.GameStates.STOP);
            currentState = currentState.GetNextState();
            
        }


        public void ProcessLostFocus()
        {
            isPaused = true;
            currentState.PrepareNextState(Constants.GameStates.PAUSE);
            currentState = currentState.GetNextState();
        }

        public void EndProcessLostFocus()
        {
            ExitPause();
            currentState.PrepareNextState();
            currentState = currentState.GetNextState();
        }

        public void ExitPause()
        {
            isPaused = false;
            transitionTime = 0;
        }


        public void WaitForNewBubble()
        {
            transitionTimeBubble = 0;
            waitLoadBubble = true;
            view.SetReloadTime(0);
            view.SetIsReloading(true);
            Console.WriteLine("boule envoyée"); // OK
        }

        public int NewLevel()
        {
            model.ClearCommandHistory();
            model.SetLabyrinth(GetLabyrinth());
            view.SetGemCounter(0);
            int currentLevel = view.GetLevelNumber();
            if(currentLevel == 3)
            {
                return 3;
            }
            view.SetLevelNumber(currentLevel+1);
            model.SetGridCoord(new coord()
            {
                x = view.GetInitPosX(), //Place holder coordinates
                y = view.GetInitPosY()
            });
            model.GoTo(Direction.DOWN, model.GetGridCoord());
            return currentLevel;
        }
        
        public List<BubbleSprite> GetBubbles() => model.GetBubbles();
        public Sprite GetPlayer() => model.GetPlayer();
        public GameModel GetGameModel() => model;
        public IState GetState() => currentState;
        public int GetTransitionTime() => (int)transitionTime;
       


    }
}
