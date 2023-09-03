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

            g.DrawImage(tileManager.getImage("Title").bitmap, leftMargin + brickSize /2 , topMargin, brickSize * 2, brickSize / 2);

            g.DrawImage(tileManager.getImage("Heart").bitmap, leftMargin + brickSize * 12 /4, topMargin, brickSize / 2, brickSize / 2);
            g.DrawImage(tileManager.getImage("BeginBar").bitmap, leftMargin + brickSize * 13/4, topMargin, brickSize / 2, brickSize / 2);
            g.DrawImage(tileManager.getImage("RedFull").bitmap, leftMargin + brickSize * 15/4, topMargin, brickSize / 2, brickSize / 2);
            g.DrawImage(tileManager.getImage("RedHalf").bitmap, leftMargin + brickSize * 17/4, topMargin, brickSize / 2, brickSize / 2);
            g.DrawImage(tileManager.getImage("EndBar").bitmap, leftMargin + brickSize * 19/4, topMargin, brickSize / 2, brickSize / 2);

            g.DrawImage(tileManager.getImage("BigBubble").bitmap, leftMargin + brickSize * 22/4, topMargin, brickSize / 2, brickSize / 2);
            g.DrawImage(tileManager.getImage("BeginBar").bitmap, leftMargin + brickSize * 23/4, topMargin, brickSize / 2, brickSize / 2);
            g.DrawImage(tileManager.getImage("BlueFull").bitmap, leftMargin + brickSize * 25/4, topMargin, brickSize / 2, brickSize / 2);
            g.DrawImage(tileManager.getImage("BlueHalf").bitmap, leftMargin + brickSize * 27/4, topMargin, brickSize / 2, brickSize / 2);
            g.DrawImage(tileManager.getImage("EndBar").bitmap, leftMargin + brickSize * 29/4, topMargin, brickSize / 2, brickSize / 2);

            g.DrawImage(tileManager.getImage("Gem").bitmap, leftMargin + brickSize * 32/4, topMargin, brickSize / 2, brickSize / 2);
            g.DrawImage(tileManager.getImage("BeginBar").bitmap, leftMargin + brickSize * 33/4, topMargin, brickSize / 2, brickSize / 2);
            g.DrawImage(tileManager.getImage("YellowFull").bitmap, leftMargin + brickSize * 35/4, topMargin, brickSize / 2, brickSize / 2);
            g.DrawImage(tileManager.getImage("YellowHalf").bitmap, leftMargin + brickSize * 37/4, topMargin, brickSize / 2, brickSize / 2);
            g.DrawImage(tileManager.getImage("EndBar").bitmap, leftMargin + brickSize * 39/4, topMargin, brickSize / 2, brickSize / 2);

            g.DrawImage(tileManager.getImage("Key").bitmap, leftMargin + brickSize * 40/4, topMargin, brickSize / 2, brickSize / 2);


            //Calls Lab from another thread, lock may be needed
            int[,] labyrinth = controller.GetLabyrinth();


            for (int i = 0; i < labyrinth.GetLength(1); i++)
            {
                for (int j = 0; j < labyrinth.GetLength(0); j++)
                {
                    if (labyrinth[j, i] == 1)
                    {
                        g.DrawImage(tileManager.getImage("Wall").bitmap, brickSize * i + leftMargin, brickSize * j + topMargin + brickSize, brickSize, brickSize);
                    }
                    else if (labyrinth[j, i] == 2)
                    {
                        g.DrawImage(tileManager.getImage("Wall").bitmap, brickSize * i + leftMargin, brickSize * j + topMargin + brickSize, brickSize, brickSize);

                        using (Brush yellowBrush = new SolidBrush(Color.FromArgb(150, Color.Black)))
                        {
                            g.FillRectangle(yellowBrush, new Rectangle(brickSize * i + leftMargin, brickSize * j + topMargin + brickSize, brickSize, brickSize));
                        }
                    }
                    else if (labyrinth[j, i] == 3)
                    {
                        //g.DrawImage(tileManager.getImage("Down1").bitmap, 96 * i + 20, 96 * j + 144);
                    }
                    else if (labyrinth[j, i] == 4)
                    {
                        g.DrawImage(tileManager.getImage("Gem").bitmap, brickSize * i + leftMargin + brickMiddle, brickSize * j + topMargin + brickSize + brickMiddle, brickSize / 2, brickSize / 2);
                    }
                    else if (labyrinth[j, i] == 5)
                    {
                        g.DrawImage(tileManager.getImage("MiniSlime").bitmap, brickSize * i + leftMargin + brickMiddle, brickSize * j + topMargin + brickSize + brickMiddle, brickSize / 2, brickSize / 2);
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

            int[,] labyrinth = controller.GetLabyrinth();
            //Console.WriteLine("coco");
            displayHeight = oGameForm.Size.Height;
            displayWidth = oGameForm.Size.Width;
            minSize = Math.Min(displayHeight, displayWidth); // Smaller size is the priority
            brickSize = (int)((minSize / 600.0) * 50); // Adapting brick sizes
            leftMargin = (int)((displayWidth - labyrinth.GetLength(1)*(brickSize+3/2))/2);
            topMargin = (int)((displayHeight - (labyrinth.GetLength(0)*(brickSize+3/2) + brickSize * 3/2))/2) ;
            brickMiddle = (int)(brickSize / 4);

        }

    }
}