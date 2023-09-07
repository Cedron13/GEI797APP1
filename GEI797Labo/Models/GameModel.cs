using GEI797Labo.Controllers;

/* EXPLORUS-E
 * Alexis BLATRIX (blaa1406)
 * Cédric CHARRON (chac0902)
 * Audric DAVID (dava1302)
 * Matthieu JEHANNE (jehm1701)
 * Cloé LEGLISE (legc1001)
 */

namespace GEI797Labo.Models
{
    internal class GameModel
    {
        private IController controller;
        private Sprite player;
        private int gridPosX;
        private int gridPosY;
        private int counter = 0;

        public void SetGridPosX(int posX){
            gridPosX = posX;
        }

        public int GetGridPosX(){  
            return gridPosX; 
        }

        public void SetGridPosY(int posY){
            gridPosY = posY;
        }

        public int GetGridPosY(){  
            return gridPosY; }


        public GameModel(IController c) {

            controller = c;

        }

        public GameModel(){}
        
        private int[,] labyrinth = {
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},  // 0 = nothing (free to go)
                {1, 0, 0, 4, 0, 0, 0, 0, 0, 0, 1},  // 1 = display wall
                {1, 0, 1, 1, 1, 0, 1, 1, 1, 0, 1},  // 2 = display door
                {1, 0, 0, 0, 0, 0, 1, 5, 1, 0, 1},  // 3 = display Slimus
                {1, 0, 1, 0, 1, 1, 1, 2, 1, 0, 1},  // 4 = display gem
                {1, 0, 1, 0, 1, 4, 0, 0, 0, 0, 1},  // 5 = display mini-slime
                {1, 0, 1, 0, 1, 1, 0, 1, 1, 0, 1},
                {1, 0, 0, 0, 3, 1, 0, 0, 4, 0, 1},
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                };
        


        public int[,] GetLabyrinth()
        {
            return labyrinth;
        }


