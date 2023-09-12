using GEI797Labo.Controllers;
using System.Windows.Input;
using GEI797Labo.Models.Commands;
using System;
using GEI797Labo.Observer;
using System.Collections.Generic;

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

        private List<IGameCommand> commandHistory = new List<IGameCommand>();

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

        public int GetCounter() => counter;
        public void SetCounter(int c) { counter = c; }

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

        public void GoTo(Direction d, coord dest)
        {
            player.StartMovement(dest, d);
        }

        public void MoveRight()
        {
            newPos.x = gridPos.x + 1;
            newPos.y = gridPos.y;
            MoveCommand com = new MoveCommand(Direction.RIGHT, gridPos, newPos);
            com.Execute(this);

        }
        public void MoveLeft()
        {
            newPos.x = gridPos.x - 1;
            newPos.y = gridPos.y;
            MoveCommand com = new MoveCommand(Direction.LEFT, gridPos, newPos);
            com.Execute(this);
        }

        public void MoveUp()
        {
            newPos.x = gridPos.x;
            newPos.y = gridPos.y - 1;
            MoveCommand com = new MoveCommand(Direction.UP, gridPos, newPos);
            com.Execute(this);
        }
        public void MoveDown()
        {
            newPos.x = gridPos.x;
            newPos.y = gridPos.y + 1;
            MoveCommand com = new MoveCommand(Direction.DOWN, gridPos, newPos);
            com.Execute(this);
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

        public IController GetController() { return controller; }

        public IResizeEventSubscriber GetPlayerResizeSub()
        {
            return player;
        }
    

    }
}
