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

namespace ExplorusE.Models
{
    internal class PauseMenu : Renderable, IResizeEventSubscriber
    {
        private IControllerMenu controller;
        private List<Text> itemList;
        private readonly int topMargin;
        private readonly int leftMargin;
        private readonly int brickSize;
        private bool isPlaying;

        private Text menuBlock;
        private Text titleText;
        private Text launchGame;
        private Text soundVolume;
        private Text soundValue;
        private Text helpText;
        private Text exitGame;

        private string start = "Start";
        private string resume = "Resume";
        private string play = "";

        private int volume;

        private Color isSelected = Color.Blue;
        private Color notSelected = Color.Black;

        private List<Color> colorList = new List<Color>();
        private Color startColor = Color.Black;
        private Color soundColor = Color.Black;
        private Color helpColor = Color.Black;
        private Color exitColor = Color.Black;


        public PauseMenu(int top, int left, int brick)
        {
            this.topMargin = top;
            this.leftMargin = left;
            this.brickSize = brick;

            colorList = new List<Color>();
            colorList.Add(startColor);
            colorList.Add(soundColor);
            colorList.Add(helpColor);
            colorList.Add(exitColor);

            if (isPlaying) { play = resume; }
            else { play = start; }

            InitMenu();

        }

        public void SetColor(int i)
        {
            int index = i - 1;
            for (int j = 0; j < colorList.Count(); j++)
            {
                if(j == index)
                {
                    colorList[j] = isSelected;
                }
                else
                {
                    colorList[j] = notSelected;
                }
            }

        }

        public void SetIsPlaying(bool b)
        {
            isPlaying = b;
        }
        public bool GetIsPlaying()
        {
            return isPlaying;
        }

        public void SetVolume(int i)
        {
            volume = i;
        }
        public int GetVolume()
        {
            return volume;
        }

        private PauseMenu(List<Text> itemList)
        {
            this.itemList = itemList;
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
            topMargin, leftMargin,brickSize);
            itemList.Add(menuBlock);

            titleText = new Text("Explorus", new SizeF()
            {
                Width = (float)6,
                Height = (float)1.5
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

            launchGame = new Text(play, new SizeF()
            {
                Width = (float)6,
                Height = (float)1
            }, "Arial", Color.White, startColor, new coord()
            {
                x = 5,
                y = 6
            }, new coordF()
            {
                x = 0.2,
                y = 0.2
            },
            topMargin, leftMargin, brickSize);
            itemList.Add(launchGame);

            soundVolume = new Text("Sound volume : ", new SizeF()
            {
                Width = (float)5,
                Height = (float)1
            }, "Arial", Color.White, soundColor, new coord()
            {
                x = 5,
                y = 7
            }, new coordF()
            {
                x = 0.2,
                y = 0.2
            },
            topMargin, leftMargin, brickSize);
            itemList.Add(soundVolume);
            soundValue = new Text(volume.ToString(), new SizeF()
            {
                Width = (float)1,
                Height = (float)1
            }, "Arial", Color.Yellow, soundColor, new coord()
            {
                x = 10,
                y = 7
            }, new coordF()
            {
                x = 0.2,
                y = 0.2
            },
            topMargin, leftMargin, brickSize);
            itemList.Add(soundValue);

            helpText = new Text("Help", new SizeF()
            {
                Width = (float)6,
                Height = (float)1
            }, "Arial", Color.White, helpColor, new coord()
            {
                x = 5,
                y = 8
            }, new coordF()
            {
                x = 0.2,
                y = 0.2
            },
            topMargin, leftMargin, brickSize);
            itemList.Add(helpText);

            exitGame = new Text("Exit game", new SizeF()
            {
                Width = (float)6,
                Height = (float)1
            }, "Arial", Color.White, exitColor, new coord()
            {
                x = 5,
                y = 9
            }, new coordF()
            {
                x = 0.2,
                y = 0.2
            },
            topMargin, leftMargin, brickSize);
            itemList.Add(exitGame);

        }

        public void Update()
        {
            itemList[2] = new Text(play, new SizeF()
            {
                Width = (float)6,
                Height = (float)1
            }, "Arial", Color.White, colorList[0], new coord()
            {
                x = 5,
                y = 6
            }, new coordF()
            {
                x = 0.2,
                y = 0.2
            },
            topMargin, leftMargin, brickSize);

            itemList[3] = new Text("Sound volume : ", new SizeF()
            {
                Width = (float)5,
                Height = (float)1
            }, "Arial", Color.White, colorList[1], new coord()
            {
                x = 5,
                y = 7
            }, new coordF()
            {
                x = 0.2,
                y = 0.2
            },
            topMargin, leftMargin, brickSize);
            itemList[4] = new Text(volume.ToString(), new SizeF()
            {
                Width = (float)1,
                Height = (float)1
            }, "Arial", Color.Yellow, colorList[1], new coord()
            {
                x = 10,
                y = 7
            }, new coordF()
            {
                x = 0.2,
                y = 0.2
            },
            topMargin, leftMargin, brickSize);

            itemList[5] = new Text("Help", new SizeF()
            {
                Width = (float)6,
                Height = (float)1
            }, "Arial", Color.White, colorList[2], new coord()
            {
                x = 5,
                y = 8
            }, new coordF()
            {
                x = 0.2,
                y = 0.2
            },
            topMargin, leftMargin, brickSize);

            itemList[6] = new Text("Exit game", new SizeF()
            {
                Width = (float)6,
                Height = (float)1
            }, "Arial", Color.White, colorList[3], new coord()
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
            foreach (Text t in itemList) controller.AddSubscriber(t);
        }

        public void Render(Graphics g)
        {
            foreach (Text text in itemList) text.Render(g);
        }

        public Renderable CopyForRender()
        {
            List<Text> list = new List<Text>();
            foreach (Text item in itemList) list.Add((Text)item.CopyForRender());
            return new PauseMenu(list);
        }

    }
}
