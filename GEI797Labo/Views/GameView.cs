using GEI797Labo.Controllers;
using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;


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
        private int leftMargin = 19;
        private int topMargin = 33;
        private int brickMiddle = 12;
        private int gemCounter = 0;
        private bool endGame = false;
        private Thread windowThread;

        //Alexis Modification
        private int taskBarWidth; 
        private int menuItemWidth = 21; // (We take the taskBarWidth and we divide by the number of items or empty space of this taskbar). I count 18 items/empty space. Come to me if you have a question.
        private int beginTaskBar = 100+25+19+25;
        private int afterTaskBar = 25 + 19;


        public int GetTopMargin() 
        { 
            return topMargin; 
        }
        public int GetLeftMargin() 
        { 
            return leftMargin; 
        }
        public int GetBrickSize() 
        { 
            return brickSize; 
        }

        public void SetGemCounter(int i)
        {
            gemCounter=i;
        }
        public void SetEndgame(bool b)
        {
            endGame = b;
        }

        

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
            
            if (controller.IsPaused == true)
            {
                
                using (Font font = new Font("Arial", 24, FontStyle.Bold))
                using (Brush brush = new SolidBrush(Color.White))
                {
                    string pauseText = "Pause";
                    SizeF textSize = g.MeasureString(pauseText, font);
                    float x = (oGameForm.ClientSize.Width - textSize.Width) / 2;
                    float y = (oGameForm.ClientSize.Height - textSize.Height) / 2;
                    g.DrawString(pauseText, font, brush, x, y);
                }
            }
            else if (endGame)
            {
                using (Font font = new Font("Arial", 24, FontStyle.Bold))
                using (Brush brush = new SolidBrush(Color.White))
                {
                    string pauseText = "Congratulations!";
                    SizeF textSize = g.MeasureString(pauseText, font);
                    float x = (oGameForm.ClientSize.Width - textSize.Width) / 2;
                    float y = (oGameForm.ClientSize.Height - textSize.Height) / 2;
                    g.DrawString(pauseText, font, brush, x, y);
                }
            }
            else
            {
                g.DrawImage(tileManager.getImage("Title").bitmap, leftMargin + brickSize / 2, topMargin, brickSize * 2, brickSize / 2);

                g.DrawImage(tileManager.getImage("Heart").bitmap, beginTaskBar, topMargin, menuItemWidth, brickSize / 2);
                g.DrawImage(tileManager.getImage("BeginBar").bitmap, beginTaskBar + menuItemWidth * 1, topMargin, menuItemWidth, menuItemWidth);
                g.DrawImage(tileManager.getImage("RedFull").bitmap, beginTaskBar + menuItemWidth * 2, topMargin, menuItemWidth, menuItemWidth);
                g.DrawImage(tileManager.getImage("RedFull").bitmap, beginTaskBar + menuItemWidth * 3, topMargin, menuItemWidth, menuItemWidth);
                g.DrawImage(tileManager.getImage("RedFull").bitmap, beginTaskBar + menuItemWidth * 4, topMargin, menuItemWidth, menuItemWidth); 
                g.DrawImage(tileManager.getImage("EndBar").bitmap, beginTaskBar + menuItemWidth * 5, topMargin, menuItemWidth, menuItemWidth);

                g.DrawImage(tileManager.getImage("BigBubble").bitmap, beginTaskBar + menuItemWidth * 6, topMargin, menuItemWidth, menuItemWidth);
                g.DrawImage(tileManager.getImage("BeginBar").bitmap, beginTaskBar + menuItemWidth * 7, topMargin, menuItemWidth, menuItemWidth);
                g.DrawImage(tileManager.getImage("BlueFull").bitmap, beginTaskBar + menuItemWidth * 8, topMargin, menuItemWidth, menuItemWidth);
                g.DrawImage(tileManager.getImage("BlueFull").bitmap, beginTaskBar + menuItemWidth * 9, topMargin, menuItemWidth, menuItemWidth); 
                g.DrawImage(tileManager.getImage("EmptyBar").bitmap, beginTaskBar + menuItemWidth * 10, topMargin, menuItemWidth, menuItemWidth);
                g.DrawImage(tileManager.getImage("EndBar").bitmap, beginTaskBar + menuItemWidth * 11, topMargin, menuItemWidth, menuItemWidth);

                g.DrawImage(tileManager.getImage("Gem").bitmap, beginTaskBar + menuItemWidth * 12, topMargin, menuItemWidth, menuItemWidth);
                g.DrawImage(tileManager.getImage("BeginBar").bitmap, beginTaskBar + menuItemWidth * 13, topMargin, menuItemWidth, menuItemWidth);
                g.DrawImage(tileManager.getImage("EndBar").bitmap, beginTaskBar + menuItemWidth * 17, topMargin, menuItemWidth, menuItemWidth);

                if (gemCounter == 0)
                {
                    g.DrawImage(tileManager.getImage("EmptyBar").bitmap, beginTaskBar + menuItemWidth * 14, topMargin, menuItemWidth, menuItemWidth);
                    g.DrawImage(tileManager.getImage("EmptyBar").bitmap, beginTaskBar + menuItemWidth * 15, topMargin, menuItemWidth, menuItemWidth);
                    g.DrawImage(tileManager.getImage("EmptyBar").bitmap, beginTaskBar + menuItemWidth * 16, topMargin, menuItemWidth, menuItemWidth);
                }

                if (gemCounter == 1)
                {
                    g.DrawImage(tileManager.getImage("YellowFull").bitmap, beginTaskBar + menuItemWidth * 14, topMargin, menuItemWidth, menuItemWidth);
                    g.DrawImage(tileManager.getImage("EmptyBar").bitmap, beginTaskBar + menuItemWidth * 15, topMargin, menuItemWidth, menuItemWidth);
                    g.DrawImage(tileManager.getImage("EmptyBar").bitmap, beginTaskBar + menuItemWidth * 16, topMargin, menuItemWidth, menuItemWidth);
                }

                if (gemCounter == 2)
                {
                    g.DrawImage(tileManager.getImage("YellowFull").bitmap, beginTaskBar + menuItemWidth * 14, topMargin, menuItemWidth, menuItemWidth);
                    g.DrawImage(tileManager.getImage("YellowFull").bitmap, beginTaskBar + menuItemWidth * 15, topMargin, menuItemWidth, menuItemWidth);
                    g.DrawImage(tileManager.getImage("EmptyBar").bitmap, beginTaskBar + menuItemWidth * 16, topMargin, menuItemWidth, menuItemWidth);
                }

                if (gemCounter == 3)
                {
                    g.DrawImage(tileManager.getImage("YellowFull").bitmap, beginTaskBar + menuItemWidth * 14, topMargin, menuItemWidth, menuItemWidth);
                    g.DrawImage(tileManager.getImage("YellowFull").bitmap, beginTaskBar + menuItemWidth * 15, topMargin, menuItemWidth, menuItemWidth);
                    g.DrawImage(tileManager.getImage("YellowFull").bitmap, beginTaskBar + menuItemWidth * 16, topMargin, menuItemWidth, menuItemWidth);
                    g.DrawImage(tileManager.getImage("Key").bitmap, beginTaskBar + menuItemWidth * 17 + 10, topMargin, menuItemWidth, menuItemWidth);
                }




                //Calls Lab from another thread, lock may be needed
                int[,] labyrinth = controller.GetLabyrinth();


                for (int i = 0; i < labyrinth.GetLength(0); i++)
                {
                    for (int j = 0; j < labyrinth.GetLength(1); j++)
                    {
                        if (labyrinth[i, j] == 1)
                        {
                            g.DrawImage(tileManager.getImage("Wall").bitmap, brickSize * j + leftMargin, brickSize * i + topMargin + brickSize, brickSize, brickSize);
                        }
                        else if (labyrinth[i, j] == 2)
                        {
                            g.DrawImage(tileManager.getImage("Wall").bitmap, brickSize * j + leftMargin, brickSize * i + topMargin + brickSize, brickSize, brickSize);

                            using (Brush transparencyBrush = new SolidBrush(Color.FromArgb(150, Color.Black)))
                            {
                                g.FillRectangle(transparencyBrush, new Rectangle(brickSize * j + leftMargin, brickSize * i + topMargin + brickSize, brickSize, brickSize));
                            }
                        }
                        else if (labyrinth[i, j] == 3)
                        {

                        }
                        else if (labyrinth[i, j] == 4)
                        {
                            g.DrawImage(tileManager.getImage("Gem").bitmap, brickSize * j + leftMargin + brickMiddle, brickSize * i + topMargin + brickSize + brickMiddle, brickSize / 2, brickSize / 2);
                        }
                        else if (labyrinth[i, j] == 5)
                        {
                            g.DrawImage(tileManager.getImage("MiniSlime").bitmap, brickSize * j + leftMargin + brickMiddle, brickSize * i + topMargin + brickSize + brickMiddle, brickSize / 2, brickSize / 2);
                        }
                    }

                }

                //Display player, independant from the maze
                spriteState playerStatus = controller.GetPlayer().GetCurrentRenderInfo();

                g.DrawImage(tileManager.getImage(controller.GetPlayer().GetImageName()).bitmap, playerStatus.spriteCoord.x, playerStatus.spriteCoord.y, brickSize, brickSize);
            }
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
            displayHeight = oGameForm.Size.Height;
            displayWidth = oGameForm.Size.Width;
            minSize = Math.Min(displayHeight, displayWidth); // Smaller size is the priority
            brickSize = (int)((minSize / 600.0) * 50); // Adapting brick sizes
            leftMargin = (int)((displayWidth - labyrinth.GetLength(1)*(brickSize+3/2))/2);
            topMargin = (int)((displayHeight - (labyrinth.GetLength(0)*(brickSize+3/2) + brickSize * 3/2))/2) ;
            brickMiddle = (int)(brickSize / 4);

            // Partie modifiée Alexis
            beginTaskBar = brickSize * 2 + leftMargin + 2*(brickSize / 2); // GOOD
            afterTaskBar = brickSize / 2 + leftMargin;
            taskBarWidth = displayWidth - beginTaskBar - afterTaskBar; // Size of the taskbar (without the title, margins)
            menuItemWidth = (int)(taskBarWidth / 18); // Size of each "item" of the taskbar
            controller.PositionUpdate();
        }

    }
}