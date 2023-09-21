using ExplorusE.Controllers.States;
using ExplorusE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

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
