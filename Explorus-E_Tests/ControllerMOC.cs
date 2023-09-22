using ExplorusE.Controllers.States;
using ExplorusE.Models;


/* EXPLORUS-E
 * Alexis BLATRIX (blaa1406)
 * Cédric CHARRON (chac0902)
 * Audric DAVID (dava1302)
 * Matthieu JEHANNE (jehm1701)
 * Cloé LEGLISE (legc1001)
 */

namespace Tests
{
    internal class ControllerMOC : ExplorusE.Controllers.IControllerModel
    {
        private int gemCounter = 0;
        public void EngineRenderEvent()
        {

        }

        public void EngineUpdateEvent(double lag)
        {

        }

        public void EngineProcessInputEvent()
        {

        }
        public void ModelCloseEvent()
        {

        }
        public int[,] GetLabyrinth()
        {
            return new int[,] { { 0 } };
        }

        public bool IsPaused { get; set; }

        public void InitGame()
        {

        }
        public void EndGameReached()
        {

        }

        public GameModel GetGameModel()
        {
            return null;
        }

        public Sprite GetPlayer()
        {
            return null;
        }
        public void SetGemCounter(int i)
        {
            gemCounter = i;
        }

        public IState GetState()
        {
            return null;
        }

        public int GetTransitionTime()
        {
            return 0;
        }

        public void ExitPause()
        {

        }

        public int NewLevel()
        {
            return 0;
        }
    }
}
