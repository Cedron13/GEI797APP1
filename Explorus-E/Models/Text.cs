using ExplorusE.Constants;
using ExplorusE.Observer;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

/* EXPLORUS-E
 * Alexis BLATRIX (blaa1406)
 * Cédric CHARRON (chac0902)
 * Audric DAVID (dava1302)
 * Matthieu JEHANNE (jehm1701)
 * Cloé LEGLISE (legc1001)
 */

namespace ExplorusE.Models
{
    internal class Text : IResizeEventSubscriber, Renderable
    {

        public string TextToDisplay { get; set; }
        private int fontSize;
        private int height;
        private int width;
        private string font;
        private Color textColor;
        private Color backgroundColor;
        private coord drawPoint;
        private coord tile;
        private coordF tilePos;
        private SizeF brickCoef;

        public int top;
        public int left;
        public int brick;

        /// <summary>
        /// Creates a text obejct
        /// </summary>
        /// <param name="text">Text to display</param>
        /// <param name="brickCoef">Size of the area of the text (relative to a brick size)</param>
        /// <param name="font">Font to use</param>
        /// <param name="textColor">Color of the text</param>
        /// <param name="backgroundColor">Color of the background, Transparent for none</param>
        /// <param name="tile">Position of the text in the grid</param>
        /// <param name="tilePos">Position of the text inside the tile (upper-left corner)</param>
        /// <param name="top">Top margin of the window</param>
        /// <param name="left">Left margin of the window</param>
        /// <param name="brick">Size of the bricks</param>
        public Text(string text, SizeF brickCoef, string font, Color textColor, Color backgroundColor, coord tile, coordF tilePos, int top, int left, int brick)
        {
            this.TextToDisplay = text;
            this.brickCoef = brickCoef;
            this.width = (int) (brickCoef.Width * brick);
            this.height = (int) (brickCoef.Height * brick);
            this.font = font;
            this.textColor = textColor;
            this.backgroundColor = backgroundColor;
            this.tile = tile;
            this.tilePos = tilePos;
            this.drawPoint = new coord()
            {
                x = left + (int) (tile.x) * brick + (int) (tilePos.x * brick),
                y = top + (int) (tile.y+1) * brick + (int) (tilePos.y * brick)
            };
            this.top = top;
            this.left = left;
            this.brick = brick;
        }

        private void FindBestFontSize(Graphics g)
        {
            int minSize = 10;
            int maxSize = 50;

            while (minSize <= maxSize)
            {
                int midSize = (minSize + maxSize) / 2;

                if (TextFits(TextToDisplay, midSize, height, width, g))
                {
                    minSize++;
                }
                else
                {
                    maxSize--;
                }
            }
            fontSize = maxSize;
        }

        private bool TextFits(string text, int fontSize, int targetHeight, int targetWidth, Graphics g)
        {
            using (Font font = new Font(this.font, fontSize))
            {
                SizeF size = g.MeasureString(text, font);
                return size.Width <= targetWidth && size.Height <= targetHeight;
            }
        }

        //IResizeEventSubscriber
        public void NotifyResize(int top, int left, int brick)
        {
            height = (int) brickCoef.Height * brick;
            width = (int) brickCoef.Width * brick;
            coord newCoords = new coord()
            {
                x = left + (int)(tile.x) * brick + (int)(tilePos.x * brick),
                y = top + (int)(tile.y+1) * brick + (int)(tilePos.y * brick)
            };
            this.top = top;
            this.left = left;
            this.brick = brick;
            drawPoint = newCoords;
        }


        //Renderable interface
        public void Render(Graphics g)
        {
            FindBestFontSize(g);
            using (Font f = new Font(this.font, this.fontSize))
            {
                if (backgroundColor != Color.Transparent)
                {
                    using (Brush b = new SolidBrush(backgroundColor))
                    {
                        g.FillRectangle(b, new RectangleF(drawPoint.x, drawPoint.y, width, height));
                    }
                }

                SizeF size = g.MeasureString(TextToDisplay, f);
                using (Brush b = new SolidBrush(this.textColor))
                {
                    g.DrawString(TextToDisplay, f, b, drawPoint.x + width / 2 - size.Width / 2, drawPoint.y + height / 2 - size.Height / 2);
                }
            }
        }

        public Renderable CopyForRender()
        {
            return new Text(TextToDisplay, new SizeF()
            {
                Width = brickCoef.Width,
                Height = brickCoef.Height
            }, font, textColor, backgroundColor, new coord()
            {
                x = tile.x,
                y = tile.y
            }, new coordF()
            {
                x = tilePos.x,
                y = tilePos.y
            }, top, left, brick);
        }
    }
}
