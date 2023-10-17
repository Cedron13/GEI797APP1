using ExplorusE.Constants;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExplorusE.Models.Sprites
{
    internal class MiniSlimeSprite : Sprite
    {
        public MiniSlimeSprite(coord gridPos, int top, int left, int brick) : base(gridPos, top, left, brick)
        {
        }

        //Renderable interface
        public override Renderable CopyForRender()
        {
            MiniSlimeSprite copy = new MiniSlimeSprite(new coord(), base.topMargin, base.leftMargin, base.brickSize);
            copy.SetGridPosition(currentPos);
            return copy;
        }

        public override void Render(Graphics g)
        {
            g.DrawImage(tileManager.getImage(GetImageName()).bitmap, GetPixelPosition().x, GetPixelPosition().y, brickSize / 2, brickSize / 2);
            using (Brush transparencyBrush = new SolidBrush(Color.FromArgb(transparency, Color.Black)))
            {
                g.FillRectangle(transparencyBrush, new Rectangle(GetPixelPosition().x, GetPixelPosition().y, brickSize / 2, brickSize / 2));
            }
        }

        //Sprite
        public override string GetImageName()
        {
            return "MiniSlime";
        }

        protected override coord GetPixelPosition()
        {
            coord playerCurrentPixelCoord = new coord()
            {
                x = (int)(leftMargin + brickSize * currentPos.x + brickSize / 4),
                y = (int)(topMargin + brickSize * (currentPos.y + 1) + brickSize / 4)
            };
            return playerCurrentPixelCoord;
        }

        public override void Update(int elapsedMs)
        {
            //No update for the mini slime guy (he's waiting...)
        }
    }
}
