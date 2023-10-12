using ExplorusE.Controllers;
using ExplorusE.Models.Commands;
using ExplorusE.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Collections.Concurrent;

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
        private ConcurrentBag<Sprite> toxicSlimes;
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
        private readonly object lockSprites = new object();

        public GameModel(IControllerModel c)
        {
            controller = c;
            InvokeCommand(new StartGameCommand());
            originalLabyrinthCopy = new int[labyrinth.GetLength(0), labyrinth.GetLength(1)];
            Array.Copy(labyrinth, originalLabyrinthCopy, labyrinth.Length);
            toxicSlimes = new ConcurrentBag<Sprite>();
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
            foreach(Sprite slime in toxicSlimes)
            {
                if (!slime.IsMovementOver())
                {
                    slime.Update((int)lag);
                }
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
        public void InitToxicSlime(coord initialPos,string name, int top, int left, int brick)
        {
            Sprite toxicSlime = new Sprite(initialPos, top, left, brick);
            toxicSlime.setName(name);
            toxicSlimes.Add(toxicSlime);
        }
        public Sprite GetPlayer()
        {
            lock (lockSprites)
            {
                return player;
            }
        }

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
            if(controller.NewLevel() != 3)
            {
                ResetLabyrinth();
            }
            else
            {
                controller.EndGameReached();
            }
            
        }
        public ConcurrentBag<Sprite> GetToxicSlimes()
        {
            lock(lockSprites)
            {
                return toxicSlimes;
            }
        }

        private void ResetLabyrinth()
        {
            Array.Copy(originalLabyrinthCopy, labyrinth, originalLabyrinthCopy.Length);
        }

        public void checkCollision()
        {
            foreach(Sprite toxSlime  in toxicSlimes)
            {
                //Verify if they collide with Slimus
            }
        }
    }
}

