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
using System.Drawing;
using System.Reflection;

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

        private RenderThread oRenderThread;
        private Thread renderThread;
        private List<Wall> walls;

        private Text statusBarText;
        private Text levelText;
        private NotInGridSprite titleSprite;
        private NotInGridSprite heartSprite;
        private NotInGridSprite bubbleSprite;
        private NotInGridSprite coinSprite;

        public bool IsPaused
        {
            get { return isPaused; }
            set { isPaused = value; }
        }

        public bool GetWaitLoadBubble()
        {
            return waitLoadBubble;
        }



        private List<Keys> inputList;

        public Controller()
        {
            oRenderThread = new RenderThread(); //TODO: Look for which object needs an access to oRenderThread
            model = new GameModel(this, oRenderThread);
            inputList = new List<Keys>();
            resizeSubscribers = new List<IResizeEventSubscriber>();
            view = new GameView(this, oRenderThread);
            currentState = new PlayState(this);
            InitGame();

            renderThread = new Thread(new ThreadStart(oRenderThread.Run));
            renderThread.Name = "Render Thread";
            renderThread.Start();

            engine = new GameEngine(this);
            //Order is very important due to dependencies between each object, this order works 👍

            InitRenderObjects();
        }
        
        public void ViewCloseEvent()
        {
            engine.KillEngine(); //Works 👍
            view.Close();
            oRenderThread.Stop();
            renderThread.Join();

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
            if (currentState is ResumeState)
            {
                transitionTime += lag;
                if (transitionTime > 3000)
                {
                    currentState.PrepareNextState();
                    currentState.GetNextState();
                }

                statusBarText.TextToDisplay = Constants.Constants.RESUME_TEXT + " (" + ((int)(4000 - transitionTime) / 1000).ToString() + ")";

            }
            else if (currentState is PlayState)
            {
                if (waitLoadBubble == true)
                {
                    transitionTimeBubble += lag;
                    view.SetReloadTime(view.GetReloadTime()+lag);  
                    if (transitionTimeBubble > 1200)
                    {
                        view.SetIsReloading(false);
                        waitLoadBubble = false;
                        Console.WriteLine("c'est okok");
                    }
                }
                statusBarText.TextToDisplay = Constants.Constants.PLAY_TEXT;
            }
            else if (currentState is StopState)
            {
                stopTime += lag;
                if (stopTime > 3000)
                {
                    ModelCloseEvent();
                }
                statusBarText.TextToDisplay = Constants.Constants.VICTORY_TEXT;
            }
            else if(currentState is PausedState)
            {
                statusBarText.TextToDisplay = Constants.Constants.PAUSE_TEXT;
            }

            oRenderThread.AskForNewItem(statusBarText, RenderItemType.NonPermanent);
            oRenderThread.AskForNewItem(levelText, RenderItemType.NonPermanent);
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
            oRenderThread.AskForNewItem(titleSprite, RenderItemType.Permanent);
            oRenderThread.AskForNewItem(heartSprite, RenderItemType.Permanent);
            oRenderThread.AskForNewItem(bubbleSprite, RenderItemType.Permanent);
            oRenderThread.AskForNewItem(coinSprite, RenderItemType.Permanent);
        }

        public void EngineProcessInputEvent()
        {

            currentState.ProcessInput(inputList);
            currentState = currentState.GetNextState();
            inputList.Clear(); //For next frame
        }

        public void InitGame()
        {

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
                        }, view.GetTopMargin(), view.GetLeftMargin(), view.GetBrickSize());
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
                }, view.GetTopMargin(), view.GetLeftMargin(), view.GetBrickSize()
            ) ;
            model.InitPlayer(player);
            AddSubscriber(model.GetPlayer());


        }

        public void InitRenderObjects()
        {
            statusBarText = new Text(Constants.Constants.PLAY_TEXT, new SizeF()
            {
                Width = (float) 2.65,
                Height = (float) 0.65
            }, "Arial", Color.Yellow, Color.Black, new coord()
            {
                x = 0,
                y = 0
            }, new coordF()
            {
                x = 0.2,
                y = 0.2
            },
            view.GetTopMargin(), view.GetLeftMargin(), view.GetBrickSize());
            AddSubscriber(statusBarText);

            levelText = new Text(view.GetLevelNumber().ToString(), new SizeF()
            {
                Width = (float)0.65,
                Height = (float)0.65
            }, "Arial", Color.Yellow, Color.Black, new coord()
            {
                x = 16,
                y = 0
            }, new coordF()
            {
                x = 0.2,
                y = 0.2
            },
            view.GetTopMargin(), view.GetLeftMargin(), view.GetBrickSize());
            AddSubscriber(levelText);

            titleSprite = new NotInGridSprite(new coord()
            {
                x = 0,
                y = -1
            }, new coordF()
            {
                x = 0.5,
                y = 0
            }, Constants.Constants.TITLE_SPRITE_NAME, view.GetTopMargin(), view.GetLeftMargin(), view.GetBrickSize(), 0.5f);
            AddSubscriber(titleSprite);
            oRenderThread.AskForNewItem(titleSprite, RenderItemType.Permanent);

            heartSprite = new NotInGridSprite(new coord()
            {
                x = 3,
                y = -2
            }, new coordF()
            {
                x = 0.15,
                y = 0.9
            }, Constants.Constants.HEART_SPRITE_NAME, view.GetTopMargin(), view.GetLeftMargin(), view.GetBrickSize(), 0.8f);
            AddSubscriber(heartSprite);
            oRenderThread.AskForNewItem(heartSprite, RenderItemType.Permanent);

            bubbleSprite = new NotInGridSprite(new coord()
            {
                x = 7,
                y = -2
            }, new coordF()
            {
                x = 0.15,
                y = 0.9
            }, Constants.Constants.BUBBLE_SPRITE_NAME + "1", view.GetTopMargin(), view.GetLeftMargin(), view.GetBrickSize(), 0.8f);
            AddSubscriber(bubbleSprite);
            oRenderThread.AskForNewItem(bubbleSprite, RenderItemType.Permanent);

            coinSprite = new NotInGridSprite(new coord()
            {
                x = 11,
                y = -2
            }, new coordF()
            {
                x = 0.15,
                y = 0.9
            }, Constants.Constants.COIN_SPRITE_NAME, view.GetTopMargin(), view.GetLeftMargin(), view.GetBrickSize(), 0.8f);
            AddSubscriber(coinSprite);
            oRenderThread.AskForNewItem(coinSprite, RenderItemType.Permanent);
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
