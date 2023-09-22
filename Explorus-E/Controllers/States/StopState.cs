using ExplorusE.Constants;
using System.Collections.Generic;
using System.Windows.Forms;

/* EXPLORUS-E
 * Alexis BLATRIX (blaa1406)
 * Cédric CHARRON (chac0902)
 * Audric DAVID (dava1302)
 * Matthieu JEHANNE (jehm1701)
 * Cloé LEGLISE (legc1001)
 */

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
