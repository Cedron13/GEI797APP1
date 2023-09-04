using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static GEI797Labo.Models.GameModel;

namespace GEI797Labo.Controllers
{
    public struct RenderData
    {
        public int[,] lab;
        //playerSprite
        //enemiesSpriteArray
    }
    internal interface IController
    {
        void EngineRenderEvent();
        void EngineUpdateEvent(double lag);
        void EngineProcessInputEvent();

        void ViewKeyPressedEvent(PreviewKeyDownEventArgs e);
        void ViewCloseEvent();
        void ViewKeyReleasedEvent();

        //TEMP
        int[,] GetLabyrinth();
        void InitGame();
        void PositionUpdate();
    }
}
