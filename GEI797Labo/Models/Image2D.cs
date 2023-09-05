using System.Drawing;

namespace GEI797Labo.Models
{
    public enum ImageType
    {
        Wall,
        Player,
        Collectible,
        Other
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
