﻿using ExplorusE.Constants;
using ExplorusE.Models.Commands;
using ExplorusE.Models;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Runtime.Remoting.Channels;

/* EXPLORUS-E
 * Alexis BLATRIX (blaa1406)
 * Cédric CHARRON (chac0902)
 * Audric DAVID (dava1302)
 * Matthieu JEHANNE (jehm1701)
 * Cloé LEGLISE (legc1001)
 */

namespace ExplorusE.Controllers.States
{
    internal class ResumeState : IState
    {
        IState nextState = null;
        IControllerModel controller;
        public ResumeState(IControllerModel c)
        {
            nextState = this;
            controller = c;
        }

        public void ProcessInput(List<Keys> keys)
        {
            foreach (Keys e in keys)
            {
                switch (e)
                {
                    case Keys.F:
                        {
                            controller.ChangeFpsDisplay();
                            break;
                        }
                }
            }
        }

        public IState GetNextState() => nextState;

        public void PrepareNextState(GameStates state = GameStates.PLAY) //Default next state is PLAY
        {
            if (state == GameStates.UNKNOWN) state = GameStates.PLAY; //If the method is called from the interface IState
            switch (state)
            {
                //List here the possible output states
                case GameStates.PLAY: nextState = new PlayState(controller); break;
                case GameStates.PAUSE: nextState = new PausedState(controller); break;
                default: break;
            }
        }
    }
}
