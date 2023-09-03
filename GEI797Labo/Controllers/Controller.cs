using GEI797Labo.Controllers;
using GEI797Labo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GEI797Labo
{
    internal class Controller : IController
    {
        private GameEngine engine;
        private TileManager tileManager;
        private GameView view;
        private GameModel model;
        private int initPosX;
        private int initPosY;


        private List<Keys> inputList;

        public Controller()
        {
            model = new GameModel(this);
            inputList = new List<Keys>();
            tileManager = new TileManager();
            view = new GameView(this);
            InitGame();

            engine = new GameEngine(this);
            //Order is very important due to dependencies between each oject, this order works 👍
        }

        //IController

        public void ViewCloseEvent()
        {
            //Engine est null. À investiguer
            engine.KillEngine(); //Marche 👍
        }
        public void ViewKeyReleasedEvent()
        {

        }

        public void ViewKeyPressedEvent(PreviewKeyDownEventArgs e)
        {
            //To avoid similar inputs between two frames
            if(!inputList.Contains(e.KeyCode)){
                if (!inputList.Contains(Keys.Down) && !inputList.Contains(Keys.Up) && !inputList.Contains(Keys.Right) && !inputList.Contains(Keys.Left)) //Checking if a directional input is already registred
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
        }
        public void EngineProcessInputEvent() {
            foreach(Keys e in inputList)
            {
                switch (e)
                {
                    case Keys.Down:
                        {
                            model.MoveDown();
                            break;
                        }
                    case Keys.Up:
                        {
                            model.MoveUp();
                            break;
                        }
                    case Keys.Right:
                        {
                            model.MoveRight();
                            break;
                        }
                    case Keys.Left:
                        {
                            model.MoveLeft();
                            break;
                        }
                }
            }
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
                        initPosX = view.GetBrickSize() * j + view.GetLeftMargin();
                        initPosY = view.GetBrickSize() * i + view.GetTopMargin() + view.GetBrickSize();
                    }

                }

            }
            Sprite player = new Sprite(
                new coord()
                {
                    x = initPosX, //Place holder coordinates, TODO: adapt with screen size 
                    y = initPosY    
                }
            );
            model.InitPlayer(player);
        }

        //TEMP
        public int[,] GetLabyrinth() { 
            return model.GetLabyrinth();
        }

        public Sprite GetPlayer() => model.GetPlayer();



    }
}