        public void Update(double lag)
        {
            if (!player.IsMovementOver())
            {
                player.Update((int)lag);
            }
        }

        
        public bool MoveRight(int top, int left, int brick)
        {
            if (player.IsMovementOver())
            {
                if (labyrinth[gridPosY, gridPosX + 1] == 1)
                {
                    coord playerDestCoord = new coord()
                    {
                        x = left + brick * gridPosX,
                        y = top + brick * (gridPosY + 1)
                    };
                    player.StartMovement(playerDestCoord, Direction.RIGHT);
                    return true;
                }
                else if (labyrinth[gridPosY, gridPosX + 1] == 2)
                {
                    if (counter == 3)
                    {
                        labyrinth[4, 7] = 0;
                        gridPosX++;
                        coord playerDestCoord = new coord()
                        {
                            x = left + brick * gridPosX,
                            y = top + brick * (gridPosY + 1)
                        };
                        player.StartMovement(playerDestCoord, Direction.RIGHT);
                        return true;
                    }
                    else
                    {
                        coord playerDestCoord = new coord()
                        {
                            x = left + brick * gridPosX,
                            y = top + brick * (gridPosY + 1)
                        };
                        player.StartMovement(playerDestCoord, Direction.RIGHT);
                        return true;
                    }
                }
                else
                {
                    if (labyrinth[gridPosY, gridPosX] == 4 || labyrinth[gridPosY, gridPosX] == 5)
                    {
                        labyrinth[gridPosY, gridPosX] = 0;
                        counter++;
                        controller.SetGemCounter(counter);
                        
                        if (counter == 4)
                        {
                            controller.SetEndGame(true);
                        }
                    }
                    labyrinth[gridPosY, gridPosX] = 0;
                    gridPosX++;
                    labyrinth[gridPosY, gridPosX] = 3;
                    coord playerDestCoord = new coord()
                    {
                        x = left + brick * gridPosX,
                        y = top + brick * (gridPosY + 1)
                    };
                    player.StartMovement(playerDestCoord, Direction.RIGHT);
                    return true;
                }
            }
            return false;

        }
        public bool MoveLeft(int top, int left, int brick)
        {
            if (player.IsMovementOver())
            {
                if (labyrinth[gridPosY, gridPosX - 1] == 1)
                {
                    coord playerDestCoord = new coord()
                    {
                        x = left + brick * gridPosX,
                        y = top + brick * (gridPosY + 1)
                    };
                    player.StartMovement(playerDestCoord, Direction.LEFT);
                    return true;
                }
                else if (labyrinth[gridPosY, gridPosX - 1] == 2)
                {
                    if (counter == 3)
                    {
                        labyrinth[4, 7] = 0;
                        gridPosX--;
                        coord playerDestCoord = new coord()
                        {
                            x = left + brick * gridPosX,
                            y = top + brick * (gridPosY + 1)
                        };
                        player.StartMovement(playerDestCoord, Direction.LEFT);
                        return true;
                    }
                    else
                    {
                        coord playerDestCoord = new coord()
                        {
                            x = left + brick * gridPosX,
                            y = top + brick * (gridPosY + 1)
                        };
                        player.StartMovement(playerDestCoord, Direction.LEFT);
                        return true;
                    }
                }
                else
                {
                    if (labyrinth[gridPosY, gridPosX] == 4 || labyrinth[gridPosY, gridPosX] == 5)
                    {
                        labyrinth[gridPosY, gridPosX] = 0;
                        counter++;
                        controller.SetGemCounter(counter);
                        
                        if (counter == 4)
                        {
                            controller.SetEndGame(true);
                        }
                    }
                    labyrinth[gridPosY, gridPosX] = 0;
                    gridPosX--;
                    labyrinth[gridPosY, gridPosX] = 3;
                    coord playerDestCoord = new coord()
                    {
                        x = left + brick * gridPosX,
                        y = top + brick * (gridPosY + 1)
                    };
                    player.StartMovement(playerDestCoord, Direction.LEFT);
                    return true;
                }
            }
            return false;
        }
        public bool MoveUp(int top, int left, int brick)
        {
            if (player.IsMovementOver())
            {
                if (labyrinth[gridPosY - 1, gridPosX] == 1)
                {
                    coord playerDestCoord = new coord()
                    {
                        x = left + brick * gridPosX,
                        y = top + brick * (gridPosY + 1)
                    };
                    player.StartMovement(playerDestCoord, Direction.UP);
                    return true;
                }
                else if (labyrinth[gridPosY - 1, gridPosX] == 2)
                {
                    if (counter == 3)
                    {
                        labyrinth[4, 7] = 0;
                        gridPosY--;
                        coord playerDestCoord = new coord()
                        {
                            x = left + brick * gridPosX,
                            y = top + brick * (gridPosY + 1)
                        };
                        player.StartMovement(playerDestCoord, Direction.UP);
                        return true;
                    }
                    else
                    {
                        coord playerDestCoord = new coord()
                        {
                            x = left + brick * gridPosX,
                            y = top + brick * (gridPosY + 1)
                        };
                        player.StartMovement(playerDestCoord, Direction.UP);
                        return true;
                    }

                }
                else
                {
                    if (labyrinth[gridPosY, gridPosX] == 4 || labyrinth[gridPosY, gridPosX] == 5)
                    {
                        labyrinth[gridPosY, gridPosX] = 0;
                        counter++;
                        controller.SetGemCounter(counter);
                        
                        if (counter == 4)
                        {
                            controller.SetEndGame(true);
                        }
                    }
                    labyrinth[gridPosY, gridPosX] = 0;
                    gridPosY--;
                    labyrinth[gridPosY, gridPosX] = 3;
                    coord playerDestCoord = new coord()
                    {
                        x = left + brick * gridPosX,
                        y = top + brick * (gridPosY + 1)
                    };
                    player.StartMovement(playerDestCoord, Direction.UP);
                    return true;
                }
            }
            return false;
        }
        public bool MoveDown(int top, int left, int brick)
        {
            if (player.IsMovementOver())
            {
                if (labyrinth[gridPosY + 1, gridPosX] == 1)
                {
                    coord playerDestCoord = new coord()
                    {
                        x = left + brick * gridPosX,
                        y = top + brick * (gridPosY + 1)
                    };
                    player.StartMovement(playerDestCoord, Direction.DOWN);
                    return true;
                }
                else if (labyrinth[gridPosY + 1, gridPosX] == 2)
                {
                    if (counter == 3)
                    {
                        labyrinth[4, 7] = 0;
                        gridPosY++;
                        coord playerDestCoord = new coord()
                        {
                            x = left + brick * gridPosX,
                            y = top + brick * (gridPosY + 1)
                        };
                        player.StartMovement(playerDestCoord, Direction.DOWN);
                        return true;
                    }
                    else
                    {
                        coord playerDestCoord = new coord()
                        {
                            x = left + brick * gridPosX,
                            y = top + brick * (gridPosY + 1)
                        };
                        player.StartMovement(playerDestCoord, Direction.DOWN);
                        return true;
                    }
                }
                else
                {
                    if (labyrinth[gridPosY, gridPosX] == 4 || labyrinth[gridPosY, gridPosX] == 5)
                    {
                        labyrinth[gridPosY, gridPosX] = 0;
                        counter++;
                        controller.SetGemCounter(counter);
                        
                        if (counter == 4)
                        {
                            controller.SetEndGame(true);

                        }
                    }
                    labyrinth[gridPosY, gridPosX] = 0;
                    gridPosY++;
                    labyrinth[gridPosY, gridPosX] = 3;
                    coord playerDestCoord = new coord()
                    {
                        x = left + brick * gridPosX,
                        y = top + brick * (gridPosY + 1)
                    };
                    player.StartMovement(playerDestCoord, Direction.DOWN);
                    return true;
                }
            }
            return false;
        }


        public void InitPlayer(Sprite p)
        {
            if(player == null)
            {
                player = p;
            }
            else
            {
                p.SetDirection(player.GetDirection());
                player = p;
            }
            
        }

        public Sprite GetPlayer() { return player; }

    

    }
}
