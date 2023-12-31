﻿using ExplorusE.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

/* EXPLORUS-E
 * Alexis BLATRIX (blaa1406)
 * Cédric CHARRON (chac0902)
 * Audric DAVID (dava1302)
 * Matthieu JEHANNE (jehm1701)
 * Cloé LEGLISE (legc1001)
 */

namespace ExplorusE.Models.Commands
{
    internal class ToxicMoveCommand : IGameCommand
    {
        private Direction dir;
        private Direction newDir;
        private coord initialPos;
        private coord newPos;
        private ToxicSprite toxicSprite;
        private static Random rnd = new Random();
        private bool isWall = false;
        private bool firstRun = true;


        public ToxicMoveCommand(ToxicSprite tox)
        {
            dir = tox.GetDirection();
            newDir = dir;
            coordF d = tox.GetGridPosition();
            initialPos = new coord() {
                x = (int)d.x,
                y = (int)d.y,
            };
            toxicSprite = tox;
        }

        //TODO: Add movement to history if there is no collision with wall
        public void Execute(GameModel model)
        {
            if (toxicSprite.IsMovementOver() && firstRun)
            {
                int[,] labyrinth = model.GetLabyrinth();
                newPos = GetCoordFromDir(dir, initialPos);
                if(labyrinth[newPos.y, newPos.x] == 0 || labyrinth[newPos.y, newPos.x] == 3)
                {
                    isWall = false;
                }
                else
                {
                    isWall = true;
                }
                List<Direction> possibleDirs = EvaluatePossibleDirections(labyrinth, initialPos);
                newDir = possibleDirs.ElementAt(rnd.Next(possibleDirs.Count));
                newPos = GetCoordFromDir(newDir, initialPos);
                toxicSprite.StartMovement(newPos, newDir);
                firstRun = false;
            }
            else if(firstRun)
            {

            }
            else
            {
                toxicSprite.StartMovement(newPos, newDir);
            }

        }
        public void Undo(GameModel model)
        {
            toxicSprite.StartMovement(initialPos, newDir);
        }
        public bool IsHistoryAction() => true;

        private List<Direction> EvaluatePossibleDirections(int[,] lab, coord initialCoord)
        {
            List<Direction> result = new List<Direction>();
            coord tempCoord = GetCoordFromDir(Direction.DOWN, initialCoord);
            if (lab[tempCoord.y, tempCoord.x] == 0 || lab[tempCoord.y, tempCoord.x] == 3)
            {
                if(this.dir != Direction.UP || isWall)
                {
                    result.Add(Direction.DOWN);
                }
            }
            tempCoord = GetCoordFromDir(Direction.UP, initialCoord);
            if (lab[tempCoord.y, tempCoord.x] == 0 || lab[tempCoord.y, tempCoord.x] == 3)
            {
                if (this.dir != Direction.DOWN || isWall)
                {
                    result.Add(Direction.UP);
                }
            }
            tempCoord = GetCoordFromDir(Direction.RIGHT, initialCoord);
            if (lab[tempCoord.y, tempCoord.x] == 0 || lab[tempCoord.y, tempCoord.x] == 3)
            {
                if (this.dir != Direction.LEFT || isWall)
                {
                    result.Add(Direction.RIGHT);
                }
            }
            tempCoord = GetCoordFromDir(Direction.LEFT, initialCoord);
            if (lab[tempCoord.y, tempCoord.x] == 0 || lab[tempCoord.y, tempCoord.x] == 3)
            {
                if (this.dir != Direction.RIGHT || isWall)
                {
                    result.Add(Direction.LEFT);
                }
            }
            return result;
        }
        private coord GetCoordFromDir(Direction d, coord initialCoord) {
            coord dest = new coord()
            {
                x= 0,
                y = 0
            };
            switch (d)
            {
                case Direction.DOWN:
                    {
                        dest = new coord()
                        {
                            x = initialCoord.x,
                            y = initialCoord.y + 1
                        };
                        break;
                    }
                case Direction.UP:
                    {
                        dest = new coord()
                        {
                            x = initialCoord.x,
                            y = initialCoord.y - 1
                        };
                        break;
                    }
                case Direction.RIGHT:
                    {
                        dest = new coord()
                        {
                            x = initialCoord.x + 1,
                            y = initialCoord.y
                        };
                        break;
                    }
                case Direction.LEFT:
                    {
                        dest = new coord()
                        {
                            x = initialCoord.x - 1,
                            y = initialCoord.y
                        };
                        break;
                    }
            }
            return dest;
        }
        
    }
}
