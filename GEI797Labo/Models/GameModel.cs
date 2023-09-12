using GEI797Labo.Controllers;
using System.Windows.Input;
using GEI797Labo.Models.Commands;
using System;
using GEI797Labo.Observer;

/* EXPLORUS-E
 * Alexis BLATRIX (blaa1406)
 * Cédric CHARRON (chac0902)
 * Audric DAVID (dava1302)
 * Matthieu JEHANNE (jehm1701)
 * Cloé LEGLISE (legc1001)
 * Mahdi Majdoub (majm2404)
 */

namespace GEI797Labo.Models
{
    internal class GameModel
    {
        private IController controller;
        private Sprite player;
        private coord gridPos;
        private coord newPos;
        private int counter = 0;

        private IGameCommand[] commandHistory;

        public void SetGridPosX(int posX){
            gridPos.x = posX;
        }

        public int GetGridPosX(){  
            return gridPos.x; 
        }

        public void SetGridPosY(int posY){
            gridPos.y = posY;
        }

        public int GetGridPosY(){  
            return gridPos.y; }


        public GameModel(IController c) {

            controller = c;

        }

        public GameModel() { }
        
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

        private void GoTo(Direction d)
        {
            coord playerDestCoord = new coord()
            {
                x = gridPos.x,
                y = gridPos.y
            };
            player.StartMovement(playerDestCoord, d);
        }


        private void MakeMovement(Direction d)
        {
            if (player.IsMovementOver())
            {
                if (labyrinth[newPos.y, newPos.x] == 1)
                {
                    GoTo(d);
                }
                else if (labyrinth[newPos.y, newPos.x] == 2)
                {
                    if (counter == 3)
                    {
                        labyrinth[4, 7] = 0;
                        SetGridPosX(newPos.x);
                        SetGridPosY(newPos.y);
                        GoTo(d);
                    }
                    else
                    {
                        GoTo(d);
                    }
                }
                else
                {
                    if (labyrinth[newPos.y, newPos.x] == 4 || labyrinth[newPos.y, newPos.x] == 5)
                    {
                        labyrinth[newPos.y, newPos.x] = 0;
                        counter++;
                        controller.SetGemCounter(counter);

                        if (counter == 4)
                        {
                            controller.SetEndGame(true);
                        }
                    }
                    labyrinth[gridPos.y, gridPos.x] = 0;
                    SetGridPosX(newPos.x);
                    SetGridPosY(newPos.y);
                    labyrinth[gridPos.y, gridPos.x] = 3;
                    GoTo(d);
                }
            }
        }


        public void MoveRight()
        {
            newPos.x = gridPos.x + 1;
            newPos.y = gridPos.y;
            MakeMovement(Direction.RIGHT); 
        }
        public void MoveLeft()
        {
            newPos.x = gridPos.x - 1;
            newPos.y = gridPos.y;
            MakeMovement(Direction.LEFT);
        }

        public void MoveUp()
        {
            newPos.x = gridPos.x;
            newPos.y = gridPos.y - 1;
            MakeMovement(Direction.UP);
        }
        public void MoveDown()
        {
            newPos.x = gridPos.x;
            newPos.y = gridPos.y + 1;
            MakeMovement(Direction.DOWN);
        }


        public void InitPlayer(Sprite p)
        {
            if (player == null)
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

        public IResizeEventSubscriber GetPlayerResizeSub()
        {
            return player;
        }
    

    }
}
