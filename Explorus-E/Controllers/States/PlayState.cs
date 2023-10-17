using ExplorusE.Constants;
using ExplorusE.Models;
using ExplorusE.Models.Commands;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

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
                            break;
                        }
                    case Keys.Space:
                        {
                            if (!controller.GetWaitLoadBubble())
                                {

                                //Console.WriteLine(init.x.ToString() +" "+ init.y.ToString());
                                Direction playerDirection = model.GetPlayer().GetDirection();
                                BubbleSprite newBubble = new BubbleSprite(initialCoord, controller.GetPlayer().GetActualTop(), controller.GetPlayer().GetActualLeft(), controller.GetPlayer().GetActualBricksize());
                                controller.AddSubscriber(newBubble);
                                newBubble.StartMovement(initialCoord, playerDirection);
                                model.AddBubble(newBubble);
                                controller.WaitForNewBubble();
                            }
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

        public void PrepareNextState(GameStates state = GameStates.PAUSE) //Default next state is PAUSE
        {
            if (state == GameStates.UNKNOWN) state = GameStates.PAUSE; //If the method is called from the interface IState
            switch (state)
            {
                //List here the possible output states
                case GameStates.PAUSE: nextState = new PausedState(controller); break;
                case GameStates.STOP: nextState = new StopState(controller); break;
                default: break;
            }
        }
    }
}
