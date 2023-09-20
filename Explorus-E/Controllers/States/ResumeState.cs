using ExplorusE.Constants;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ExplorusE.Controllers.States
{
    internal class ResumeState : IState
    {
        IState nextState = null;
        IController controller;
        public ResumeState(IController c)
        {
            nextState = this;
            controller = c;
        }
        public void ProcessInput(List<Keys> keys) {
            //Do nothing on input, state should switch to play STate after delay
            //Temporarily, changing state directly to Play
            //nextState = new PlayState(controller);
        }
        public IState GetNextState() => nextState;

        public void PrepareNextState(GameStates state = GameStates.PLAY) //Default next state is PLAY
        {
            if (state == GameStates.UNKNOWN) state = GameStates.PLAY; //If the method is called from the interface IState
            switch (state)
            {
                //List here the possible output states
                case GameStates.PLAY: nextState = new PlayState(controller); break;
                case GameStates.PAUSE: nextState = new PausedState(controller); break;
                default: break;
            }
        }
    }
}
