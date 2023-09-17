using GEI797Labo.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GEI797Labo.Controllers.States
{
    internal class TransitionState : IState
    {
        IState nextState = null;
        IController controller;
        public TransitionState(IController c)
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
