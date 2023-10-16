using ExplorusE.Constants;
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
/*
namespace ExplorusE.Models.Commands
{
    internal class BubbleMoveCommand : IGameCommand
    {
        private Direction dir;
        private coord initialPos;
        private coord newPos;
        private BubbleSprite bubbleSprite;
        private Random rnd;


        public BubbleMoveCommand(BubbleSprite bubble)
        {
            dir = tox.GetDirection();
            coordF d = tox.GetGridPosition();
            initialPos = new coord() {
                x = (int)d.x,
                y = (int)d.y,
            };
            bubbleSprite = bubble;
            rnd = new Random();
        }

        //TODO: Add movement to history if there is no collision with wall
        public void Execute(GameModel model)
        {
            if (bubbleSprite.IsMovementOver())
            {
                int[,] labyrinth = model.GetLabyrinth();
                coord defaultDirCoord = GetCoordFromDir(dir, initialPos);
                if(labyrinth[defaultDirCoord.y, defaultDirCoord.x] == 0 || labyrinth[defaultDirCoord.y, defaultDirCoord.x] == 3)
                {
                    bubbleSprite.StartMovement(defaultDirCoord, dir);
                }
                else
                {
                    List<Direction> possibleDirs = EvaluatePossibleDirections(labyrinth, initialPos);
                    Direction newDir = possibleDirs.ElementAt(rnd.Next(possibleDirs.Count));
                    defaultDirCoord = GetCoordFromDir(newDir, initialPos);
                    toxicSprite.StartMovement(defaultDirCoord, newDir);
                }
            }
        }
        public void Undo(GameModel model)
        {
            
        }
        public bool IsHistoryAction() => true;

        private List<Direction> EvaluatePossibleDirections(int[,] lab, coord initialCoord)
        {
            List<Direction> result = new List<Direction>();
            coord tempCoord = GetCoordFromDir(Direction.DOWN, initialCoord);
            if (lab[tempCoord.y, tempCoord.x] == 0 || lab[tempCoord.y, tempCoord.x] == 3)
            {
                result.Add(Direction.DOWN);
            }
            tempCoord = GetCoordFromDir(Direction.UP, initialCoord);
            if (lab[tempCoord.y, tempCoord.x] == 0 || lab[tempCoord.y, tempCoord.x] == 3)
            {
                result.Add(Direction.UP);
            }
            tempCoord = GetCoordFromDir(Direction.RIGHT, initialCoord);
            if (lab[tempCoord.y, tempCoord.x] == 0 || lab[tempCoord.y, tempCoord.x] == 3)
            {
                result.Add(Direction.RIGHT);
            }
            tempCoord = GetCoordFromDir(Direction.LEFT, initialCoord);
            if (lab[tempCoord.y, tempCoord.x] == 0 || lab[tempCoord.y, tempCoord.x] == 3)
            {
                result.Add(Direction.LEFT);
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
*/