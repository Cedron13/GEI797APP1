using GEI797Labo.Controllers;
using GEI797Labo.Controllers.States;
using GEI797Labo.Models;
using GEI797Labo.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

/* EXPLORUS-E
 * Alexis BLATRIX (blaa1406)
 * Cédric CHARRON (chac0902)
 * Audric DAVID (dava1302)
 * Matthieu JEHANNE (jehm1701)
 * Cloé LEGLISE (legc1001)
 */

namespace GEI797Labo
{
    internal class Controller : IController, IResizeEventPublisher
    {
        private GameEngine engine;
        private GameView view;
        private GameModel model;
        private IState currentState;
        private List<IResizeEventSubscriber> resizeSubscribers;
        private double transitionTime = 0;
        private bool isPaused = false;
        public bool IsPaused
        {
            get { return isPaused; }
            set { isPaused = value; }
        }

        private List<Keys> inputList;

        public Controller()
        {
            model = new GameModel(this);
            inputList = new List<Keys>();
            resizeSubscribers = new List<IResizeEventSubscriber>();
            view = new GameView(this);
            currentState = new PlayState(this);
            InitGame();

            engine = new GameEngine(this);
            //Order is very important due to dependencies between each oject, this order works 👍
        }

        //IController

        public void ViewCloseEvent()
        {
            engine.KillEngine(); //Works 👍
        }
        public void ViewKeyReleasedEvent()
        {

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
        public void EngineRenderEvent() {
            view.Render();
        }
        public void EngineUpdateEvent(double lag)
        {
            model.Update(lag);
            if(currentState is TransitionState)
            {
                Console.WriteLine(lag);
                transitionTime += lag;
                if(transitionTime > 3000)
                {
                    currentState = new PlayState(this);
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
        public void EngineProcessInputEvent() {

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


        //TEMP
        public int[,] GetLabyrinth() { 
            return model.GetLabyrinth();
        }

        public void ProcessMinimize()
        {
            isPaused = true;
            currentState = new PausedState(this);
            Console.WriteLine("minimize ok");
        }

        public void EndProcessMinimize()
        {
            ExitPause();
            currentState = new TransitionState(this);
            Console.WriteLine("reprise du jeu");
        }

        public void ProcessLostFocus()
        {
            isPaused = true;
            currentState = new PausedState(this);
            Console.WriteLine("perte focus");
        }

        public void EndProcessLostFocus()
        {
            ExitPause();
            currentState = new TransitionState(this);
            Console.WriteLine("fin perte focus");
        }

        public void ExitPause()
        {
            isPaused = false;
            transitionTime = 0;
        }

        public void NewLevel()
        {
            model.ClearCommandHistory();
            model.SetLabyrinth(GetLabyrinth());
            view.SetGemCounter(0);
            view.SetLevelNumber(view.GetLevelNumber()+1);
            model.SetGridCoord(new coord()
            {
                x = view.GetInitPosX(), //Place holder coordinates
                y = view.GetInitPosY()
            });
            model.GoTo(Direction.DOWN, model.GetGridCoord());
        }


        public Sprite GetPlayer() => model.GetPlayer();
        public GameModel GetGameModel() => model;
        public IState GetState() => currentState;
        public int GetTransitionTime() => (int)transitionTime;



    }
}
