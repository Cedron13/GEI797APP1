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
        private PlayerSprite player;
        private List<ToxicSprite> toxicSlimes;
        private List<BubbleSprite> bubbles = new List<BubbleSprite>();       
        private List<GemSprite> gems;
        private coord gridPos;
        private int counter = 0;
        private int commandIndex = 0;
        private int playerLives = 3;
        private int[,] originalLabyrinthCopy;
        private int[,] labyrinth = {
                 {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},  // 0 = nothing (free to go)
                {1, 0, 0, 0, 0, 1, 0, 0, 4, 0, 0, 1, 0, 0, 0, 0, 1},  // 1 = display wall
                {1, 0, 1, 1, 0, 1, 0, 1, 1, 1, 0, 1, 0, 1, 1, 0, 1},  // 2 = display door
                {1, 4, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 4, 1},  // 3 = display Slimus
                {1, 0, 1, 0, 1, 1, 1, 1, 0, 1, 1, 1, 1, 0, 1, 0, 1},  // 4 = display gem
                {1, 0, 1, 0, 1, 0, 0, 0, 4, 0, 0, 0, 1, 0, 1, 0, 1},  // 5 = display mini-slime
                {1, 0, 0, 0, 1, 0, 1, 1, 2, 1, 1, 0, 1, 0, 0, 0, 1},
                {1, 1, 1, 0, 0, 0, 1, 0, 5, 0, 1, 0, 0, 0, 1, 1, 1},
                {1, 0, 0, 0, 1, 0, 1, 1, 1, 1, 1, 0, 1, 0, 0, 0, 1},
                {1, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 1},
                {1, 0, 1, 0, 1, 1, 1, 1, 0, 1, 1, 1, 1, 0, 1, 0, 1},
                {1, 4, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 4, 1},
                {1, 0, 1, 1, 0, 1, 0, 1, 1, 1, 0, 1, 0, 1, 1, 0, 1},
                {1, 0, 0, 0, 0, 1, 0, 0, 3, 0, 0, 1, 0, 0, 0, 0, 1},
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}
                };

        private List<IGameCommand> commandHistory = new List<IGameCommand>();
        private readonly object lockSprites = new object();
        private readonly object lockCollision = new object();

        public int GetPlayerLives()
        {
            return playerLives;
        }

        public GameModel(IControllerModel c)
        {
            controller = c;
            InvokeCommand(new StartGameCommand());
            originalLabyrinthCopy = new int[labyrinth.GetLength(0), labyrinth.GetLength(1)];
            Array.Copy(labyrinth, originalLabyrinthCopy, labyrinth.Length);
            toxicSlimes = new List<ToxicSprite>();
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
            if (bubbles.Count > 0 && !bubbles[0].IsMovementOver())
            {
                foreach (BubbleSprite element in bubbles)
                {
                    element.Update((int)lag);
                }
                //bubbles[0].Update((int)lag);
            }
            else if (bubbles.Count > 0 && bubbles[0].IsMovementOver())
            {
                bubbles.Remove(bubbles[0]);
            }
            foreach (Sprite slime in toxicSlimes)
            {
                if (!slime.IsMovementOver())
                {
                    slime.Update((int)lag);
                }
            }
        }
        private void ToxicBubbleCollision(ToxicSprite tox, BubbleSprite b)
        {
            bool death = tox.LoseLife();
            if(death)
            {
                toxicSlimes.Remove(tox);
                foreach(ToxicSprite slime in toxicSlimes)
                {
                    slime.IncreaseSpeed();
                }
                // create new gem 
            }
            bubbles.Remove(b);
        }
        private void ToxicPlayerCollision(ToxicSprite tox)
        {
            if(!player.IsInvincible())
            {
                player.LoseLife();
                player.SetInvincible();
                playerLives--;
            }
        }
        private void PlayerGemCollision(GemSprite gem)
        {
            //ADD GEM COUNTER
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

        public void InitPlayer(PlayerSprite p)
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
            ToxicSprite toxicSlime = new ToxicSprite(initialPos, top, left, brick);
            toxicSlime.setName(name);
            toxicSlimes.Add(toxicSlime);
        }

        public void AddBubble(BubbleSprite bubble)
        {
            //BubbleSprite bubble = new BubbleSprite(initialPos,top,left,brick);
            bubbles.Add(bubble);
        }
        public List<BubbleSprite> GetBubbles()
        {
            return bubbles;
        }


        public Sprite GetPlayer()
        {
            lock (lockSprites)
            {
                return player;
            }
        }
        public ConcurrentBag<ToxicSprite> GetToxicSlimes()
        {
            lock (lockSprites)
            {
                return new ConcurrentBag<ToxicSprite>(toxicSlimes); ;
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

        private void ResetLabyrinth()
        {
            Array.Copy(originalLabyrinthCopy, labyrinth, originalLabyrinthCopy.Length);
        }

        private bool IsCollision(Sprite s1, Sprite s2)
        {
            double r1 = s1.GetBoundingRadius();
            double r2 = s2.GetBoundingRadius();
            coordF c1 = s1.GetGridPosition();
            coordF c2 = s2.GetGridPosition();
            double d = Math.Sqrt(Math.Pow(c2.x - c1.x, 2) + Math.Pow(c2.y - c1.y, 2));
            if (r1 + r2 < d)
            {
                return true;
            }
            return false;
        }

        public void checkCollision()
        {
            lock (lockSprites)
            {
                foreach (ToxicSprite toxSlime in toxicSlimes)
                {
                    if(IsCollision(player, toxSlime))
                    {
                        ToxicPlayerCollision(toxSlime);
                    }
                    foreach (BubbleSprite bubble in bubbles)
                    {
                        if(IsCollision(toxSlime, bubble))
                        {
                            ToxicBubbleCollision(toxSlime, bubble);
                        }    
                    }
                }
                foreach(GemSprite gem in gems)
                {
                    if(IsCollision(player, gem))
                    {
                        PlayerGemCollision(gem);
                    }
                }
            }
        }

    }
}