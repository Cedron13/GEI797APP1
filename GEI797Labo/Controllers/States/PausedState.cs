using GEI797Labo.Constants;
using GEI797Labo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GEI797Labo.Controllers.States
{
    internal class PausedState: IState
    {
        IState nextState = null;
        IController controller;
        public PausedState(IController c) { 
            nextState = this;
            controller = c;
        }
        public void ProcessInput(List<Keys> keys) {
            GameModel model = controller.GetGameModel();
            Sprite player = model.GetPlayer();
            foreach (Keys e in keys)
            {
                switch (e)
                {
                    case Keys.Right:
                        {
                            if(player.IsMovementOver())
                                model.RedoNextCommand();
                            break;
                        }
                    case Keys.Left:
                        {
                            if (player.IsMovementOver())
                                model.UndoLastCommand();
                            break;
                        }

                    case Keys.R:
                        {
                            model.ClearAfterCurrentActionIndex();
                            PrepareNextState();
                            //Unpause Logic
                            controller.ExitPause();
                            break;
                        }
                }
            }

        }
        public IState GetNextState() => nextState;
        public void PrepareNextState(GameStates state = GameStates.RESUME) //Default next state is RESUME
        {
            if (state == GameStates.UNKNOWN) state = GameStates.RESUME; //If the method is called from the interface IState
            switch (state)
            {
                //List here the possible output states
                case GameStates.RESUME: nextState = new TransitionState(controller); break;
                default: break;
            }
        }
    }
}
