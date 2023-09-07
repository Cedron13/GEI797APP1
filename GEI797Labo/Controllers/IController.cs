using System.Windows.Forms;

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
        bool IsPaused { get; set; }
        void InitGame();
        void PositionUpdate();

        Sprite GetPlayer();
        void SetGemCounter(int i);
        void SetEndGame(bool b);
    }
}
