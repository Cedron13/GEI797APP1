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
    internal class GemSprite : Sprite
    {

        public GemSprite(coord gridPos, int top, int left, int brick) : base(gridPos, top, left, brick)
        {

        }
        public override void Update(int elapsedMs)
        {
            //No Change, does not move
        }
        public override String GetImageName()
        {
            return "Gem";
        }
    }
}
