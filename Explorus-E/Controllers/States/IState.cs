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
    internal interface IState
    {

        void ProcessInput(List<Keys> keys);

        IState GetNextState();

        void PrepareNextState(GameStates nextState = GameStates.UNKNOWN); //Let the classes decide on their own default case
        
    }
}
