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
        private PauseMenu pauseMenu;

        private int selectionIndex = 0;

        public MenuState(IControllerModel controller)
        {
            nextState = this;
            this.controller = controller;
            this.pauseMenu = controller.GetPauseMenu();
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
                        if (selectionIndex >= 4) { selectionIndex=1; }
                        else { selectionIndex ++; }
                        pauseMenu.SetColor(selectionIndex);
                        pauseMenu.Update();
                        break;
                    }
                    case Keys.Up:
                    {
                        if (selectionIndex > 1) { selectionIndex--; }
                        else { selectionIndex = 4; }
                        pauseMenu.SetColor(selectionIndex);  
                        pauseMenu.Update();
                        break;
                    }
                    case Keys.Right:
                        {
                            if (selectionIndex == 2 && pauseMenu.GetVolume() < 100)
                            {
                                pauseMenu.SetVolume(pauseMenu.GetVolume()+10);
                                pauseMenu.Update();
                            }
                            break;
                        }
                    case Keys.Left:
                        {
                            if (selectionIndex == 2 && pauseMenu.GetVolume() > 0)
                            {
                                pauseMenu.SetVolume(pauseMenu.GetVolume() - 10);
                                pauseMenu.Update();
                            }
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
                            switch (selectionIndex)
                            {
                                case 1:
                                    {
                                        if(pauseMenu.GetIsPlaying())
                                        {
                                            pauseMenu.SetColor(0);
                                            pauseMenu.Update();
                                            model.ClearAfterCurrentActionIndex();
                                            PrepareNextState();
                                            model.SetIsPaused(false);
                                            controller.ExitPause();
                                            controller.GetGameModel().SetIsPaused(false);
                                        }
                                        else
                                        {
                                            pauseMenu.SetColor(0);
                                            pauseMenu.Update();
                                            controller.NewGame();
                                        }

                                        break;
                                    }
                                case 2:
                                    {
                                        pauseMenu.SetColor(0);
                                        pauseMenu.Update();

                                        //sound

                                        break; 
                                    }
                                case 3:
                                    {
                                        pauseMenu.SetColor(0);
                                        pauseMenu.Update();
                                        controller.LaunchHelp();
                                        
                                        break;
                                    }
                                case 4:
                                    {
                                        controller.KillApp(); 
                                        break;
                                    }
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
                case GameStates.HELP: nextState = new HelpState(controller);break;
                case GameStates.STOP: nextState = new StopState(controller);  break;
                default: break;
            }
        }


    }
}
