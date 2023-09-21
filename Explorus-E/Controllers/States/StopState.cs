using ExplorusE.Constants;
using System.Collections.Generic;
using System.Windows.Forms;


namespace ExplorusE.Controllers.States
{
    internal class StopState : IState
    {
        IState nextState = null;
        IControllerModel controller;
        public StopState(IControllerModel c)
        {
            nextState = this;
            controller = c;
        }
        public void ProcessInput(List<Keys> keys)
        {
        }

        public IState GetNextState() => nextState;

        public void PrepareNextState(GameStates state = GameStates.STOP) //No Default Next
        {
            
        }
    }
}
