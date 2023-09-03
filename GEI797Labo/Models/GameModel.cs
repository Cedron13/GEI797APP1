using GEI797Labo.Controllers;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace GEI797Labo.Models
{
    internal class GameModel
    {
        private IController controller;
        private Sprite player;
        private int TILE_SIZE;
        private GameView view;
        public GameModel(IController c) {

            controller = c;
            TILE_SIZE = 50; 
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

        public bool MoveRight()
        {
            coord playerDestCoord = player.GetPosition();
            playerDestCoord.x += TILE_SIZE;
            if (player.IsMovementOver()) player.StartMovement(playerDestCoord , Direction.RIGHT);
            return true;
        }
        public bool MoveLeft()
        {
            coord playerDestCoord = player.GetPosition();
            playerDestCoord.x -= TILE_SIZE;
            if (player.IsMovementOver()) player.StartMovement(playerDestCoord , Direction.LEFT);
            return true;
        }
        public bool MoveUp()
        {
            coord playerDestCoord = player.GetPosition();
            playerDestCoord.y -= TILE_SIZE;
            if (player.IsMovementOver()) player.StartMovement(playerDestCoord, Direction.UP);
            return true;
        }
        public bool MoveDown()
        {
            coord playerDestCoord = player.GetPosition();
            playerDestCoord.y += TILE_SIZE;
            if (player.IsMovementOver()) player.StartMovement(playerDestCoord, Direction.DOWN);
            return true;
        }


        public void InitPlayer(Sprite p)
        {
            player = p;
        }

        public Sprite GetPlayer() { return player; }

        private void GameForm_SizeChanged(object sender, EventArgs e)
        {
            TILE_SIZE = view.GetBrickSize();
        }

    }
}
