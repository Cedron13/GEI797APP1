using ExplorusE.Constants;
using ExplorusE.Controllers;
using System.Reflection;
using System.Security.Cryptography;

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
        public void Execute(GameModel model, bool isPlayer)
        {
            if (isPlayer)
            {
                if (model.GetPlayer().IsMovementOver())
                {
                    int[,] labyrinth = model.GetLabyrinth();

                    if (labyrinth[newPos.y, newPos.x] == 1)
                    {
                        model.PlayerGoTo(dir, initialPos);
                    }
                    else if (labyrinth[newPos.y, newPos.x] == 2)
                    {
                        if (model.GetCounter() == 6)
                        {
                            doorUnlocked = true;
                            isHistoryAction = true;
                            labyrinth[newPos.x, newPos.y] = 0;
                            model.SetGridPosX(newPos.x);
                            model.SetGridPosY(newPos.y);
                            model.PlayerGoTo(dir, newPos);
                        }
                        else
                        {
                            model.PlayerGoTo(dir, initialPos);
                        }

                    }
                    else
                    {
                        isHistoryAction = true;
                        if (labyrinth[newPos.y, newPos.x] == 4)
                        {
                            // labyrinth[newPos.y, newPos.x] = 0;
                            // gemFound = true;
                            // model.SetCounter(model.GetCounter() + 1);
                            // model.GetController().SetGemCounter(model.GetCounter());

                            // labyrinth[initialPos.y, initialPos.x] = 0;²
                            model.SetGridPosX(newPos.x);
                            model.SetGridPosY(newPos.y);
                            // labyrinth[newPos.y, newPos.x] = 3;
                            model.PlayerGoTo(dir, newPos);

                        }

                        else if (labyrinth[newPos.y, newPos.x] == 5)
                        {
                            labyrinth[initialPos.y, initialPos.x] = 0;
                            model.SetGridPosX(newPos.x);
                            model.SetGridPosY(newPos.y);
                            labyrinth[newPos.y, newPos.x] = 3;
                            model.PlayerGoTo(dir, newPos);

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
                            model.PlayerGoTo(dir, newPos);
                        }
                    }
                }
            }
            else
            {
                int[,] labyrinth = model.GetLabyrinth();
                foreach (ToxicSprite t in model.GetToxicSlimes())
                {
                    coord initCoord = t.GetGridPositionC();  

                    if(t.GetDirection() == Direction.UP)
                    {
                        coord toxDest = new coord { x = initCoord.x, y = initCoord.y - 1 };
                        if (labyrinth[toxDest.y, toxDest.x] != 1 && labyrinth[toxDest.y, toxDest.x] != 2)
                        {
/*                            model.SetGridPosX(newPos.x);
                            model.SetGridPosY(newPos.y);*/
                            model.ToxicGoTo(t, Direction.UP, toxDest);
                        }
                        else
                        {

                        }

                    }
                    if (t.GetDirection() == Direction.DOWN)
                    {
                        coord toxDest = new coord { x = initCoord.x, y = initCoord.y + 1 };
                        if (labyrinth[toxDest.y, toxDest.x] != 1 && labyrinth[toxDest.y, toxDest.x] != 2)
                        {
                            /*                            model.SetGridPosX(newPos.x);
                                                        model.SetGridPosY(newPos.y);*/
                            model.ToxicGoTo(t, Direction.DOWN, toxDest);
                        }
                        else
                        {

                        }

                    }
                    if (t.GetDirection() == Direction.LEFT)
                    {
                        coord toxDest = new coord { x = initCoord.x - 1, y = initCoord.y };
                        if (labyrinth[toxDest.y, toxDest.x] != 1 && labyrinth[toxDest.y, toxDest.x] != 2)
                        {
                            /*                            model.SetGridPosX(newPos.x);
                                                        model.SetGridPosY(newPos.y);*/
                            model.ToxicGoTo(t, Direction.LEFT, toxDest);
                        }
                        else
                        {

                        }

                    }
                    if (t.GetDirection() == Direction.RIGHT)
                    {
                        coord toxDest = new coord { x = initCoord.x + 1, y = initCoord.y };
                        if (labyrinth[toxDest.y, toxDest.x] != 1 && labyrinth[toxDest.y, toxDest.x] != 2)
                        {
                            /*                            model.SetGridPosX(newPos.x);
                                                        model.SetGridPosY(newPos.y);*/
                            model.ToxicGoTo(t, Direction.RIGHT, toxDest);
                        }
                        else
                        {

                        }

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
            model.PlayerGoTo(dir, initialPos);
        }
        public bool IsHistoryAction()=> isHistoryAction;

    }
}
