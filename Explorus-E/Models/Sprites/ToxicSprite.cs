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
    internal class ToxicSprite : SlimeTypeSprite
    {
        public ToxicSprite(coord gridPos, int top, int left, int brick) : base(gridPos, top, left, brick)
        {
            timeToMove = 600;
            lives = 2;
        }

        public void IncreaseSpeed()
        {
            timeToMove -= 30;
        }


        public override String GetImageName()
        {
            if (dir == Direction.UP) return Constants.Constants.TOXICUP_SPRITE_NAME + (imageIndex + 1).ToString();
            else if (dir == Direction.DOWN) return Constants.Constants.TOXICDOWN_SPRITE_NAME + (imageIndex + 1).ToString();
            else if (dir == Direction.RIGHT) return Constants.Constants.TOXICRIGHT_SPRITE_NAME + (imageIndex + 1).ToString();
            else if (dir == Direction.LEFT) return Constants.Constants.TOXICLEFT_SPRITE_NAME + (imageIndex + 1).ToString();
            else return "Idle";
        }

        //Renderable Interface
        public override Renderable CopyForRender()
        {
            ToxicSprite copy = new ToxicSprite(new coord(), base.topMargin, base.leftMargin, base.brickSize);
            copy.SetDirection(dir);
            copy.SetGridPosition(currentPos);
            copy.SetTransparency(transparency);
            return copy;
        }
    }
}
