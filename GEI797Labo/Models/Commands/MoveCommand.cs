using GEI797Labo.Controllers;
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
        public MoveCommand(Direction d, coord initial, coord dest)
        {
            dir = d;
            initialPos = initial;
            newPos = dest;
        }
        //TODO: Add movement to history if there is no collision with wall
        public void Execute(GameModel model)
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
                if (labyrinth[newPos.y, newPos.x] == 4 || labyrinth[newPos.y, newPos.x] == 5)
                {
                    labyrinth[newPos.y, newPos.x] = 0;
                    model.SetCounter(model.GetCounter() + 1);
                    model.GetController().SetGemCounter(model.GetCounter());

                    if (model.GetCounter() == 4)
                    {
                        model.GetController().SetEndGame(true);
                    }
                }
                labyrinth[initialPos.y, initialPos.x] = 0;
                model.SetGridPosX(newPos.x);
                model.SetGridPosY(newPos.y);
                labyrinth[newPos.y, newPos.x] = 3;
                model.GoTo(dir, newPos);
            }
        }
        public void Undo(GameModel model) { }
    }
}
