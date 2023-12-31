﻿using ExplorusE.Constants;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* EXPLORUS-E
 * Alexis BLATRIX (blaa1406)
 * Cédric CHARRON (chac0902)
 * Audric DAVID (dava1302)
 * Matthieu JEHANNE (jehm1701)
 * Cloé LEGLISE (legc1001)
 */

namespace ExplorusE.Models.Sprites
{
    internal class NotInGridSprite : Sprite
    {
        private coordF tilePos;
        private string imageName;
        private float brickScale;

        public NotInGridSprite(coord gridPos, coordF tilePos, string imageName, int top, int left, int brick, float brickScale) : base(gridPos, top, left, brick)
        { 
            this.tilePos = tilePos;
            this.imageName = imageName;
            this.brickScale = brickScale;
        }

        public override void Update(int elapsedMs)
        {
            //Not supposed to update
        }

        public override string GetImageName()
        {
            return imageName;
        }

        public void SetImageName(string newName)
        {
            imageName = newName;
        }

        protected override coord GetPixelPosition()
        {
            coord playerCurrentPixelCoord = new coord()
            {
                x = (int)(leftMargin + brickSize * currentPos.x + brickSize * tilePos.x),
                y = (int)(topMargin + brickSize * (currentPos.y + 1) + brickSize * tilePos.y)
            };
            return playerCurrentPixelCoord;
        }

        public float GetScaleWidth()
        {
            return brickSize / tileManager.getImage(GetImageName()).bitmap.Width * brickScale;
        }

        //Renderable interface
        public override Renderable CopyForRender()
        {
            NotInGridSprite ret = new NotInGridSprite(new coord(), tilePos, imageName, topMargin, leftMargin, brickSize, brickScale);
            ret.SetGridPosition(base.GetGridPosition());
            return ret;
        }

        public override void Render(Graphics g)
        {
            float imageHeight = brickSize * brickScale;
            float imageWidth = tileManager.getImage(GetImageName()).bitmap.Width / (float)tileManager.getImage(GetImageName()).bitmap.Height * brickSize * brickScale;

            g.DrawImage(tileManager.getImage(GetImageName()).bitmap, GetPixelPosition().x, GetPixelPosition().y, imageWidth, imageHeight);
        }
    }
}
