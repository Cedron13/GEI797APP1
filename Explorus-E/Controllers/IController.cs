using ExplorusE.Controllers.States;
using ExplorusE.Models;
using System.Windows.Forms;

/* EXPLORUS-E
 * Alexis BLATRIX (blaa1406)
 * Cédric CHARRON (chac0902)
 * Audric DAVID (dava1302)
 * Matthieu JEHANNE (jehm1701)
 * Cloé LEGLISE (legc1001)
 */

namespace ExplorusE.Controllers
{
    public struct RenderData
    {
        public int[,] lab;
    }
    internal interface IController
    {
        void EngineRenderEvent();
        void EngineUpdateEvent(double lag);
        void EngineProcessInputEvent();

        void ViewKeyPressedEvent(PreviewKeyDownEventArgs e);
        void ViewCloseEvent();
        void ProcessMinimize();

        void ProcessLostFocus();

        void EndProcessLostFocus();
        //TEMP
        int[,] GetLabyrinth();
        bool IsPaused { get; set; }
        void InitGame();
        void PositionUpdate();

        GameModel GetGameModel();

        Sprite GetPlayer();
        void SetGemCounter(int i);
        IState GetState();
        int GetTransitionTime();
        void ExitPause();
        void NewLevel(); 

    }
}
