using ExplorusE.Controllers;
using ExplorusE.Models.Commands;
using ExplorusE.Constants;
using System;
using System.Collections.Generic;
using System.Linq;

/* EXPLORUS-E
 * Alexis BLATRIX (blaa1406)
 * Cédric CHARRON (chac0902)
 * Audric DAVID (dava1302)
 * Matthieu JEHANNE (jehm1701)
 * Cloé LEGLISE (legc1001)
 */

namespace ExplorusE.Models
{
    internal class GameModel
    {
        private IControllerModel controller;
        private Sprite player;
        private coord gridPos;
        private int counter = 0;
        private int commandIndex = 0;
        private int[,] originalLabyrinthCopy;
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

        private List<IGameCommand> commandHistory = new List<IGameCommand>();

        public GameModel(IControllerModel c)
        {
            controller = c;
            InvokeCommand(new StartGameCommand());
            originalLabyrinthCopy = new int[labyrinth.GetLength(0), labyrinth.GetLength(1)];
            Array.Copy(labyrinth, originalLabyrinthCopy, labyrinth.Length);
        }

        public void SetGridPosX(int posX)
        {
            gridPos.x = posX;
        }

        public int GetGridPosX()
        {  
            return gridPos.x; 
        }

        public void SetGridPosY(int posY)
        {
            gridPos.y = posY;
        }

        public int GetGridPosY() => gridPos.y;

        public int GetCounter() => counter;

        public void SetCounter(int c) { counter = c; }

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

        public void InvokeCommand(IGameCommand command)
        {
            command.Execute(this);
            if(command.IsHistoryAction())
            {
                commandHistory.Add(command);
                commandIndex++;
            }
        }

        public void UndoLastCommand()
        {
            if (commandIndex>1)
            {
                commandHistory.ElementAt(commandIndex-1).Undo(this);
                commandIndex--;
            }
        }

        public void RedoNextCommand()
        {
            int len = commandHistory.Count;
            if(len > commandIndex)
            {
                commandIndex++;
                commandHistory.ElementAt(commandIndex-1).Execute(this);
            }
        }

        public void ClearAfterCurrentActionIndex()
        {
            commandHistory = commandHistory.GetRange(0, commandIndex);
        }

        public void ClearCommandHistory()
        {
            commandHistory = new List<IGameCommand>();
            commandIndex = 0;
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

        public Sprite GetPlayer() => player;

        public void SetLabyrinth(int[,] lab)
        {
            labyrinth = lab;
        }

        public IControllerModel GetController() { return controller; }
        
        public coord GetGridCoord() => gridPos;

        public void SetGridCoord(coord coord)
        {  
            gridPos = coord; 
        }

        public void EndLevel()
        {
            ResetLabyrinth();
            controller.NewLevel();
        }

        private void ResetLabyrinth()
        {
            Array.Copy(originalLabyrinthCopy, labyrinth, originalLabyrinthCopy.Length);
        }
    }
}

