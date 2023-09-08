using GEI797Labo.Controllers;

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
        private int newPosX;
        private int newPosY;
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


        private void GoTo(Direction d, int top, int left, int brick)
        {
            coord playerDestCoord = new coord()
            {
                x = left + brick * gridPosX,
                y = top + brick * (gridPosY + 1)
            };
            player.StartMovement(playerDestCoord, d);
        }


        private void MakeMovement(Direction d, int top, int left, int brick)
        {
            if (player.IsMovementOver())
            {
                if (labyrinth[newPosY, newPosX] == 1)
                {
                    GoTo(d, top, left, brick);
                }
                else if (labyrinth[newPosY, newPosX] == 2)
                {
                    if (counter == 3)
                    {
                        labyrinth[4, 7] = 0;
                        SetGridPosX(newPosX);
                        SetGridPosY(newPosY);
                        GoTo(d, top, left, brick);
                    }
                    else
                    {
                        GoTo(d, top, left, brick);
                    }
                }
                else
                {
                    if (labyrinth[newPosY, newPosX] == 4 || labyrinth[newPosY, newPosX] == 5)
                    {
                        labyrinth[newPosY, newPosX] = 0;
                        counter++;
                        controller.SetGemCounter(counter);

                        if (counter == 4)
                        {
                            controller.SetEndGame(true);
                        }
                    }
                    labyrinth[gridPosY, gridPosX] = 0;
                    SetGridPosX(newPosX);
                    SetGridPosY(newPosY);
                    labyrinth[gridPosY, gridPosX] = 3;
                    GoTo(d, top, left, brick);
                }
            }
        }


        public void MoveRight(int top, int left, int brick)
        {
            newPosX = gridPosX + 1;
            newPosY = gridPosY;
            MakeMovement(Direction.RIGHT, top, left, brick); 
        }
        public void MoveLeft(int top, int left, int brick)
        {
            newPosX = gridPosX - 1;
            newPosY = gridPosY;
            MakeMovement(Direction.LEFT, top, left, brick);
        }

        public void MoveUp(int top, int left, int brick)
        {
            newPosX = gridPosX;
            newPosY = gridPosY - 1;
            MakeMovement(Direction.UP, top, left, brick);
        }
        public void MoveDown(int top, int left, int brick)
        {
            newPosX = gridPosX;
            newPosY = gridPosY + 1;
            MakeMovement(Direction.DOWN, top, left, brick);
        }


        public void InitPlayer(Sprite p)
        {
            player = p;
        }

        public Sprite GetPlayer() { return player; }

    

    }
}
