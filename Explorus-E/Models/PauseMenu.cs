﻿using ExplorusE.Constants;
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
        private readonly int size;
        private readonly coord gridPos;
        private readonly int topMargin;
        private readonly int leftMargin;
        private readonly int brickSize;
        private readonly float brickScale;
        private readonly bool half;

        private Text menuBlock;
        private Text titleText;
        private Text launchGame;
        private Text soundVolume;
        private Text helpText;
        private Text exitGame;

        public PauseMenu(int top, int left, int brick)
        {
            this.topMargin = top;
            this.leftMargin = left;
            this.brickSize = brick;

            InitMenu();

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

            launchGame = new Text("Start", new SizeF()
            {
                Width = (float)6,
                Height = (float)1
            }, "Arial", Color.White, Color.Black, new coord()
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

            soundVolume = new Text("Sound volume : " + 0.ToString(), new SizeF()
            {
                Width = (float)6,
                Height = (float)1
            }, "Arial", Color.White, Color.Black, new coord()
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

            helpText = new Text("Help", new SizeF()
            {
                Width = (float)6,
                Height = (float)1
            }, "Arial", Color.White, Color.Black, new coord()
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
            itemList.Add(exitGame);



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
