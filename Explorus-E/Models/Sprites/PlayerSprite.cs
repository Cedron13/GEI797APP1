﻿using ExplorusE.Observer;
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
    internal class PlayerSprite : SlimeTypeSprite
    {
        bool isInvincible = false;
        private readonly object lockInvincibleTimer = new object();
        bool timeDone;
        public PlayerSprite(coord gridPos, int top, int left, int brick) : base(gridPos, top, left, brick)
        {
            timeToMove = 500;
        }

        public void SetInvincible()
        {
            //Partir Timer pour remettre invincible a false
            isInvincible=true;
        }

        public void SetTimeDone(bool b)
        {
            timeDone=b;
        }

        public override void Update(int elapsedMs)
        {
            lock(lockInvincibleTimer)
            {
                if (timeDone)
                {
                    isInvincible = false;
                    timeDone = false;
                }
            }    
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

        public bool IsInvincible()
        {
            return isInvincible;
        }
        public override String GetImageName()
        {
            if (dir == Direction.UP) return "Up" + (imageIndex + 1).ToString();
            else if (dir == Direction.DOWN) return "Down" + (imageIndex + 1).ToString();
            else if (dir == Direction.RIGHT) return "Right" + (imageIndex + 1).ToString();
            else if (dir == Direction.LEFT) return "Left" + (imageIndex + 1).ToString();
            else return "Idle";
        }

        //Renderable Interface
        public override Renderable CopyForRender()
        {
            PlayerSprite copy = new PlayerSprite(new coord(), base.topMargin, base.leftMargin, base.brickSize);
            copy.SetDirection(base.dir);
            copy.SetGridPosition(currentPos);
            return copy;
        }
    }
}
