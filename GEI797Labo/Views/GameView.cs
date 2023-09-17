using GEI797Labo.Controllers;
using GEI797Labo.Controllers.States;
using GEI797Labo.Models;
using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

/* EXPLORUS-E
 * Alexis BLATRIX (blaa1406)
 * Cédric CHARRON (chac0902)
 * Audric DAVID (dava1302)
 * Matthieu JEHANNE (jehm1701)
 * Cloé LEGLISE (legc1001)
 */

namespace GEI797Labo.Views
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
        private bool isFirstLoad = true;
        private int levelNumber = 1;
        private int initPosX;
        private int initPosY;
        private string statusText;
        private Thread windowThread;
     

        
        private int taskBarWidth; 
        private int menuItemWidth = 21; 
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


        public void SetLevelNumber(int i)
        {
            levelNumber = i;
        }
        public int GetLevelNumber()
        {
            return levelNumber;
        }

        public int GetInitPosX()
        {
            return initPosX;
        }
        public int GetInitPosY()
        {
            return initPosY;
        }

        

        public GameView(IController c)
        {
            controller = c;
            oGameForm = new GameForm();
            oGameForm.Paint += GameRenderer;
            oGameForm.PreviewKeyDown += KeyDownEvent;
            oGameForm.FormClosing += CloseWindowEvent;
            oGameForm.SizeChanged += SizeChangedEvent;
            oGameForm.LostFocus += LostFocusEvent;
            oGameForm.GotFocus += GotFocusEvent;
            oGameForm.Shown += FirstLoadEvent;
            tileManager = TileManager.GetInstance();


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


            
            g.DrawImage(tileManager.getImage("Title").bitmap, leftMargin + brickSize / 2, topMargin, brickSize * 2, brickSize / 2);

            g.DrawImage(tileManager.getImage("Heart").bitmap, beginTaskBar, topMargin, menuItemWidth, menuItemWidth);
            g.DrawImage(tileManager.getImage("BeginBar").bitmap, beginTaskBar + menuItemWidth * 1, topMargin, menuItemWidth, menuItemWidth);
            g.DrawImage(tileManager.getImage("RedFull").bitmap, beginTaskBar + menuItemWidth * 2, topMargin, menuItemWidth, menuItemWidth);
            g.DrawImage(tileManager.getImage("RedFull").bitmap, beginTaskBar + menuItemWidth * 3, topMargin, menuItemWidth, menuItemWidth);
            g.DrawImage(tileManager.getImage("RedFull").bitmap, beginTaskBar + menuItemWidth * 4, topMargin, menuItemWidth, menuItemWidth); 
            g.DrawImage(tileManager.getImage("EndBar").bitmap, beginTaskBar + menuItemWidth * 5, topMargin, menuItemWidth, menuItemWidth);

            g.DrawImage(tileManager.getImage("BigBubble").bitmap, beginTaskBar + menuItemWidth * 6, topMargin, menuItemWidth, menuItemWidth);
            g.DrawImage(tileManager.getImage("BeginBar").bitmap, beginTaskBar + menuItemWidth * 7, topMargin, menuItemWidth, menuItemWidth);
            g.DrawImage(tileManager.getImage("BlueFull").bitmap, beginTaskBar + menuItemWidth * 8, topMargin, menuItemWidth, menuItemWidth);
            g.DrawImage(tileManager.getImage("BlueFull").bitmap, beginTaskBar + menuItemWidth * 9, topMargin, menuItemWidth, menuItemWidth); 
            g.DrawImage(tileManager.getImage("BlueFull").bitmap, beginTaskBar + menuItemWidth * 10, topMargin, menuItemWidth, menuItemWidth);
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
                        initPosX = i - 1;
                        initPosY = j;
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


            using (Brush blackBrush = new SolidBrush(Color.Black))
            {
                e.Graphics.FillRectangle(blackBrush, new Rectangle(leftMargin + brickSize * 41/4, topMargin + brickSize * 62/50, brickSize * 11/20, brickSize * 11/20));
            }

            using (Font font = new Font("Arial", 16))
            using (Brush brush = new SolidBrush(Color.Yellow))
            {
                string pauseText = Convert.ToString(levelNumber);
                SizeF textSize = g.MeasureString(pauseText, font);
                float x = (leftMargin + brickSize * 41 / 4) + (brickSize * 11 / 20 - textSize.Width) / 2;
                float y = (topMargin + brickSize * 62 / 50) + (brickSize * 11 / 20 - textSize.Height) / 2;
                g.DrawString(pauseText, font, brush, x, y);
            }

            using (Brush blackBrush = new SolidBrush(Color.Black))
            {
                e.Graphics.FillRectangle(blackBrush, new Rectangle(leftMargin + brickSize / 3, topMargin + brickSize * 6 / 5, brickSize * 7 / 3, brickSize * 3 / 5));
            }
            using (Font font = new Font("Arial", 16))
            using (Brush brush = new SolidBrush(Color.Yellow))
            {
                SizeF textSize = g.MeasureString(statusText, font);
                float x = (leftMargin + brickSize / 3) + (brickSize * 7 / 3 - textSize.Width) / 2;
                float y = (topMargin + brickSize * 6 / 5) + (brickSize * 3 / 5 - textSize.Height) / 2;
                g.DrawString(statusText, font, brush, x, y);
            }

            if (controller.GetState() is PausedState)
            {
                statusText = "PAUSE";
            } 
            else if(controller.GetState() is TransitionState)
            {
                
                statusText = "Resume ("+((int)(4000-controller.GetTransitionTime())/1000).ToString()+")";
                 
            }
            else
            {
                statusText = "PLAY";
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

        private void LostFocusEvent(object sender, EventArgs e) // Not the "EVENT" in the ending of the method name
        {
            controller.ProcessLostFocus();
        }

        private void GotFocusEvent(object sender, EventArgs e) // Not the "EVENT" in the ending of the method name
        {
            if (isFirstLoad == true)
            {
                Console.WriteLine("Premier affichage de la fenêtre");
            }
            else
            {
                controller.EndProcessLostFocus();
            }
            
        }

        private void FirstLoadEvent(object sender, EventArgs e)
        {
            oGameForm.ActiveControl = null;
            isFirstLoad = false;
        }

        private void SizeChangedEvent(object sender, EventArgs e) // Not the "EVENT" in the ending of the method name
        {
            if (oGameForm.WindowState == FormWindowState.Minimized)
            {
                controller.ProcessMinimize();
            }

            else
            {
                //controller.EndProcessMinimize();
                int[,] labyrinth = controller.GetLabyrinth();
                displayHeight = oGameForm.Size.Height;
                displayWidth = oGameForm.Size.Width;
                minSize = Math.Min(displayHeight, displayWidth); // Smaller size is the priority
                brickSize = (int)((minSize / 600.0) * 50); // Adapting brick sizes
                leftMargin = (int)((displayWidth - labyrinth.GetLength(1) * (brickSize + 3 / 2)) / 2);
                topMargin = (int)((displayHeight - (labyrinth.GetLength(0) * (brickSize + 3 / 2) + brickSize * 3 / 2)) / 2);
                brickMiddle = (int)(brickSize / 4);


                beginTaskBar = brickSize * 2 + leftMargin + 2 * (brickSize / 2);
                afterTaskBar = brickSize / 2 + leftMargin;
                taskBarWidth = displayWidth - beginTaskBar - afterTaskBar; // Size of the taskbar (without the title, margins)
                menuItemWidth = (int)(taskBarWidth / 18); // Size of each "item" of the taskbar
                controller.PositionUpdate();



            }
        }




    }
}