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
        private GameView view;
        private GameModel model;
        public Controller() { 
            model = new GameModel(this);
            view = new GameView(this);
            engine = new GameEngine(this);

        }

        //IController

        public void ViewCloseEvent()
        {

        }
        public void ViewKeyReleasedEvent()
        {

        }

        public void ViewKeyPressedEvent(PreviewKeyDownEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Down:
                    
                    break;
                case Keys.Up:
                    break;
                case Keys.Right:
                    break;
                case Keys.Left:
                    break;
            }
        }
        public void EngineRenderEvent() {
            view.Render();
        }
        public void EngineUpdateEvent(double lag) {
            
        }
        public void EngineProcessInputEvent() {
            
        }

        //TEMP
        public int[,] GetLabyrinth() { 
            return model.GetLabyrinth();
        }

    }
}
