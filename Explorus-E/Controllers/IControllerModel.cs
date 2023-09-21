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
    internal interface IControllerModel
    {
        void EngineRenderEvent();

        void EngineUpdateEvent(double lag);

        void EngineProcessInputEvent();

        int[,] GetLabyrinth();

        bool IsPaused { get; set; }

        void InitGame();

        void EndGameReached();

        void ModelCloseEvent();

        GameModel GetGameModel();

        Sprite GetPlayer();
        void SetGemCounter(int i);

        IState GetState();

        int GetTransitionTime();

        void ExitPause();

        int NewLevel(); 
    }
}
