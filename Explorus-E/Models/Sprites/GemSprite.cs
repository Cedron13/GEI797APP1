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
    internal class GemSprite : Sprite
    {
        private bool isDestroyed = false;
        private String origin = "";
        public GemSprite(coord gridPos, int top, int left, int brick, string origin) : base(gridPos, top, left, brick)
        {
            this.origin = origin;
        }
        public override void Update(int elapsedMs)
        {
            //No Change, does not move
        }
        public string GetOrigin()
        {
            return origin;
        }
        public override String GetImageName()
        {
            return "Gem";
        }
        public void Destroy()
        {
            isDestroyed = true;
        }
        public bool IsDestroyed()
        {
            return isDestroyed;
        }
        public void ReCreate()
        {
            isDestroyed = false;
        }

        //Renderable Interface
        public override Renderable CopyForRender()
        {
            GemSprite copy = new GemSprite(new coord(), base.topMargin, base.leftMargin, base.brickSize, base.GetName());
            copy.SetDirection(base.dir);
            copy.SetGridPosition(currentPos);
            return copy;
        }
    }
}
