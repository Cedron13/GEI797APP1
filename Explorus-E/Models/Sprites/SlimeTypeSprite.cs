using ExplorusE.Observer;
using ExplorusE.Constants;
using System;
using System.Collections.Generic;
using System.Drawing;

/* EXPLORUS-E
 * Alexis BLATRIX (blaa1406)
 * Cédric CHARRON (chac0902)
 * Audric DAVID (dava1302)
 * Matthieu JEHANNE (jehm1701)
 * Cloé LEGLISE (legc1001)
 */

namespace ExplorusE.Models
{
    internal abstract class SlimeTypeSprite : Sprite
    {
        protected int lives;
        protected bool isAlive = true;
        public SlimeTypeSprite(coord gridPos, int top, int left, int brick) : base(gridPos, top, left, brick)
        {

        }

        public override void Update(int elapsedMs)
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
        public bool LoseLife()
        {
            lives--;
            if (lives == 0)
            {
                isAlive = false;
                return true;
            }
            return false;
        }
        public bool IsAlive()
        {
            return isAlive;
        }
        public void SetLives(int l)
        {
            lives = l;
        }
        public void GainLife()
        {
            lives++;
        }
        public void ReCreate()
        {
            SetLives(1);
            isAlive = true;
        }

        public int GetLives()
        {
            return lives;
        }
    }
}
