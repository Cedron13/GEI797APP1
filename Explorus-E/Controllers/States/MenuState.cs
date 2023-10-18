using ExplorusE.Constants;
using ExplorusE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExplorusE.Controllers.States
{
    internal class MenuState : IState
    {
        private IState nextState = null;
        private IControllerModel controller;

        public MenuState(IControllerModel controller)
        {
            nextState = this;
            this.controller = controller;
        }

        public void ProcessInput(List<Keys> keys)
        {
            GameModel model = controller.GetGameModel();
            foreach (Keys e in keys)
            {
                switch (e)
                {
                    case Keys.Down:
                    {
                        //change selection 
                        break;
                    }
                    case Keys.Up:
                    {
                        //change selection 
                        break;
                    }
                    case Keys.Right:
                        {
                            
                            break;
                        }
                    case Keys.Left:
                        {
                            
                            break;
                        }

                    case Keys.R:
                        {
                            model.ClearAfterCurrentActionIndex();
                            PrepareNextState();
                            //Unpause Logic
                            model.SetIsPaused(false);
                            controller.ExitPause();
                            controller.GetGameModel().SetIsPaused(false);
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
