﻿using GEI797Labo.Models;
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
        IState nextState = null;
        IController controller;
        public PlayState(IController c)
        {
            nextState = this;
            controller = c;
        }
        public void ProcessInput(List<Keys> keys) {
            GameModel model = controller.GetGameModel();
            int topMargin = controller.GetTopMargin();
            int leftmargin = controller.GetLeftMargin();
            int brickSize = controller.GetBrickSize();
            foreach (Keys e in keys)
            {
                switch (e)
                {
                    case Keys.Down:
                        {
                            model.MoveDown(topMargin, leftmargin, brickSize);
                            break;
                        }
                    case Keys.Up:
                        {
                            model.MoveUp(topMargin, leftmargin, brickSize);
                            break;
                        }
                    case Keys.Right:
                        {
                            model.MoveRight(topMargin, leftmargin, brickSize);
                            break;
                        }
                    case Keys.Left:
                        {
                            model.MoveLeft(topMargin, leftmargin, brickSize);
                            break;
                        }
                    case Keys.P:
                        {
                            nextState = new PausedState(controller);
                            //Enter Pause Logic
                            break;
                        }
                }
            }
        }
        public IState GetNextState() { return nextState; }
    }
}
