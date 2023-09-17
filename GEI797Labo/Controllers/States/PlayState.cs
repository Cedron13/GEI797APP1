﻿using GEI797Labo.Constants;
using GEI797Labo.Models;
using GEI797Labo.Models.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GEI797Labo.Controllers.States
{
    internal class PlayState: IState
    {
        private IState nextState = null;
        private IController controller;

        public PlayState(IController c)
        {
            nextState = this;
            controller = c;
        }
        public void ProcessInput(List<Keys> keys) {
            GameModel model = controller.GetGameModel();
            coord initialCoord = model.GetGridCoord();
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
                default: break;
            }
        }
    }
}
