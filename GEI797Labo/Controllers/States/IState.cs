using GEI797Labo.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GEI797Labo.Controllers.States
{
    internal interface IState
    {

        void ProcessInput(List<Keys> keys);

        IState GetNextState();

        void PrepareNextState(GameStates nextState = GameStates.UNKNOWN); //Let the classes decide on their own default case
        
    }
}
