using ExplorusE.Constants;
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
    internal class TileManager
    {
        private static TileManager instance;
        private static readonly object lockObject = new object();
        private Dictionary<string, Image2D> images;



        private TileManager()
        {
            images = new Dictionary<string, Image2D>();
            Bitmap tilesheet = Properties.Resources.TilesSheet;



            //Wall
            Rectangle tileBounds = new Rectangle(0, 0, 96, 96);
            images.Add("Wall", new Image2D(1, ImageType.Wall, tilesheet.Clone(tileBounds, tilesheet.PixelFormat)));



            //Title
            tileBounds = new Rectangle(96, 0, 195, 48);
            images.Add("Title", new Image2D(2, ImageType.Other, tilesheet.Clone(tileBounds, tilesheet.PixelFormat)));



            //Heart
            tileBounds = new Rectangle(288, 0, 48, 48);
            images.Add("Heart", new Image2D(3, ImageType.Other, tilesheet.Clone(tileBounds, tilesheet.PixelFormat)));



            //BigBubble
            tileBounds = new Rectangle(336, 0, 48, 48);
            images.Add("BigBubble", new Image2D(4, ImageType.Other, tilesheet.Clone(tileBounds, tilesheet.PixelFormat)));



            //SmallBubble
            tileBounds = new Rectangle(384, 0, 48, 48);
            images.Add("SmallBubble", new Image2D(5, ImageType.Other, tilesheet.Clone(tileBounds, tilesheet.PixelFormat)));



            //Effect
            tileBounds = new Rectangle(432, 0, 48, 48);
            images.Add("Effect", new Image2D(6, ImageType.Other, tilesheet.Clone(tileBounds, tilesheet.PixelFormat)));



            //Gem
            tileBounds = new Rectangle(480, 0, 48, 48);
            images.Add("Gem", new Image2D(7, ImageType.Other, tilesheet.Clone(tileBounds, tilesheet.PixelFormat)));



            //Key
            tileBounds = new Rectangle(528, 0, 48, 48);
            images.Add("Key", new Image2D(8, ImageType.Other, tilesheet.Clone(tileBounds, tilesheet.PixelFormat)));



            //BeginBar
            tileBounds = new Rectangle(96, 48, 48, 48);
            images.Add("BeginBar", new Image2D(9, ImageType.Other, tilesheet.Clone(tileBounds, tilesheet.PixelFormat)));



            //RedFull
            tileBounds = new Rectangle(144, 48, 48, 48);
            images.Add("RedFull", new Image2D(10, ImageType.Other, tilesheet.Clone(tileBounds, tilesheet.PixelFormat)));



            //RedHalf
            tileBounds = new Rectangle(192, 48, 48, 48);
            images.Add("RedHalf", new Image2D(11, ImageType.Other, tilesheet.Clone(tileBounds, tilesheet.PixelFormat)));



            //BlueFull
            tileBounds = new Rectangle(240, 48, 48, 48);
            images.Add("BlueFull", new Image2D(12, ImageType.Other, tilesheet.Clone(tileBounds, tilesheet.PixelFormat)));



            //BlueHalf
            tileBounds = new Rectangle(288, 48, 48, 48);
            images.Add("BlueHalf", new Image2D(13, ImageType.Other, tilesheet.Clone(tileBounds, tilesheet.PixelFormat)));

            //YellowFull
            tileBounds = new Rectangle(336, 48, 48, 48);
            images.Add("YellowFull", new Image2D(14, ImageType.Other, tilesheet.Clone(tileBounds, tilesheet.PixelFormat)));



            //YellowHalf
            tileBounds = new Rectangle(384, 48, 48, 48);
            images.Add("YellowHalf", new Image2D(15, ImageType.Other, tilesheet.Clone(tileBounds, tilesheet.PixelFormat)));



            //EmptyBar
            tileBounds = new Rectangle(432, 48, 48, 48);
            images.Add("EmptyBar", new Image2D(16, ImageType.Other, tilesheet.Clone(tileBounds, tilesheet.PixelFormat)));



            //EndBar
            tileBounds = new Rectangle(480, 48, 48, 48);
            images.Add("EndBar", new Image2D(17, ImageType.Other, tilesheet.Clone(tileBounds, tilesheet.PixelFormat)));



            //MiniSlime
            tileBounds = new Rectangle(528, 48, 48, 48);
            images.Add("MiniSlime", new Image2D(18, ImageType.Other, tilesheet.Clone(tileBounds, tilesheet.PixelFormat)));



            //Down1
            tileBounds = new Rectangle(0, 96, 96, 96);
            images.Add("Down1", new Image2D(19, ImageType.Player, tilesheet.Clone(tileBounds, tilesheet.PixelFormat)));



            //Down2
            tileBounds = new Rectangle(96, 96, 96, 96);
            images.Add("Down2", new Image2D(20, ImageType.Player, tilesheet.Clone(tileBounds, tilesheet.PixelFormat)));



            //Down3
            tileBounds = new Rectangle(96 * 2, 96, 96, 96);
            images.Add("Down3", new Image2D(21, ImageType.Player, tilesheet.Clone(tileBounds, tilesheet.PixelFormat)));



            //Right1
            tileBounds = new Rectangle(96 * 3, 96, 96, 96);
            images.Add("Right1", new Image2D(22, ImageType.Player, tilesheet.Clone(tileBounds, tilesheet.PixelFormat)));



            //Right2
            tileBounds = new Rectangle(96 * 4, 96, 96, 96);
            images.Add("Right2", new Image2D(23, ImageType.Player, tilesheet.Clone(tileBounds, tilesheet.PixelFormat)));



            //Right3
            tileBounds = new Rectangle(96 * 5, 96, 96, 96);
            images.Add("Right3", new Image2D(24, ImageType.Player, tilesheet.Clone(tileBounds, tilesheet.PixelFormat)));



            //Up1
            tileBounds = new Rectangle(0, 96 * 2, 96, 96);
            images.Add("Up1", new Image2D(25, ImageType.Player, tilesheet.Clone(tileBounds, tilesheet.PixelFormat)));



            //Up2
            tileBounds = new Rectangle(96, 96 * 2, 96, 96);
            images.Add("Up2", new Image2D(26, ImageType.Player, tilesheet.Clone(tileBounds, tilesheet.PixelFormat)));



            //Up3
            tileBounds = new Rectangle(96 * 2, 96 * 2, 96, 96);
            images.Add("Up3", new Image2D(27, ImageType.Player, tilesheet.Clone(tileBounds, tilesheet.PixelFormat)));



            //Left1
            tileBounds = new Rectangle(96 * 3, 96 * 2, 96, 96);
            images.Add("Left1", new Image2D(28, ImageType.Player, tilesheet.Clone(tileBounds, tilesheet.PixelFormat)));



            //Left2
            tileBounds = new Rectangle(96 * 4, 96 * 2, 96, 96);
            images.Add("Left2", new Image2D(29, ImageType.Player, tilesheet.Clone(tileBounds, tilesheet.PixelFormat)));



            //Left3
            tileBounds = new Rectangle(96 * 4, 96 * 2, 96, 96);
            images.Add("Left3", new Image2D(30, ImageType.Player, tilesheet.Clone(tileBounds, tilesheet.PixelFormat)));







            //ToxicDown1
            tileBounds = new Rectangle(0, 96 * 3, 96, 96);
            images.Add("ToxicDown1", new Image2D(19, ImageType.Player, tilesheet.Clone(tileBounds, tilesheet.PixelFormat)));



            //ToxicDown2
            tileBounds = new Rectangle(96, 96 * 3, 96, 96);
            images.Add("ToxicDown2", new Image2D(20, ImageType.Player, tilesheet.Clone(tileBounds, tilesheet.PixelFormat)));



            //ToxicDown3
            tileBounds = new Rectangle(96 * 2, 96 * 3, 96, 96);
            images.Add("ToxicDown3", new Image2D(21, ImageType.Player, tilesheet.Clone(tileBounds, tilesheet.PixelFormat)));



            //ToxicRight1
            tileBounds = new Rectangle(96 * 3, 96 * 3, 96, 96);
            images.Add("ToxicRight1", new Image2D(22, ImageType.Player, tilesheet.Clone(tileBounds, tilesheet.PixelFormat)));



            //ToxicRight2
            tileBounds = new Rectangle(96 * 4, 96 * 3, 96, 96);
            images.Add("ToxicRight2", new Image2D(23, ImageType.Player, tilesheet.Clone(tileBounds, tilesheet.PixelFormat)));



            //ToxicRight3
            tileBounds = new Rectangle(96 * 5, 96 * 3, 96, 96);
            images.Add("ToxicRight3", new Image2D(24, ImageType.Player, tilesheet.Clone(tileBounds, tilesheet.PixelFormat)));



            //ToxicUp1
            tileBounds = new Rectangle(0, 96 * 4, 96, 96);
            images.Add("ToxicUp1", new Image2D(25, ImageType.Player, tilesheet.Clone(tileBounds, tilesheet.PixelFormat)));



            //ToxicUp2
            tileBounds = new Rectangle(96, 96 * 4, 96, 96);
            images.Add("ToxicUp2", new Image2D(26, ImageType.Player, tilesheet.Clone(tileBounds, tilesheet.PixelFormat)));



            //ToxicUp3
            tileBounds = new Rectangle(96 * 2, 96 * 4, 96, 96);
            images.Add("ToxicUp3", new Image2D(27, ImageType.Player, tilesheet.Clone(tileBounds, tilesheet.PixelFormat)));



            //ToxicLeft1
            tileBounds = new Rectangle(96 * 3, 96 * 4, 96, 96);
            images.Add("ToxicLeft1", new Image2D(28, ImageType.Player, tilesheet.Clone(tileBounds, tilesheet.PixelFormat)));



            //ToxicLeft2
            tileBounds = new Rectangle(96 * 4, 96 * 4, 96, 96);
            images.Add("ToxicLeft2", new Image2D(29, ImageType.Player, tilesheet.Clone(tileBounds, tilesheet.PixelFormat)));



            //ToxicLeft3
            tileBounds = new Rectangle(96 * 4, 96 * 4, 96, 96);
            images.Add("ToxicLeft3", new Image2D(30, ImageType.Player, tilesheet.Clone(tileBounds, tilesheet.PixelFormat)));





        }



        public static TileManager GetInstance()
        {
            if (instance == null)
            {
                lock (lockObject)
                {
                    if (instance == null)
                    {
                        instance = new TileManager();
                    }
                }
            }
            return instance;
        }



        public Image2D getImage(string name)
        {
            Image2D image = images[name];
            return image;
        }
    }
}

