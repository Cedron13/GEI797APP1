using ExplorusE.Constants;
using ExplorusE.Observer;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExplorusE.Models
{
    internal class Text : IResizeEventSubscriber, Renderable
    {

        private string text;
        private int fontSize;
        private string font;
        private Color textColor;
        private Color backgroundColor;
        private coord drawPoint;

        public Text(string text, int fontSize, string font, Color textColor, Color backgroundColor, coord drawPoint)
        {
            this.text = text;
            this.fontSize = fontSize;
            this.font = font;
            this.textColor = textColor;
            this.backgroundColor = backgroundColor;
        }

        //IResizeEventSubscriber
        public void NotifyResize(int top, int left, int brick)
        {
            //TODO: Adapt fontSize and drawPoint with new dimensions
        }


        //Renderable interface
        public void Render(Graphics g)
        {
            using(Font f = new Font(this.font, this.fontSize))
            using(Brush b = new SolidBrush(this.textColor))
            {
                g.DrawString(text, f, b, drawPoint.x, drawPoint.y);
            }
        }

        public Renderable CopyForRender()
        {
            return new Text(text, fontSize, font, textColor, backgroundColor, drawPoint);
        }
    }
}
