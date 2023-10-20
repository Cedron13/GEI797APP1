using ExplorusE.Constants;
using ExplorusE.Controllers;
using ExplorusE.Models.Sprites;
using ExplorusE.Observer;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Header;

/* EXPLORUS-E
 * Alexis BLATRIX (blaa1406)
 * Cédric CHARRON (chac0902)
 * Audric DAVID (dava1302)
 * Matthieu JEHANNE (jehm1701)
 * Cloé LEGLISE (legc1001)
 */

namespace ExplorusE.Models
{
    internal class HelpMenu : Renderable, IResizeEventSubscriber
    {
        private IControllerMenu controller;
        private List<Text> itemList;
        private readonly int size;
        private readonly coord gridPos;
        private int topMargin;
        private int leftMargin;
        private int brickSize;
        private readonly float brickScale;
        private readonly bool half;

        private Text menuBlock;
        private Text titleText;
        private Text controlKeys;
        private Text goBack;
        private Color selectedColor;

        public HelpMenu(int top, int left, int brick)
        {
            this.topMargin = top;
            this.leftMargin = left;
            this.brickSize = brick;

            InitMenu();

        }
        private HelpMenu(List<Text> itemList)
        {
            this.itemList = itemList;
        }

        public void SetColor(bool b)
        {
            if (b) { selectedColor = Color.Blue; }
            else { selectedColor = Color.Black; }
        }
        private void InitMenu()
        {
            itemList = new List<Text>();

            menuBlock = new Text("", new SizeF()
            {
                Width = (float)8.6,
                Height = (float)6.6
            }, "Arial", Color.Black, Color.Black, new coord()
            {
                x = 4,
                y = 4
            }, new coordF()
            {
                x = 0.2,
                y = 0.2
            },
            topMargin, leftMargin, brickSize);
            itemList.Add(menuBlock);

            titleText = new Text("Explorus", new SizeF()
            {
                Width = (float)6,
                Height = (float)1.25
            }, "Arial", Color.FromArgb(100, 69, 180, 239), Color.Black, new coord()
            {
                x = 5,
                y = 4
            }, new coordF()
            {
                x = 0.2,
                y = 0.2
            },
            topMargin, leftMargin, brickSize);
            itemList.Add(titleText);

            controlKeys = new Text("Control Keys :\n " +
                "\t ← → ↓ ↑ : Slimus movement\n " +
                "\t ← → : Undo / Redo (on pause)\n " +
                "\t Spacebar : Throw a Bubble\n" +
                "\t F : Show FPS\n" +
                "\t P : Pause game\n" +
                "\t R : Resume Game\n" +
                "\t ESC : Show menu", new SizeF()
            {
                Width = (float)6,
                Height = (float)4
            }, "Arial", Color.White, Color.Black, new coord()
            {
                x = 5,
                y = 5
            }, new coordF()
            {
                x = 0.2,
                y = 0.2
            },
            topMargin, leftMargin, brickSize);
            itemList.Add(controlKeys);

           
            goBack = new Text("Return", new SizeF()
            {
                Width = (float)6,
                Height = (float)0.9
            }, "Arial", Color.White, Color.Black, new coord()
            {
                x = 5,
                y = 9
            }, new coordF()
            {
                x = 0.2,
                y = 0.2
            },
            topMargin, leftMargin, brickSize);
            itemList.Add(goBack);
        }

        public void Update()
        {
            itemList[3] = new Text("Return", new SizeF()
            {
                Width = (float)6,
                Height = (float)0.9
            }, "Arial", Color.White, selectedColor, new coord()
            {
                x = 5,
                y = 9
            }, new coordF()
            {
                x = 0.2,
                y = 0.2
            },
            topMargin, leftMargin, brickSize);
        }


        public void NotifyResize(int top, int left, int brick)
        {
            topMargin = top;
            leftMargin = left;
            brickSize = brick;
            foreach (Text t in itemList)
            {
                t.NotifyResize(top, left, brick);
            }
        }

        public void Render(Graphics g)
        {
            foreach (Text text in itemList) text.Render(g);
        }

        public Renderable CopyForRender()
        {
            List<Text> list = new List<Text>();
            foreach (Text item in itemList) list.Add((Text)item.CopyForRender());
            return new HelpMenu(list);
        }

    }
}
