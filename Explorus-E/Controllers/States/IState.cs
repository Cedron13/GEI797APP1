using ExplorusE.Constants;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ExplorusE.Controllers.States
{
    internal interface IState
    {

        void ProcessInput(List<Keys> keys);

        IState GetNextState();

        void PrepareNextState(GameStates nextState = GameStates.UNKNOWN); //Let the classes decide on their own default case
        
    }
}
