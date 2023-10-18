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
    internal class HelpState : IState
    {
        private IState nextState = null;
        private IControllerModel controller;
        private HelpMenu helpMenu;

        private bool selection = false;

        public HelpState(IControllerModel controller)
        {
            nextState = this;
            this.controller = controller;
            this.helpMenu = controller.GetHelpMenu();
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
                            selection = !selection;
                            helpMenu.SetColor(selection);
                            helpMenu.Update();
                            break;
                        }
                    case Keys.Up:
                        {
                            selection = !selection;
                            helpMenu.SetColor(selection);
                            helpMenu.Update();
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
                    case Keys.Enter:
                        {
                            if(selection)
                            {
                                helpMenu.SetColor(false);
                                helpMenu.Update();
                                controller.LaunchMenu();
                            }
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
                case GameStates.MENU: nextState = new MenuState(controller);break;
                default: break;
            }
        }


    }
}
