﻿using ExplorusE.Controllers;
using ExplorusE.Models.Commands;
using ExplorusE.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Collections.Concurrent;
using ExplorusE.Threads;
using ExplorusE.Models.Sprites;
using ExplorusE.Controllers.States;
using System.Xml.Linq;
using System.Windows.Media;

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
        private ToxicSprite toxicTouche;
        private List<ToxicSprite> toxicSlimes;
        private List<BubbleSprite> bubbles;
        private List<GemSprite> gems;
        private bool isPaused = false;
        private coord gridPos;
        private int counter = 0;
        private int commandIndex = 0;
        private int playerLives = 3;
        private bool isAlreadyDead = false;
        private int[,] originalLabyrinthCopy;
        private bool isTouched = false;
        private bool undoMax = false;
        private int[,] labyrinth = {
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},  // 0 = nothing (free to go)
                {1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1},  // 1 = display wall
                {1, 0, 1, 1, 0, 1, 0, 1, 1, 1, 0, 1, 0, 1, 1, 0, 1},  // 2 = display door
                {1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1},  // 3 = display Slimus
                {1, 0, 1, 0, 1, 1, 1, 1, 0, 1, 1, 1, 1, 0, 1, 0, 1},  // 4 = display gem
                {1, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 1},  // 5 = display mini-slime
                {1, 0, 0, 0, 1, 0, 1, 1, 2, 1, 1, 0, 1, 0, 0, 0, 1},
                {1, 1, 1, 0, 0, 0, 1, 0, 5, 0, 1, 0, 0, 0, 1, 1, 1},
                {1, 0, 0, 0, 1, 0, 1, 1, 1, 1, 1, 0, 1, 0, 0, 0, 1},
                {1, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 1},
                {1, 0, 1, 0, 1, 1, 1, 1, 0, 1, 1, 1, 1, 0, 1, 0, 1},
                {1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1},
                {1, 0, 1, 1, 0, 1, 0, 1, 1, 1, 0, 1, 0, 1, 1, 0, 1},
                {1, 0, 0, 0, 0, 1, 0, 0, 3, 0, 0, 1, 0, 0, 0, 0, 1},
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}
                };

        private List<IGameCommand> commandHistory = new List<IGameCommand>();
        private readonly object lockSprites = new object();
        private readonly object lockCollision = new object();


        private IRenderQueueAsker queue;
        private RenderThread render;
        private AudioList audio; // 


        private bool doorUnlocked = false;
        private bool needupdate = false;

        public int GetPlayerLives()
        {
            return player.GetLives();
        }

        public GameModel(IControllerModel c, IRenderQueueAsker q, AudioList a)

        {
            controller = c;
            InvokeCommand(new StartGameCommand());
            originalLabyrinthCopy = new int[labyrinth.GetLength(0), labyrinth.GetLength(1)];
            Array.Copy(labyrinth, originalLabyrinthCopy, labyrinth.Length);
            toxicSlimes = new List<ToxicSprite>();
            bubbles = new List<BubbleSprite>();
            gems = new List<GemSprite>();
            queue = q;
            audio = a;
        }

        public void SetGridPosX(int posX)
        {
            gridPos.x = posX;
        }

        public void SetUndoMax(bool b)
        {
            undoMax = b;
        }
        public void SetIsPaused(bool p)
        {
            lock (lockSprites)
            {
                isPaused = p;
            }
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

        public void SetCounter(int c) {
            counter = c;
            controller.SetGemCounter(counter);
        }

        public int[,] GetLabyrinth()
        {
            return labyrinth;
        }

        public void SetDoorUnlocked(bool unlocked)
        {
            doorUnlocked = unlocked;
        }

        public bool GetDoorUnlocked() => doorUnlocked;
       
        public bool GetIsAlreadyDead()
        {
            return isAlreadyDead;
        }
        public void SetIsAlreadyDead(bool b)
        {
            isAlreadyDead = b;
        }

        public void Update(double lag)
        {
            lock (lockSprites)
            {
                if (!player.IsMovementOver() || isTouched==true)
                {
                    player.Update((int)lag);
                    isTouched = false;
                    
                }
                if (controller.GetFlashPlayer()) // If we are invinsible, we altern between hide and appear
                {
                    player.SetTransparency(175);
                    queue.AskForNewItem(player, RenderItemType.NonPermanent);
                }
                else if (!controller.GetFlashPlayer())
                {
                    player.SetTransparency(0);
                    queue.AskForNewItem(player, RenderItemType.NonPermanent);
                }
                if (bubbles.Count > 0)
                {
                    foreach (BubbleSprite element in bubbles)
                    {
                        if (element.IsMovementOver())
                        {   
                            NextBubbleMovement(element);
                        }
                        element.Update((int)lag);
                        
                        queue.AskForNewItem(element, RenderItemType.NonPermanent);
                        if(element.IsDestroyed())
                        {
                            HandleDestroyedBubble(element);
                        }
                    }

                    bubbles.RemoveAll(element => element.IsDestroyed());
                }

                foreach (GemSprite gem in gems)
                {
                    queue.AskForNewItem(gem, RenderItemType.NonPermanent);
                }

                foreach (ToxicSprite slime in toxicSlimes)
                {
                    if (slime.IsMovementOver())
                    {
                        
                        NextToxicMovement(slime);
                    }
                    slime.Update((int)lag);

                    

                    if ((controller.GetFlashToxic() && slime == toxicTouche))
                    {
                        toxicTouche.SetTransparency(175);
                        queue.AskForNewItem(slime, RenderItemType.NonPermanent);
                    }
                    else
                    {
                        slime.SetTransparency(0);
                        queue.AskForNewItem(slime, RenderItemType.NonPermanent);
                    }
                                     
                }

                toxicSlimes.RemoveAll(element => !element.IsAlive());
            }

        }
        private void ToxicBubbleCollision(ToxicSprite tox, BubbleSprite b)
        {
            if (!b.IsExploded())
            {
                controller.SetIsFlashingToxic(true);
                controller.SetFlashToxicTimer(0);
                toxicTouche = tox;
                InvokeCommand(new LoseLifeCommand(tox));
                if (!tox.IsAlive())
                {
                    foreach (ToxicSprite slime in toxicSlimes)
                    {
                        slime.IncreaseSpeed();
                    }
                    coordF toxPos = tox.GetGridPosition();
                    coord gemCoord = new coord()
                    {
                        x = (int)toxPos.x,
                        y = (int)toxPos.y
                    };
                    GemSprite gem = new GemSprite(gemCoord, tox.GetActualTop(), tox.GetActualLeft(), tox.GetActualBricksize(), tox.GetName(), 0.5f);
                    gem.StartMovement(gemCoord, Direction.DOWN);
                    gems.Add(gem);
                    InvokeCommand(new DestroySpriteCommand(tox));
                    InvokeCommand(new DestroySpriteCommand(b));
                }
                
            }
            b.Explode();
            //b.Destroy();
        }
        private void ToxicPlayerCollision(ToxicSprite tox)
        {
            isTouched = true;
            if (!player.IsInvincible())
            {
                InvokeCommand(new LoseLifeCommand(player));
                player.SetInvincible();
                controller.SetIsInvincible(true);
                controller.SetInvincibleTimer(0);
                controller.SetFlashPlayer(true);
                playerLives = player.GetLives();
                audio.Add("CollisionPlayerToxic.wav");

                if (playerLives == 0 && !isAlreadyDead)
                {
                    controller.IsDying();
                    controller.IsDeadOnce = true;
                    isAlreadyDead = true;
                    isPaused = true;
                    audio.Add("CollisionPlayerToxic.wav");
                    //UndoLastCommand();
                }
                else if (playerLives == 0 && isAlreadyDead) 
                {
                    controller.IsDeadTwice = true;
                    controller.IsDying();
                    audio.Add("CollisionPlayerToxic.wav");
                }
            }
        }

        public AudioList GetAudioList()
        {
            return audio;
        }
        private void PlayerGemCollision(GemSprite gem)
        {
            InvokeCommand(new GemPickedUpCommand());
            InvokeCommand(new DestroySpriteCommand(gem));
            gem.Destroy();
            audio.Add("GemCollected.wav");

        }

        public void RemoveGemForToxic(string toxicName)
        {
            lock(lockSprites)
            {
                gems.RemoveAll(element => element.GetOrigin() == toxicName);
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
            if (commandIndex>1 &&(!undoMax))
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
                undoMax = false;
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

        public void InitToxicSlime(ToxicSprite tox, String name)
        {
            tox.setName(name);
            toxicSlimes.Add(tox);
        }

        public void AddBubble(BubbleSprite bubble)
        {
            //BubbleSprite bubble = new BubbleSprite(initialPos,top,left,brick);
            
            bubbles.Add(bubble);
        }
        public void AddToxic(ToxicSprite tox)
        {
            toxicSlimes.Add(tox);
        }
        public void AddGem(GemSprite gem)
        {
            gems.Add(gem);
        }
        public void RemoveBubble(BubbleSprite bubble)
        {
            //BubbleSprite bubble = new BubbleSprite(initialPos,top,left,brick);

            bubbles.Remove(bubble);
        }
        public void RemoveToxic(ToxicSprite tox)
        {
            toxicSlimes.Remove(tox);
        }
        public void RemoveGem(GemSprite gem)
        {
            gems.Remove(gem);
        }
        public List<BubbleSprite> GetBubbles()
        {
            return bubbles;
        }


        public PlayerSprite GetPlayer()
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
                controller.InitGame();
                player.SetLives(3); // Reset de la barre de vie
                doorUnlocked = false;
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
                return false;
            }
            return true;
        }

        public void checkCollision()
        {
            lock (lockSprites)
            {   
                if(!isPaused)
                {
                    foreach (ToxicSprite toxSlime in toxicSlimes)
                    {
                        if (IsCollision(player, toxSlime))
                        {
                            ToxicPlayerCollision(toxSlime);
                        }
                        foreach (BubbleSprite bubble in bubbles)
                        {
                            if (IsCollision(toxSlime, bubble))
                            {

                                ToxicBubbleCollision(toxSlime, bubble);

                            }
                        }
                    }
                    bubbles.RemoveAll(element => element.IsDestroyed());
                    toxicSlimes.RemoveAll(element => !element.IsAlive());

                    foreach (GemSprite gem in gems)
                    {
                        if (IsCollision(player, gem))
                        {
                            PlayerGemCollision(gem);
                        }
                    }
                    gems.RemoveAll(element => element.IsDestroyed());
                }
            }
        }
        private void NextToxicMovement(ToxicSprite tox)
        {
            if (controller.GetState() is PlayState) // STOP THE TOXIC SLIME IF WE ARE NOT IN PLAY STATE (pour gérer undo et redo penser à ajouter variable en +)
            {
                ToxicMoveCommand c = new ToxicMoveCommand(tox);
                InvokeCommand(c);
            }
            
        }
        private void NextBubbleMovement(BubbleSprite bubble)
        {
            if (!bubble.IsDestroyed() && !bubble.IsExploded() && controller.GetState() is PlayState) // STOP THE TOXIC SLIME IF WE ARE NOT IN PLAY STATE (pour gérer undo et redo penser à ajouter variable en +)
            {
                BubbleMoveCommand c = new BubbleMoveCommand(bubble);
                InvokeCommand(c);
            }
        }
        private void HandleDestroyedBubble(BubbleSprite bubble)
        {
            if (bubble.IsDestroyed() && controller.GetState() is PlayState) // STOP THE TOXIC SLIME IF WE ARE NOT IN PLAY STATE (pour gérer undo et redo penser à ajouter variable en +)
            {
                InvokeCommand(new DestroySpriteCommand(bubble));
            }
        }

    }
}