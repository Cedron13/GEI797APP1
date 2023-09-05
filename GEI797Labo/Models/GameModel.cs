using GEI797Labo.Controllers;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GEI797Labo.Models
{
    internal class GameModel
    {
        private IController controller;
        private Sprite player;
        private int TILE_SIZE;
        private GameView view;
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
                if (labyrinth[gridPosY, gridPosX + 1] == 1 || labyrinth[gridPosY, gridPosX + 1] == 2)
                {
                    return false;
                }
                else
                {
                    if (labyrinth[gridPosY, gridPosX] == 4 || labyrinth[gridPosY, gridPosX] == 5)
                    {
                        labyrinth[gridPosY, gridPosX] = 0;
                        counter++;
                        if (counter == 3)
                        {
                            labyrinth[4, 7] = 0;
                            controller.SetAllGemsIn(true);
                        }
                    }
                    gridPosX++;
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
                if (labyrinth[gridPosY, gridPosX - 1] == 1 || labyrinth[gridPosY, gridPosX - 1] == 2)
                {
                    return false;
                }
                else
                {
                    if (labyrinth[gridPosY, gridPosX] == 4 || labyrinth[gridPosY, gridPosX] == 5)
                    {
                        labyrinth[gridPosY, gridPosX] = 0;
                        counter++;
                        if (counter == 3)
                        {
                            labyrinth[4, 7] = 0;
                            controller.SetAllGemsIn(true);
                        }
                    }
                    gridPosX--;
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
                if (labyrinth[gridPosY - 1, gridPosX] == 1 || labyrinth[gridPosY - 1, gridPosX] == 2)
                {
                    return false;
                }
                else
                {
                    if (labyrinth[gridPosY, gridPosX] == 4 || labyrinth[gridPosY, gridPosX] == 5)
                    {
                        labyrinth[gridPosY, gridPosX] = 0;
                        counter++;
                        if (counter == 3)
                        {
                            labyrinth[4, 7] = 0;
                            controller.SetAllGemsIn(true);
                        }
                    }
                    gridPosY--;
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
                if (labyrinth[gridPosY + 1, gridPosX] == 1 || labyrinth[gridPosY + 1, gridPosX] == 2)
                {
                    return false;
                }
                else
                {
                    if (labyrinth[gridPosY, gridPosX] == 4 || labyrinth[gridPosY, gridPosX] == 5)
                    {
                        labyrinth[gridPosY, gridPosX] = 0;
                        counter++;
                        if (counter == 3)
                        {
                            labyrinth[4, 7] = 0;
                            controller.SetAllGemsIn(true);
                        }
                    }
                    gridPosY++;
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
            player = p;
        }

        public Sprite GetPlayer() { return player; }

    

    }
}
