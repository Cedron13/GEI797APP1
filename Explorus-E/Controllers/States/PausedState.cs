using ExplorusE.Constants;
using ExplorusE.Models;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ExplorusE.Controllers.States
{
    internal class PausedState: IState
    {
        private IState nextState = null;
        private IControllerModel controller;
        public PausedState(IControllerModel c) { 
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
                    case Keys.F:
                        {
                            controller.ChangeFpsDisplay();
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
                case GameStates.RESUME: nextState = new ResumeState(controller); break;
                default: break;
            }
        }
    }
}
