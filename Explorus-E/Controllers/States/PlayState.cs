using ExplorusE.Constants;
using ExplorusE.Models;
using ExplorusE.Models.Commands;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Windows.Media;

/* EXPLORUS-E
 * Alexis BLATRIX (blaa1406)
 * Cédric CHARRON (chac0902)
 * Audric DAVID (dava1302)
 * Matthieu JEHANNE (jehm1701)
 * Cloé LEGLISE (legc1001)
 */

namespace ExplorusE.Controllers.States
{
    internal class PlayState: IState
    {
        private IState nextState = null;
        private IControllerModel controller;
        private int index = 0;

        public PlayState(IControllerModel c)
        {
            nextState = this;
            controller = c;
        }

        public void ProcessInput(List<Keys> keys) {
            GameModel model = controller.GetGameModel();
            coord initialCoord = model.GetGridCoord();
            int index = 0;
            foreach (Keys e in keys)
            {
                switch (e)
                {
                    case Keys.Down:
                        {
                            coord dest = new coord()
                            {
                                x = initialCoord.x,
                                y = initialCoord.y+1
                            };
                            MoveCommand com = new MoveCommand(Direction.DOWN, initialCoord, dest);
                            model.InvokeCommand(com);
                            break;
                        }
                    case Keys.Up:
                        {
                            coord dest = new coord()
                            {
                                x = initialCoord.x, 
                                y = initialCoord.y -1
                            };
                            MoveCommand com = new MoveCommand(Direction.UP, initialCoord, dest);
                            model.InvokeCommand(com);
                            break;
                        }
                    case Keys.Right:
                        {
                            coord dest = new coord()
                            {
                                x = initialCoord.x+1, 
                                y = initialCoord.y
                            };
                            MoveCommand com = new MoveCommand(Direction.RIGHT, initialCoord, dest);
                            model.InvokeCommand(com);
                            break;
                        }
                    case Keys.Left:
                        {
                            coord dest = new coord()
                            {
                                x = initialCoord.x-1, 
                                y = initialCoord.y
                            };
                            MoveCommand com = new MoveCommand(Direction.LEFT, initialCoord, dest);
                            model.InvokeCommand(com);
                            
                            break;
                        }
                    case Keys.P:
                        {
                            PrepareNextState();
                            //Enter Pause Logic
                            controller.IsPaused = true;
                            controller.GetGameModel().SetIsPaused(true);
                            break;
                        }
                    case Keys.Space:
                        {
                            if (!controller.GetWaitLoadBubble())
                                {

                                Direction playerDirection = model.GetPlayer().GetDirection();
                                BubbleSprite newBubble = new BubbleSprite(initialCoord, controller.GetPlayer().GetActualTop(), controller.GetPlayer().GetActualLeft(), controller.GetPlayer().GetActualBricksize(), 0.5f);
                                controller.AddSubscriber(newBubble);
                                newBubble.StartMovement(initialCoord, playerDirection);
                                model.GetAudioList().Add("ThrowBubble.wav");
                                model.InvokeCommand(new BubbleCreateCommand(newBubble));
                                model.InvokeCommand(new BubbleMoveCommand(newBubble));
                                controller.WaitForNewBubble();
                            }
                            break;
                        }
                    case Keys.F:
                        {
                            controller.ChangeFpsDisplay();
                            break;
                        }
                    case Keys.Escape:
                        {
                            PrepareNextState();
                            controller.GetPauseMenu().SetIsPlaying(true);
                            controller.GetPauseMenu().Update();
                            controller.LaunchMenu();
                            break;
                        }

                }
            }
        }

        public IState GetNextState() => nextState;

        public void PrepareNextState(GameStates state = GameStates.PAUSE) //Default next state is PAUSE
        {
            if (state == GameStates.UNKNOWN) state = GameStates.PAUSE; //If the method is called from the interface IState
            switch (state)
            {
                //List here the possible output states
                case GameStates.PAUSE: nextState = new PausedState(controller); break;
                case GameStates.STOP: nextState = new StopState(controller); break;
                case GameStates.MENU: nextState = new MenuState(controller);break;
                default: break;
            }
        }
    }
}
