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
    internal class ToxicSprite : SlimeTypeSprite
    {
        public ToxicSprite(coord gridPos, int top, int left, int brick) : base(gridPos, top, left, brick)
        {
            timeToMove = 600;
        }

        public void IncreaseSpeed()
        {
            timeToMove -= 20;
        }


        public override String GetImageName()
        {
            if (dir == Direction.UP) return "ToxicUp" + (imageIndex + 1).ToString();
            else if (dir == Direction.DOWN) return "ToxicDown" + (imageIndex + 1).ToString();
            else if (dir == Direction.RIGHT) return "ToxicRight" + (imageIndex + 1).ToString();
            else if (dir == Direction.LEFT) return "ToxicLeft" + (imageIndex + 1).ToString();
            else return "Idle";
        }
    }
}
