using System.Drawing;

/* EXPLORUS-E
 * Alexis BLATRIX (blaa1406)
 * Cédric CHARRON (chac0902)
 * Audric DAVID (dava1302)
 * Matthieu JEHANNE (jehm1701)
 * Cloé LEGLISE (legc1001)
 * Mahdi Majdoub (majm2404)
 */

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
