using ExplorusE.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExplorusE.Models.Sprites
{
    internal class Wall : Sprite
    {
        public Wall(coord gridPos, int top, int left, int brick) : base(gridPos, top, left, brick)
        {
        }

        public Wall(coord gridPos, int top, int left, int brick, int transparency) : base(gridPos, top, left, brick, transparency)
        {
        }

        //Renderable interface
        public override Renderable CopyForRender()
        {
            Wall copy = new Wall(new coord(), base.topMargin, base.leftMargin, base.brickSize, base.transparency);
            copy.SetGridPosition(currentPos);
            return copy;
        }

        //Sprite
        public override string GetImageName()
        {
            return "Wall";
        }

        public override void Update(int elapsedMs)
        {
            //No update for the wall guy (a brick)
        }
    }
}
