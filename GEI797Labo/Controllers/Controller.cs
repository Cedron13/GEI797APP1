using GEI797Labo.Controllers;
using GEI797Labo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
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

        private Sprite player;

        private List<Keys> inputList;

        private int TILE_SIZE;

        public Controller()
        {
            model = new GameModel(this);
            inputList = new List<Keys>();
            TILE_SIZE = 96;
            tileManager = new TileManager();
            InitGame();

            view = new GameView(this);
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
            if (!player.IsMovementOver())
            {
                player.Update((int)lag);
            }
        }
        public void EngineProcessInputEvent() {
            foreach(Keys e in inputList)
            {
                switch (e)
                {
                    case Keys.Down:
                        {
                            coord playerCoord = player.GetPosition();
                            playerCoord.y += TILE_SIZE;
                            if(player.IsMovementOver()) player.StartMovement(playerCoord);
                            break;
                        }
                    case Keys.Up:
                        {
                            coord playerCoord = player.GetPosition();
                            playerCoord.y -= TILE_SIZE;
                            if (player.IsMovementOver()) player.StartMovement(playerCoord);
                            break;
                        }
                    case Keys.Right:
                        {
                            coord playerCoord = player.GetPosition();
                            playerCoord.x += TILE_SIZE;
                            if (player.IsMovementOver()) player.StartMovement(playerCoord);
                            break;
                        }
                    case Keys.Left:
                        {
                            coord playerCoord = player.GetPosition();
                            playerCoord.x -= TILE_SIZE;
                            if (player.IsMovementOver()) player.StartMovement(playerCoord);
                            break;
                        }
                }
            }
            inputList.Clear(); //For next frame
        }

        public void InitGame()
        {
            //Initiate Slimus
            player = new Sprite(
                tileManager.getImage("Down1"),
                new coord()
                {
                    x = 96 * 4 + 20, //Place holder coordinates, TODO: adapt with screen size and initial placement
                    y = 96 * 7 + 144
                },
                0,
                500
            );
        }

        //TEMP
        public int[,] GetLabyrinth() { 
            return model.GetLabyrinth();
        }

        public Sprite GetPlayer() => player;


    }
}
