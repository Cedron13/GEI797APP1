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
        private GameForm oGameForm; // Supposons que GameForm est une classe existante
        private TileManager tileManager;
        private IController controller;
        private int displaywidth;
        private int displayheight;
        private int tailletuile = 50;
        private int taillemin;
        private int leftmargin = 25;
        private int topmargin = 100;
        private int middletuile = 12;
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
            // Mettre l'arrière-plan en noir
            g.Clear(Color.Black);

            //g.DrawImage(tileManager.getImage("Title").bitmap, 20, 48);

            //Calls Lab from another thread, lock may be needed
            int[,] labyrinth = controller.GetLabyrinth();


            for (int i = 0; i < labyrinth.GetLength(1); i++)
            {
                for (int j = 0; j < labyrinth.GetLength(0); j++)
                {
                    if (labyrinth[j, i] == 1)
                    {
                        g.DrawImage(tileManager.getImage("Wall").bitmap, tailletuile * i + leftmargin, tailletuile * j + topmargin, tailletuile, tailletuile);
                    }
                    else if (labyrinth[j, i] == 2)
                    {
                        g.DrawImage(tileManager.getImage("Wall").bitmap, tailletuile * i + leftmargin, tailletuile * j + topmargin, tailletuile, tailletuile);
                        //TODO : modify transparency
                        using (Brush yellowBrush = new SolidBrush(Color.FromArgb(150, Color.Black)))
                        {
                            g.FillRectangle(yellowBrush, new Rectangle(tailletuile * i + leftmargin, tailletuile * j + topmargin, tailletuile, tailletuile));
                        }
                    }
                    else if (labyrinth[j, i] == 3)
                    {
                        //g.DrawImage(tileManager.getImage("Down1").bitmap, 96 * i + 20, 96 * j + 144);
                    }
                    else if (labyrinth[j, i] == 4)
                    {
                        g.DrawImage(tileManager.getImage("Gem").bitmap, tailletuile * i + leftmargin + middletuile, tailletuile * j + topmargin + middletuile, tailletuile / 2, tailletuile / 2);
                    }
                    else if (labyrinth[j, i] == 5)
                    {
                        g.DrawImage(tileManager.getImage("MiniSlime").bitmap, tailletuile * i + leftmargin + middletuile, tailletuile * j + topmargin + middletuile, tailletuile / 2, tailletuile / 2);
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
            displayheight = oGameForm.Size.Height;
            displaywidth = oGameForm.Size.Width;
            taillemin = Math.Min(displayheight, displaywidth); // On priorise la taille la plus faible
            tailletuile = (int)((taillemin / 600.0) * 50); // Adaptation taille des tuiles
            leftmargin = (int)(tailletuile / 2);
            topmargin = (int)(tailletuile * 2);
            middletuile = (int)(tailletuile / 4);

        }

    }
}