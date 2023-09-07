/* EXPLORUS-E
 * Alexis BLATRIX (blaa1406)
 * Cédric CHARRON (chac0902)
 * Audric DAVID (dava1302)
 * Matthieu JEHANNE (jehm1701)
 * Cloé LEGLISE (legc1001)
 */

using System;

namespace GEI797Labo
{
    public enum Direction
    {
        DOWN, RIGHT, UP, LEFT, IDLE
    }
    public struct coord
    {
        public int x { get; set; }
        public int y { get; set; }
    }
    public struct spriteState
    {
        public int imageIndex { get; set; }
        public coord spriteCoord { get; set; }
    }
    internal class Sprite
    {
        private coord initialPos;
        private coord currentPos;
        private coord destinationPos;
        private int imageIndex;
        private Direction dir;
        private double timeToMove = 500;// Ms
        private double timeElapsed = 0;

        public Sprite(coord pos)
        {
            currentPos = pos;
            destinationPos = pos;
        }

        public void StartMovement(coord finalPos, Direction d)
        {
            dir = d;
            initialPos = currentPos;
            destinationPos = finalPos;
            timeElapsed = 0;
        }
        public spriteState GetCurrentRenderInfo()
        {
            spriteState state = new spriteState();
            state.imageIndex = imageIndex;
            state.spriteCoord = currentPos;
            return state;
        }
        public bool IsMovementOver()
        {
            return currentPos.x == destinationPos.x && currentPos.y == destinationPos.y;
        }
        public void Update(int elapsedMs)
        {
            timeElapsed += elapsedMs;
            double ratio = timeElapsed / timeToMove;
            if (ratio > 1)
            {
                ratio = 1;
            }
            currentPos.x = (int)((destinationPos.x - initialPos.x) * ratio + initialPos.x);
            currentPos.y = (int)((destinationPos.y - initialPos.y) * ratio + initialPos.y);
            imageIndex = (int)(5 * ratio);
            if (imageIndex == 3) { imageIndex = 1; }
            if (imageIndex >= 4) {  imageIndex = 0; }
        }

        public coord GetPosition() => currentPos;

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
