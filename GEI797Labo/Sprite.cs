using GEI797Labo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEI797Labo
{
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
        private Image2D image;
        private coord initialPos;
        private coord currentPos;
        private coord destinationPos;
        private int imageIndex;
        private double timeToMove = 1000; // Ms
        private double timeElapsed = 0;
        public Sprite() {
            
        }

        public void StartMovement(coord finalPos)
        {
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
            if(ratio >1)
            {
                ratio = 1;
            }
            currentPos.x = (int)((destinationPos.x - initialPos.x) * ratio + initialPos.x);
            currentPos.y = (int)((destinationPos.y - initialPos.y) * ratio + initialPos.y);
            imageIndex = (int)(5 * ratio);
        }
    }
}
