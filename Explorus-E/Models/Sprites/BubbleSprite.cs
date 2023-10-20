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
        private volatile  bool isExploded = false;
        private int timeSinceExplosion = 0;

        public BubbleSprite(coord gridPos, int top, int left, int brick) : base(gridPos, top, left, brick)
        {

        }

        public BubbleSprite(coord gridPos, int top, int left, int brick, float brickScale) : base(gridPos, top, left, brick, brickScale)
        {

        }

        public void SetImageIndex(int index)
        {
            imageIndex = index;
        }

        public override void Update(int elapsedMs)
        {

            if (!isExploded)
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

            if (isExploded)
            {
                timeSinceExplosion += elapsedMs;

                
                if (timeSinceExplosion >= 500) 
                {
                    Destroy();
                }
            }

        }

        public void ReCreate()
        {
            isExploded = false;
            timeSinceExplosion = 0;
            isDestroyed = false;
        }
        public override String GetImageName()
        {
            
                if (isExploded) return Constants.Constants.EFFECT_SPRITE_NAME;

                return Constants.Constants.BUBBLE_SPRITE_NAME + (imageIndex + 1).ToString();
            

        }
        public bool IsDestroyed()
        {
            return isDestroyed;
        }

        public void Destroy()
        {
            /*isExploded = false;*/
            isDestroyed = true;
        }

        public void Explode()
        {
            
                isExploded = true;
            
        }

        public bool IsExploded()
        {
            
                
                return isExploded;
            
        }

        
        //Renderable Interface
        public override Renderable CopyForRender()
        {
            BubbleSprite copy = new BubbleSprite(new coord(), topMargin, leftMargin, brickSize, brickScale);
            copy.SetDirection(dir);
            copy.SetGridPosition(currentPos);
            if (isExploded)
            {
                copy.Explode();
            }
            if (isDestroyed)
            {
                copy.Destroy();
            }
            copy.SetImageIndex(imageIndex);

            return copy;
        }
    }
}
