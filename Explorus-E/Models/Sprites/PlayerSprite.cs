using ExplorusE.Observer;
using ExplorusE.Constants;
using System;
using System.Collections.Generic;

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
        public PlayerSprite(coord gridPos, int top, int left, int brick) : base(gridPos, top, left, brick)
        {
            timeToMove = 500;
        }
        public void SetInvincible()
        {
            //Partir Timer pour remettre invincible a false
            isInvincible=true;
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
    }
}
