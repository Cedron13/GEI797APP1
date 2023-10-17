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
using System.Drawing;
using System.Reflection;
using System.Security.Policy;

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
        private bool isDeadOnce = false;
        private bool isDeadTwice = false;
        private bool waitLoadBubble = false;
        private bool isInvincible = false;
        private double invincibleTimer=0;
        private bool flashPlayer = false;
        private bool flashToxic = false;
        private bool isFlashingToxic;
        private double flashTempTimePlayer=0;
        private double flashTempTimeToxic = 0;
        private double flashToxicTimer;
        private bool fullCoin = false;
        private double gameOverTimer = 0;
        private double deadTimer = 0;

        private RenderThread oRenderThread;
        private PhysicsThread oPhysicsThread;
        private Thread renderThread;
        private Thread physicsThread; 
        private List<Wall> walls;
        private Wall transparentWall;
        private MiniSlimeSprite miniSlime;

        private Text statusBarText;
        private Text levelText;
        private Text deadText;
        private NotInGridSprite titleSprite;
        private NotInGridSprite heartSprite;
        private NotInGridSprite bubbleSprite;
        private NotInGridSprite coinSprite;
        private Bar healthBar;
        private Bar bubbleBar;
        private Bar coinBar;
        private NotInGridSprite keySprite;

        private const int BUBBLE_RELOAD_TIME = 1200;

        public bool IsPaused
        {
            get { return isPaused; }
            set { isPaused = value; }
        }

        public void SetFlashPlayer(bool v)
        {
            flashPlayer = v;
        }
        public bool GetFlashPlayer()
        {
            return flashPlayer;
        }
        public void SetFlashToxic(bool v)
        {
            flashToxic = v;
        }

        public bool GetFlashToxic()
        {
            return flashToxic;
        }

        public bool IsDeadOnce
        {
            get { return isDeadOnce; }
            set { isDeadOnce = value; }
        }
        public bool IsDeadTwice
        {
            get { return isDeadTwice; }
            set { isDeadTwice = value; }
        }

        public bool GetWaitLoadBubble()
        {
            return waitLoadBubble;
        }

        public void SetIsInvincible(bool b)
        {
            isInvincible = b;
        }

        public void SetIsFlashingToxic(bool b)
        {
            isFlashingToxic = b;
        }

        public void SetInvincibleTimer(double time)
        {
            invincibleTimer = time;
        }
        public void SetFlashToxicTimer(double time)
        {
            flashToxicTimer = time;
        }

        public void SetGameOverTimer(double time)
        {
            gameOverTimer = time;
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

            InitRenderObjects();
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
            oRenderThread.ResetItems();
            model.Update(lag);
            if (!model.GetDoorUnlocked()) oRenderThread.AskForNewItem(transparentWall, RenderItemType.NonPermanent);
            healthBar.SetProgression(model.GetPlayerLives()); //Vie du joueur
            levelText.TextToDisplay = view.GetLevelNumber().ToString();
            if (fullCoin) oRenderThread.AskForNewItem(keySprite, RenderItemType.NonPermanent);
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
                statusBarText.TextToDisplay = Constants.Constants.PLAY_TEXT;
                if (isFlashingToxic)
                {
                    flashToxicTimer += lag;
                    if (flashToxicTimer > flashTempTimeToxic + 500 && flashToxicTimer < 1000)
                    {
                        flashToxic = !flashToxic;
                        flashTempTimeToxic = flashToxicTimer;
                    }
                    if (flashToxicTimer > 1000)
                    {
                        isFlashingToxic = false;
                        flashTempTimeToxic = 0;
                        flashToxic = false;
                    }
                }
                if (isInvincible)
                {
                    //Console.WriteLine("je suis invincible");
                    invincibleTimer += lag;
                    if (invincibleTimer > flashTempTimePlayer + 500 && invincibleTimer<3000)
                    {
                        flashPlayer = !flashPlayer;
                        flashTempTimePlayer = invincibleTimer;
                    }
                    if (invincibleTimer > 3000)
                    {
                        flashPlayer = false;
                        isInvincible = false;
                        //Console.WriteLine("je suis plus invincible");
                        model.GetPlayer().SetTimeDone(true);
                        flashTempTimePlayer = 0;
                        model.GetPlayer().SetTransparency(0);
                    }
                }
                if(waitLoadBubble)
                {
                    transitionTimeBubble += lag;
                    int prog = (int)(transitionTimeBubble / BUBBLE_RELOAD_TIME * 6);
                    bubbleBar.SetProgression(prog);
                    if (transitionTimeBubble > BUBBLE_RELOAD_TIME)
                    {
                        waitLoadBubble = false;
                        //Console.WriteLine("c'est okok");
                    }
                }

            }
            else if (currentState is PausedState)
            {
                statusBarText.TextToDisplay = Constants.Constants.PAUSE_TEXT;
                if (isDeadTwice)
                {
                    deadText.TextToDisplay = Constants.Constants.GAMEOVER_TEXT;
                    oRenderThread.AskForNewItem(deadText, RenderItemType.NonPermanent);
                    gameOverTimer += lag;
                    if (gameOverTimer > 3000)
                    {
                        isDeadTwice = false;
                        isDeadOnce = false;
                        model.SetIsAlreadyDead(false);
                        view.Close();
                        // menu display
                    }
                }
                else
                {
                    oRenderThread.AskForNewItem(deadText, RenderItemType.NonPermanent);
                    deadTimer += lag;
                    if (deadTimer > 5000)
                    {
                        if (model.GetPlayerLives() == 0)
                        {
                            deadText.TextToDisplay = Constants.Constants.GAMEOVER_TEXT;
                            oRenderThread.AskForNewItem(deadText, RenderItemType.NonPermanent);
                            gameOverTimer += lag;
                            if (gameOverTimer > 3000)
                            {
                                isDeadTwice = false;
                                isDeadOnce = false;
                                model.SetIsAlreadyDead(false);
                                view.Close();
                                isPaused=false;
                                // menu display
                            }
                        }
                        else
                        {
                            currentState.PrepareNextState();
                            currentState.GetNextState();
                            isPaused = false;

                        }
                        model.SetIsPaused(false);

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
                statusBarText.TextToDisplay = Constants.Constants.VICTORY_TEXT;
            }

            oRenderThread.AskForNewItem(statusBarText, RenderItemType.NonPermanent);
            oRenderThread.AskForNewItem(levelText, RenderItemType.NonPermanent);
            oRenderThread.AskForNewItem(healthBar, RenderItemType.NonPermanent);
            oRenderThread.AskForNewItem(bubbleBar, RenderItemType.NonPermanent);
            oRenderThread.AskForNewItem(coinBar, RenderItemType.NonPermanent);
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
            oRenderThread.AskForNewItem(miniSlime, RenderItemType.Permanent);
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
                    else if (model.GetLabyrinth()[i, j] == 2)
                    {
                        transparentWall = new Wall(new coord()
                        {
                            x = j,
                            y = i
                        }, top, left, brick, 150);
                        AddSubscriber(transparentWall);
                    }
                    else if (model.GetLabyrinth()[i, j] == 5)
                    {
                        miniSlime = new MiniSlimeSprite(new coord()
                        {
                            x = j,
                            y = i
                        }, top, left, brick);
                        AddSubscriber(miniSlime);
                        oRenderThread.AskForNewItem(miniSlime, RenderItemType.Permanent);
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

            deadText = new Text(Constants.Constants.DEADONCE_TEXT, new SizeF()
            {
                Width = (float)8,
                Height = (float)6
            }, "Arial", Color.Yellow, Color.Black, new coord()
            {
                x = 4,
                y = 4
            }, new coordF()
            {
                x = 0.5,
                y = 0.5
            },
            view.GetTopMargin(), view.GetLeftMargin(), view.GetBrickSize());
            AddSubscriber(deadText);

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

            healthBar = new Bar(new coord()
            {
                x = 3,
                y = -2
            }, new coordF()
            {
                x = 0.5,
                y = 0.9
            }, false, 3, BarType.HEALTH, view.GetTopMargin(), view.GetLeftMargin(), view.GetBrickSize(), 0.8f);
            AddSubscriber(healthBar);

            bubbleBar = new Bar(new coord()
            
            {
                x = 7,
                y = -2
            }, new coordF()
            {
                x = 0.5,
                y = 0.9
            }, true, 6, BarType.BUBBLE, view.GetTopMargin(), view.GetLeftMargin(), view.GetBrickSize(), 0.8f);
            bubbleBar.SetProgression(6);
            AddSubscriber(bubbleBar);

            coinBar = new Bar(new coord()
            {
                x = 11,
                y = -2
            }, new coordF()
            {
                x = 0.5,
                y = 0.9
            }, true, 6, BarType.COIN, view.GetTopMargin(), view.GetLeftMargin(), view.GetBrickSize(), 0.8f);
            AddSubscriber(coinBar);

            keySprite = new NotInGridSprite(new coord()
            {
                x = 15,
                y = -2
            }, new coordF()
            {
                x = 0.60,
                y = 0.9
            }, Constants.Constants.KEY_SPRITE_NAME, view.GetTopMargin(), view.GetLeftMargin(), view.GetBrickSize(), 0.8f);
            AddSubscriber(keySprite);
        }


        public void SetGemCounter(int i)
        {
            coinBar.SetProgression(i);
            fullCoin = i == 6 ? true : false;
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
            //Console.WriteLine("boule envoyée"); // OK
        }

        public int NewLevel()
        {
            model.ClearCommandHistory();
            model.SetLabyrinth(GetLabyrinth());
            view.SetGemCounter(0);
            SetGemCounter(0);
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

        
        

        public void IsDying()
        {
            isPaused = true;
            currentState.PrepareNextState(Constants.GameStates.PAUSE);
            currentState = currentState.GetNextState();

        }
        public void ShowFPS(float fps)
        {
            view.SetFPS(fps);
        }
        public void ChangeFpsDisplay()
        {
            view.SetFpsDisplay(!view.GetFpsDisplay());
        }


    }
}
