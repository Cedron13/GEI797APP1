﻿using ExplorusE.Controllers.States;
using ExplorusE.Models;
using ExplorusE.Observer;
using ExplorusE.Views;
using ExplorusE.Constants;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ExplorusE.Threads;
using System.Threading;

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
        private double stopTime = 0;
        private bool isPaused = false;

        private RenderThread oRenderThread;
        private Thread renderThread;

        public bool IsPaused
        {
            get { return isPaused; }
            set { isPaused = value; }
        }

        private List<Keys> inputList;

        public Controller()
        {
            oRenderThread = new RenderThread(); //TODO: Look for which object needs an access to oRenderThread
            model = new GameModel(this);
            inputList = new List<Keys>();
            resizeSubscribers = new List<IResizeEventSubscriber>();
            view = new GameView(this);
            currentState = new PlayState(this);
            InitGame();

            renderThread = new Thread(new ThreadStart(oRenderThread.Run));
            renderThread.Name = "Render Thread";
            renderThread.Start();

            engine = new GameEngine(this);
            //Order is very important due to dependencies between each object, this order works 👍
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

            foreach (IResizeEventSubscriber s in resizeSubscribers)
            {
                
                s.NotifyResize(view.GetTopMargin(), view.GetLeftMargin(), view.GetBrickSize());
            }
        }

        public void EngineProcessInputEvent()
        {

            currentState.ProcessInput(inputList);
            currentState = currentState.GetNextState();
            inputList.Clear(); //For next frame
        }

        public void InitGame()
        {
            for (int i = 0; i < model.GetLabyrinth().GetLength(0); i++)
            {
                for (int j = 0; j < model.GetLabyrinth().GetLength(1); j++)
                {
                    if (model.GetLabyrinth()[i, j] == 3)
                    {
                        model.SetGridPosX(j);
                        model.SetGridPosY(i);
                    }
                }
            }

            Sprite player = new Sprite(
                new coord()
                {
                    x = model.GetGridPosX(), //Place holder coordinates
                    y = model.GetGridPosY()
                }, view.GetTopMargin(), view.GetLeftMargin(), view.GetBrickSize()
            ) ;
            model.InitPlayer(player);
            AddSubscriber(model.GetPlayer());
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
        

        public Sprite GetPlayer() => model.GetPlayer();
        public GameModel GetGameModel() => model;
        public IState GetState() => currentState;
        public int GetTransitionTime() => (int)transitionTime;



    }
}
