using GEI797Labo.Controllers;
using GEI797Labo.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;


namespace GEI797Labo
{
    internal class GameView
    {
        private GameForm oGameForm; // Let GameForm be an existing class
        private TileManager tileManager;
        private IController controller;
        private int displayWidth;
        private int displayHeight;
        private int brickSize = 50;
        private int minSize;
        private int leftMargin = 25;
        private int topMargin = 25;
        private int brickMiddle = 12;
        private Thread windowThread;


        public GameView(IController c)
        {
            controller = c;
            oGameForm = new GameForm();
            oGameForm.Paint += GameRenderer;
            oGameForm.PreviewKeyDown += KeyDownEvent;
            oGameForm.FormClosing += CloseWindowEvent;
            oGameForm.SizeChanged += GameForm_SizeChanged;
            tileManager = new TileManager();

            windowThread = new Thread(new ThreadStart(Show)); //New thread because "Application.run()" blocks the actual thread and prevents the engine to run
            windowThread.Start();

        }

        public void Show()
        {
            Application.Run(oGameForm);
        }

        public void Render()
        {
            if (oGameForm.Visible)
            {
                oGameForm.BeginInvoke((MethodInvoker)delegate
                {
                    oGameForm.Refresh();
                });
            }
        }

        public void Close()
        {
            if (oGameForm.Visible)
            {
                oGameForm.BeginInvoke((MethodInvoker)delegate
                {
                    oGameForm.Close();
                });
            }
        }

        private void GameRenderer(object sender, PaintEventArgs e)
        {

            Graphics g = e.Graphics;
            // Black background and menu
            g.Clear(Color.Black);

            g.DrawImage(tileManager.getImage("Title").bitmap, leftMargin, topMargin, brickSize * 2, brickSize / 2);

            g.DrawImage(tileManager.getImage("Heart").bitmap, leftMargin + brickSize * 11 /4, topMargin, brickSize / 2, brickSize / 2);
            g.DrawImage(tileManager.getImage("BeginBar").bitmap, leftMargin + brickSize * 3, topMargin, brickSize / 2, brickSize / 2);
            g.DrawImage(tileManager.getImage("RedFull").bitmap, leftMargin + brickSize * 7/2, topMargin, brickSize / 2, brickSize / 2);
            g.DrawImage(tileManager.getImage("RedHalf").bitmap, leftMargin + brickSize * 4, topMargin, brickSize / 2, brickSize / 2);
            g.DrawImage(tileManager.getImage("EndBar").bitmap, leftMargin + brickSize * 9/2, topMargin, brickSize / 2, brickSize / 2);

            g.DrawImage(tileManager.getImage("BigBubble").bitmap, leftMargin + brickSize * 21/4, topMargin, brickSize / 2, brickSize / 2);
            g.DrawImage(tileManager.getImage("BeginBar").bitmap, leftMargin + brickSize * 11/2, topMargin, brickSize / 2, brickSize / 2);
            g.DrawImage(tileManager.getImage("BlueFull").bitmap, leftMargin + brickSize * 6, topMargin, brickSize / 2, brickSize / 2);
            g.DrawImage(tileManager.getImage("BlueHalf").bitmap, leftMargin + brickSize * 13/2, topMargin, brickSize / 2, brickSize / 2);
            g.DrawImage(tileManager.getImage("EndBar").bitmap, leftMargin + brickSize * 7, topMargin, brickSize / 2, brickSize / 2);

            g.DrawImage(tileManager.getImage("Gem").bitmap, leftMargin + brickSize * 31/4, topMargin, brickSize / 2, brickSize / 2);
            g.DrawImage(tileManager.getImage("BeginBar").bitmap, leftMargin + brickSize * 8, topMargin, brickSize / 2, brickSize / 2);
            g.DrawImage(tileManager.getImage("YellowFull").bitmap, leftMargin + brickSize * 17/2, topMargin, brickSize / 2, brickSize / 2);
            g.DrawImage(tileManager.getImage("YellowHalf").bitmap, leftMargin + brickSize * 9, topMargin, brickSize / 2, brickSize / 2);
            g.DrawImage(tileManager.getImage("EndBar").bitmap, leftMargin + brickSize * 19/2, topMargin, brickSize / 2, brickSize / 2);

            //g.DrawImage(tileManager.getImage("Key").bitmap, leftMargin + brickSize * 10, topMargin, brickSize / 2, brickSize / 2);


            //Calls Lab from another thread, lock may be needed
            int[,] labyrinth = controller.GetLabyrinth();


            for (int i = 0; i < labyrinth.GetLength(1); i++)
            {
                for (int j = 0; j < labyrinth.GetLength(0); j++)
                {
                    if (labyrinth[j, i] == 1)
                    {
                        g.DrawImage(tileManager.getImage("Wall").bitmap, brickSize * i + leftMargin, brickSize * j + 2 * topMargin + brickSize/2, brickSize, brickSize);
                    }
                    else if (labyrinth[j, i] == 2)
                    {
                        g.DrawImage(tileManager.getImage("Wall").bitmap, brickSize * i + leftMargin, brickSize * j + 2 * topMargin + brickSize / 2, brickSize, brickSize);

                        using (Brush yellowBrush = new SolidBrush(Color.FromArgb(150, Color.Black)))
                        {
                            g.FillRectangle(yellowBrush, new Rectangle(brickSize * i + leftMargin, brickSize * j + 2 * topMargin + brickSize / 2, brickSize, brickSize));
                        }
                    }
                    else if (labyrinth[j, i] == 3)
                    {
                        //g.DrawImage(tileManager.getImage("Down1").bitmap, 96 * i + 20, 96 * j + 144);
                    }
                    else if (labyrinth[j, i] == 4)
                    {
                        g.DrawImage(tileManager.getImage("Gem").bitmap, brickSize * i + leftMargin + brickMiddle, brickSize * j + 2 * topMargin + brickSize / 2 + brickMiddle, brickSize / 2, brickSize / 2);
                    }
                    else if (labyrinth[j, i] == 5)
                    {
                        g.DrawImage(tileManager.getImage("MiniSlime").bitmap, brickSize * i + leftMargin + brickMiddle, brickSize * j + 2 * topMargin + brickSize / 2 + brickMiddle, brickSize / 2, brickSize / 2);
                    }
                }

            }

            //Display player, independant from the maze
            spriteState playerStatus = ((Controller)controller).GetPlayer().GetCurrentRenderInfo();

            g.DrawImage(tileManager.getImage(((Controller)controller).GetPlayer().GetImageName()).bitmap, playerStatus.spriteCoord.x, playerStatus.spriteCoord.y);
        }

        private void KeyDownEvent(object sender, PreviewKeyDownEventArgs e)
        {
            controller.ViewKeyPressedEvent(e);
        }

        private void CloseWindowEvent(object sender, FormClosingEventArgs e)
        {
            controller.ViewCloseEvent();
            Console.WriteLine("Close");
        }

        public TileManager GetTileManager() => tileManager;

        private void GameForm_SizeChanged(object sender, EventArgs e)
        {
            //Console.WriteLine("coco");
            displayHeight = oGameForm.Size.Height;
            displayWidth = oGameForm.Size.Width;
            minSize = Math.Min(displayHeight, displayWidth); // Smaller size is the priority
            brickSize = (int)((minSize / 600.0) * 50); // Adapting brick sizes
            leftMargin = (int)(brickSize / 2);
            topMargin = (int)(brickSize / 2);
            brickMiddle = (int)(brickSize / 4);

        }

    }
}