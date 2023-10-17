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

namespace ExplorusE.Models.Commands
{
    internal class BubbleMoveCommand : IGameCommand
    {
        private Direction dir;
        private coord initialPos;
        private coord newPos;
        private BubbleSprite bubbleSprite;



        public BubbleMoveCommand(BubbleSprite bubble)
        {
            dir = bubble.GetDirection();
            coordF d = bubble.GetGridPosition();
            initialPos = new coord() {
                x = (int)d.x,
                y = (int)d.y,
            };
            bubbleSprite = bubble;

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

                    
                    bubbleSprite.Explode();

                }
            }
        }
        public void Undo(GameModel model)
        {
            bubbleSprite.StartMovement(initialPos, dir);
        }
        public bool IsHistoryAction() => true;

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
