using ExplorusE.Observer;
using ExplorusE.Constants;
using System;

/* EXPLORUS-E
 * Alexis BLATRIX (blaa1406)
 * Cédric CHARRON (chac0902)
 * Audric DAVID (dava1302)
 * Matthieu JEHANNE (jehm1701)
 * Cloé LEGLISE (legc1001)
 */

namespace ExplorusE.Models
{
    internal class Sprite : IResizeEventSubscriber
    {
        private coord initialPos;
        private coordF currentPos;
        private coord destinationPos;
        private int imageIndex;
        private Direction dir;
        private double timeToMove = 500; //Time in ms
        private double timeElapsed = 0; //Time in ms
        private string name = "Anonymous";
        private readonly object coordlock = new object();

        private int topMargin;
        private int leftMargin;
        private int brickSize;

        public Sprite(coord gridPos, int top, int left, int brick)
        {
            currentPos.x = gridPos.x;
            currentPos.y = gridPos.y;
            destinationPos = gridPos;
            topMargin = top;
            leftMargin = left;
            brickSize = brick;
        }
        public void setName(string n)
        {
            name = n;
        }
        //IResizeEventSubscriber
        public void NotifyResize(int top, int left, int brick)
        {
            topMargin = top;
            leftMargin = left;
            brickSize = brick;
        }

        //Sprite
        public void StartMovement(coord finalGridPos, Direction d)
        {
            lock (coordlock)
            {
                dir = d;
                initialPos.x = (int)currentPos.x;
                initialPos.y = (int)currentPos.y;
                currentPos.x = (int)currentPos.x;
                currentPos.y = (int)currentPos.y;
                destinationPos = finalGridPos;
                timeElapsed = 0;
            }
        }
        public spriteState GetCurrentRenderInfo()
        {
            spriteState state = new spriteState();
            state.imageIndex = imageIndex;
            state.spriteCoord = GetPixelPosition();
            return state;
        }
        public bool IsMovementOver()
        {
            lock (coordlock)
            {
                return currentPos.x == destinationPos.x && currentPos.y == destinationPos.y;
            }
        }

        public void Update(int elapsedMs)
        {
            lock (coordlock)
            {
                timeElapsed += elapsedMs;
                double ratio = timeElapsed / timeToMove;
                if (ratio > 1)
                {
                    ratio = 1; //Avoid the position going further than it is supposed
                }
                currentPos.x = ((double)destinationPos.x - initialPos.x) * ratio + initialPos.x;
                currentPos.y = ((double)destinationPos.y - initialPos.y) * ratio + initialPos.y;
                imageIndex = (int)(5 * ratio);
                if (imageIndex == 3) { imageIndex = 1; }
                if (imageIndex >= 4) { imageIndex = 0; }
            }
        }

        public coordF GetGridPosition() => currentPos;

        public coord GetPixelPosition()
        {
            lock (coordlock)
            {
                coord playerCurrentPixelCoord = new coord()
                {
                    x = (int)(leftMargin + brickSize * currentPos.x),
                    y = (int)(topMargin + brickSize * (currentPos.y + 1))
                };
                return playerCurrentPixelCoord;
            }
        }
        public Direction GetDirection() => dir;

        public void SetDirection(Direction d)
        {
            dir = d;
        }

        public String GetImageName()
        {
            if (dir == Direction.UP) return "Up" + (imageIndex + 1).ToString();
            else if (dir == Direction.DOWN) return "Down" + (imageIndex + 1).ToString();
            else if (dir == Direction.RIGHT) return "Right" + (imageIndex + 1).ToString();
            else if (dir == Direction.LEFT) return "Left" + (imageIndex + 1).ToString();
            else return "Idle";
        }
    }
}
