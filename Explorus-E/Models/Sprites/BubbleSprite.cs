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
    internal class BubbleSprite : Sprite
    {
        protected new double timeToMove = 250;
        private bool isDestroyed = false;

        public BubbleSprite(coord gridPos, int top, int left, int brick) : base(gridPos, top, left, brick)
        {

        }

        public override void Update(int elapsedMs)
        {
            timeElapsed += elapsedMs;
            double ratio = timeElapsed / timeToMove;
            if (ratio > 1)
            {
                ratio = 1; //Avoid the position going further than it is supposed
            }
            currentPos.x = ((double)destinationPos.x - initialPos.x) * ratio + initialPos.x;
            currentPos.y = ((double)destinationPos.y - initialPos.y) * ratio + initialPos.y;
            imageIndex = (int)(3.99 * ratio);//so we never have an index of 4
            if (imageIndex == 2) { imageIndex = 0; }
            if (imageIndex >= 3) { imageIndex = 1; }
        }

        public override String GetImageName()
        {
            return "Bubble" + (imageIndex + 1).ToString();
        }
        public bool IsDestroyed()
        {
            return isDestroyed;
        }
        public void Destroy()
        {
            isDestroyed = true;
        }

        //Renderable Interface
        public override Renderable CopyForRender()
        {
            BubbleSprite copy = new BubbleSprite(new coord(), topMargin, leftMargin, brickSize);
            copy.SetDirection(dir);
            copy.SetGridPosition(currentPos);
            return copy;
        }
    }
}
