using ExplorusE.Constants;

/* EXPLORUS-E
 * Alexis BLATRIX (blaa1406)
 * Cédric CHARRON (chac0902)
 * Audric DAVID (dava1302)
 * Matthieu JEHANNE (jehm1701)
 * Cloé LEGLISE (legc1001)
 */

namespace ExplorusE.Models.Commands
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
                    if (model.GetCounter() == 6)
                    {
                        doorUnlocked = true;
                        isHistoryAction = true;
                        labyrinth[newPos.y, newPos.x] = 0; // TODO : automatically locate (coordX coordY)
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
                    if (labyrinth[newPos.y, newPos.x] == 4)
                    {
                        labyrinth[newPos.y, newPos.x] = 0;
                        gemFound = true;
                        model.SetCounter(model.GetCounter() + 1);
                        model.GetController().SetGemCounter(model.GetCounter());

                        labyrinth[initialPos.y, initialPos.x] = 0;
                        model.SetGridPosX(newPos.x);
                        model.SetGridPosY(newPos.y);
                        labyrinth[newPos.y, newPos.x] = 3;
                        model.GoTo(dir, newPos);

                    }

                    else if (labyrinth[newPos.y, newPos.x] == 5)
                    {
                        labyrinth[initialPos.y, initialPos.x] = 0;
                        model.SetGridPosX(newPos.x);
                        model.SetGridPosY(newPos.y);
                        labyrinth[newPos.y, newPos.x] = 3;
                        model.GoTo(dir, newPos);

                        labyrinth[newPos.y, newPos.x] = 0;
                        model.SetCounter(0);
                        isEndGame = true;
                        model.EndLevel();
                    }
                    else
                    {
                        labyrinth[initialPos.y, initialPos.x] = 0;
                        model.SetGridPosX(newPos.x);
                        model.SetGridPosY(newPos.y);
                        labyrinth[newPos.y, newPos.x] = 3;
                        model.GoTo(dir, newPos);
                    }
                }
            }
        }
        public void Undo(GameModel model)
        {
            int[,] labyrinth = model.GetLabyrinth();
            if (gemFound)
            {
                labyrinth[newPos.y, newPos.x] = 4;
                model.SetCounter(model.GetCounter() - 1);
                model.GetController().SetGemCounter(model.GetCounter());
                if (isEndGame)
                {
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
        public bool IsHistoryAction()=> isHistoryAction;

    }
}
