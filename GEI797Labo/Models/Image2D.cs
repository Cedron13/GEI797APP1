using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEI797Labo.Models
{
    public enum ImageType
    {
        Wall,
        Player,
        Collectible
    };

    internal class Image2D
    {
        public int id;
        public ImageType type;
        public Bitmap bitmap;
        public Image2D(int imageId, ImageType imageType, Bitmap imageBitmap)
        {
            id = imageId;
            type = imageType;
            bitmap = imageBitmap;
        }
    }
}
