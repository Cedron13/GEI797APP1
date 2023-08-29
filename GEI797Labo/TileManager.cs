using GEI797Labo.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEI797Labo
{
    internal class TileManager
    {
        private Dictionary<string, Image2D> images;
        public TileManager() { 
            
            images = new Dictionary<string, Image2D>();
            Bitmap tilesheet = Properties.Resources.TilesSheet;

            //Wall
            Rectangle tileBounds = new Rectangle(0, 0, 96, 96);
            images.Add("Wall", new Image2D(1, ImageType.Wall, tilesheet.Clone(tileBounds, tilesheet.PixelFormat)));

            //Down1
            tileBounds = new Rectangle(0, 96, 96, 96);
            images.Add("Down1", new Image2D(2, ImageType.Player, tilesheet.Clone(tileBounds, tilesheet.PixelFormat)));

            //Down2
            tileBounds = new Rectangle(96, 96, 96, 96);
            images.Add("Down2", new Image2D(2, ImageType.Player, tilesheet.Clone(tileBounds, tilesheet.PixelFormat)));

            //Down3
            tileBounds = new Rectangle(96*2, 96, 96, 96);
            images.Add("Down3", new Image2D(2, ImageType.Player, tilesheet.Clone(tileBounds, tilesheet.PixelFormat)));

        }
        public Image2D getImage(string name)
        {
            Image2D image = images[name];
            return image;
        }

    }
}
