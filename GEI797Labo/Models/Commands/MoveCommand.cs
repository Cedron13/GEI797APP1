﻿using GEI797Labo.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GEI797Labo.Models.Commands
{
    internal class MoveCommand : IGameCommand
    {
        private Direction dir;
        private coord initialPos;
        private coord newPos;
        private bool gemFound = false;
        private bool doorUnlocked = false;
        private bool isEndGame = false;
        private bool isHistoryAction = false;
        public MoveCommand(Direction d, coord initial, coord dest)
        {
            dir = d;
            initialPos = initial;
            newPos = dest;
        }
        //TODO: Add movement to history if there is no collision with wall
        public void Execute(GameModel model)
        {
            if (model.GetPlayer().IsMovementOver())
            {
                int[,] labyrinth = model.GetLabyrinth();

                if (labyrinth[newPos.y, newPos.x] == 1)
                {
                    model.GoTo(dir, initialPos);
                }
                else if (labyrinth[newPos.y, newPos.x] == 2)
                {
                    if (model.GetCounter() == 3)
                    {
                        doorUnlocked = true;
                        isHistoryAction = true;
                        labyrinth[4, 7] = 0;
                        model.SetGridPosX(newPos.x);
                        model.SetGridPosY(newPos.y);
                        model.GoTo(dir, newPos); 
                    }
                    else
                    {
                        model.GoTo(dir, initialPos);
                    }
                }
                else
                {
                    isHistoryAction = true;
                    if (labyrinth[newPos.y, newPos.x] == 4 || labyrinth[newPos.y, newPos.x] == 5)
                    {
                        labyrinth[newPos.y, newPos.x] = 0;
                        gemFound = true;
                        model.SetCounter(model.GetCounter() + 1);
                        model.GetController().SetGemCounter(model.GetCounter());

                        if (model.GetCounter() == 4)
                        {
                            model.GetController().SetEndGame(true);
                            isEndGame = true;
                        }
                    }
                    labyrinth[initialPos.y, initialPos.x] = 0;
                    model.SetGridPosX(newPos.x);
                    model.SetGridPosY(newPos.y);
                    labyrinth[newPos.y, newPos.x] = 3;
                    model.GoTo(dir, newPos);
                }
            }
        }
        public void Undo(GameModel model) {
            
            int[,] labyrinth = model.GetLabyrinth();
            if(gemFound)
            {
                labyrinth[newPos.y, newPos.x] = 4;
                model.SetCounter(model.GetCounter() - 1);
                model.GetController().SetGemCounter(model.GetCounter());
                if(isEndGame)
                {
                    model.GetController().SetEndGame(false);
                    isEndGame = false;
                }
                gemFound = false;
            }
            else
            {
                labyrinth[newPos.y, newPos.x] = 0;
            }

            if (doorUnlocked)
            {
                labyrinth[4, 7] = 2;
            }
            

            labyrinth[initialPos.y, initialPos.x] = 3;
            model.SetGridPosX(initialPos.x);
            model.SetGridPosY(initialPos.y);
            model.GoTo(dir, initialPos);
        }
        public bool IsHistoryAction()
        {
            return isHistoryAction;
        }
    }
}
