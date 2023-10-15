using ExplorusE.Constants;
using ExplorusE.Controllers;
using ExplorusE.Controllers.States;
using ExplorusE.Models;
using ExplorusE.Threads;
using System;
using System.Collections.Generic;
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

namespace ExplorusE.Views
{
    internal class GameView
    {
        private GameForm oGameForm;
        private TileManager tileManager;
        private IControllerView controller;
        private int displayWidth;
        private int displayHeight;
        private int brickSize = (int)(600.0 / 900.0 * 50);
        private int minSize;
        private int leftMargin = 11;
        private int topMargin = 20;
        private int brickMiddle = (int)((600.0 / 900.0 * 50)/4);
        private int gemCounter = 0;
        private bool isFirstLoad = true;
        private int levelNumber = 1;
        private int initPosX;
        private int initPosY;
        private int playerLives = 3;
        private List<coord> toxicCoord = new List<coord>();
        private string statusText;
        private Thread windowThread;
        private coord coordbubble = new coord();
        private bool isReloading = false;
        private double reloadTime = 0;
        


        
        private int taskBarWidth; 
        private int menuItemWidth = 25; 
        private int beginTaskBar = 109;
        private int afterTaskBar = 25 + 19;

        private bool bubbleshoot = false;

        private RenderThread render;
        
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

        public void SetPlayerLives(int i)
        {
            playerLives = i;
        }

        public bool GetIsRealoding()
        {
            return isReloading;
        }

        public void SetIsReloading(bool value)
        {
            isReloading = value;
        }

        public void SetReloadTime(double value)
        {
            reloadTime = value;
        }
        public double GetReloadTime()
        {
            return reloadTime;
        }

        public GameView(IControllerView c, RenderThread r)
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
            render = r;

            windowThread = new Thread(new ThreadStart(Show)); //New thread because "Application.run()" blocks the actual thread and prevents the engine to run
            windowThread.Name = "Window Thread";
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


        private void TaskBarDisplay(Graphics g)
        {
            // Black background and menu
            //g.Clear(Color.Black);

            g.DrawImage(tileManager.getImage("Title").bitmap, leftMargin + brickSize / 2, topMargin, brickSize * 2, brickSize / 2);


            
                g.DrawImage(tileManager.getImage("Heart").bitmap, beginTaskBar, topMargin, menuItemWidth, menuItemWidth);
                g.DrawImage(tileManager.getImage("BeginBar").bitmap, beginTaskBar + menuItemWidth * 1, topMargin, menuItemWidth, menuItemWidth);
                g.DrawImage(tileManager.getImage("RedFull").bitmap, beginTaskBar + menuItemWidth * 2, topMargin, menuItemWidth, menuItemWidth);
                g.DrawImage(tileManager.getImage("RedFull").bitmap, beginTaskBar + menuItemWidth * 3, topMargin, menuItemWidth, menuItemWidth);
                g.DrawImage(tileManager.getImage("RedFull").bitmap, beginTaskBar + menuItemWidth * 4, topMargin, menuItemWidth, menuItemWidth);
                g.DrawImage(tileManager.getImage("EndBar").bitmap, beginTaskBar + menuItemWidth * 5, topMargin, menuItemWidth, menuItemWidth);

                g.DrawImage(tileManager.getImage("Bubble1").bitmap, beginTaskBar + menuItemWidth * 6, topMargin, menuItemWidth, menuItemWidth);
                g.DrawImage(tileManager.getImage("EmptyBar").bitmap, beginTaskBar + menuItemWidth * 8, topMargin, menuItemWidth, menuItemWidth);//
                g.DrawImage(tileManager.getImage("EmptyBar").bitmap, beginTaskBar + menuItemWidth * 9, topMargin, menuItemWidth, menuItemWidth);//
                g.DrawImage(tileManager.getImage("EmptyBar").bitmap, beginTaskBar + menuItemWidth * 10, topMargin, menuItemWidth, menuItemWidth);//

            g.DrawImage(tileManager.getImage("BeginBar").bitmap, beginTaskBar + menuItemWidth * 7, topMargin, menuItemWidth, menuItemWidth);
            if (isReloading == false)
            {
                
                g.DrawImage(tileManager.getImage("BlueFull").bitmap, beginTaskBar + menuItemWidth * 8, topMargin, menuItemWidth, menuItemWidth);
                g.DrawImage(tileManager.getImage("BlueFull").bitmap, beginTaskBar + menuItemWidth * 9, topMargin, menuItemWidth, menuItemWidth);
                g.DrawImage(tileManager.getImage("BlueFull").bitmap, beginTaskBar + menuItemWidth * 10, topMargin, menuItemWidth, menuItemWidth);
                

             
            }
            else if (isReloading == true)
            {
                if (reloadTime == 0)
                {
                    g.DrawImage(tileManager.getImage("EmptyBar").bitmap, beginTaskBar + menuItemWidth * 8, topMargin, menuItemWidth, menuItemWidth);
                    g.DrawImage(tileManager.getImage("EmptyBar").bitmap, beginTaskBar + menuItemWidth * 9, topMargin, menuItemWidth, menuItemWidth);
                    g.DrawImage(tileManager.getImage("EmptyBar").bitmap, beginTaskBar + menuItemWidth * 10, topMargin, menuItemWidth, menuItemWidth);
                }
                else if (reloadTime > 200 && reloadTime < 400)
                {
                    g.DrawImage(tileManager.getImage("BlueHalf").bitmap, beginTaskBar + menuItemWidth * 8, topMargin, menuItemWidth, menuItemWidth);
                    g.DrawImage(tileManager.getImage("EmptyBar").bitmap, beginTaskBar + menuItemWidth * 9, topMargin, menuItemWidth, menuItemWidth);
                    g.DrawImage(tileManager.getImage("EmptyBar").bitmap, beginTaskBar + menuItemWidth * 10, topMargin, menuItemWidth, menuItemWidth);
                }
                else if (reloadTime > 400 && reloadTime < 600)
                {
                    g.DrawImage(tileManager.getImage("BlueFull").bitmap, beginTaskBar + menuItemWidth * 8, topMargin, menuItemWidth, menuItemWidth);
                    g.DrawImage(tileManager.getImage("EmptyBar").bitmap, beginTaskBar + menuItemWidth * 9, topMargin, menuItemWidth, menuItemWidth);
                    g.DrawImage(tileManager.getImage("EmptyBar").bitmap, beginTaskBar + menuItemWidth * 10, topMargin, menuItemWidth, menuItemWidth);
                }
                else if (reloadTime > 600 && reloadTime < 800)
                {
                    g.DrawImage(tileManager.getImage("BlueFull").bitmap, beginTaskBar + menuItemWidth * 8, topMargin, menuItemWidth, menuItemWidth);
                    g.DrawImage(tileManager.getImage("BlueHalf").bitmap, beginTaskBar + menuItemWidth * 9, topMargin, menuItemWidth, menuItemWidth);
                    g.DrawImage(tileManager.getImage("EmptyBar").bitmap, beginTaskBar + menuItemWidth * 10, topMargin, menuItemWidth, menuItemWidth);
                }
                else if (reloadTime > 800 && reloadTime < 1000)
                {
                    g.DrawImage(tileManager.getImage("BlueFull").bitmap, beginTaskBar + menuItemWidth * 8, topMargin, menuItemWidth, menuItemWidth);
                    g.DrawImage(tileManager.getImage("BlueFull").bitmap, beginTaskBar + menuItemWidth * 9, topMargin, menuItemWidth, menuItemWidth);
                    g.DrawImage(tileManager.getImage("EmptyBar").bitmap, beginTaskBar + menuItemWidth * 10, topMargin, menuItemWidth, menuItemWidth);
                }
                else if (reloadTime > 1000 && reloadTime < 1200)
                {
                    g.DrawImage(tileManager.getImage("BlueFull").bitmap, beginTaskBar + menuItemWidth * 8, topMargin, menuItemWidth, menuItemWidth);
                    g.DrawImage(tileManager.getImage("BlueFull").bitmap, beginTaskBar + menuItemWidth * 9, topMargin, menuItemWidth, menuItemWidth);
                    g.DrawImage(tileManager.getImage("BlueHalf").bitmap, beginTaskBar + menuItemWidth * 10, topMargin, menuItemWidth, menuItemWidth);
                }
                else if (reloadTime > 1200)
                {
                    g.DrawImage(tileManager.getImage("BlueFull").bitmap, beginTaskBar + menuItemWidth * 8, topMargin, menuItemWidth, menuItemWidth);
                    g.DrawImage(tileManager.getImage("BlueFull").bitmap, beginTaskBar + menuItemWidth * 9, topMargin, menuItemWidth, menuItemWidth);
                    g.DrawImage(tileManager.getImage("BlueFull").bitmap, beginTaskBar + menuItemWidth * 10, topMargin, menuItemWidth, menuItemWidth);
                }




            }
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

            else if (gemCounter == 1)
            {
                g.DrawImage(tileManager.getImage("YellowHalf").bitmap, beginTaskBar + menuItemWidth * 14, topMargin, menuItemWidth, menuItemWidth);
                g.DrawImage(tileManager.getImage("EmptyBar").bitmap, beginTaskBar + menuItemWidth * 15, topMargin, menuItemWidth, menuItemWidth);
                g.DrawImage(tileManager.getImage("EmptyBar").bitmap, beginTaskBar + menuItemWidth * 16, topMargin, menuItemWidth, menuItemWidth);
            }

            else if (gemCounter == 2)
            {
                g.DrawImage(tileManager.getImage("YellowFull").bitmap, beginTaskBar + menuItemWidth * 14, topMargin, menuItemWidth, menuItemWidth);
                g.DrawImage(tileManager.getImage("EmptyBar").bitmap, beginTaskBar + menuItemWidth * 15, topMargin, menuItemWidth, menuItemWidth);
                g.DrawImage(tileManager.getImage("EmptyBar").bitmap, beginTaskBar + menuItemWidth * 16, topMargin, menuItemWidth, menuItemWidth);
            }

            else if (gemCounter == 3)
            {
                g.DrawImage(tileManager.getImage("YellowFull").bitmap, beginTaskBar + menuItemWidth * 14, topMargin, menuItemWidth, menuItemWidth);
                g.DrawImage(tileManager.getImage("YellowHalf").bitmap, beginTaskBar + menuItemWidth * 15, topMargin, menuItemWidth, menuItemWidth);
                g.DrawImage(tileManager.getImage("EmptyBar").bitmap, beginTaskBar + menuItemWidth * 16, topMargin, menuItemWidth, menuItemWidth);
            }

            else if (gemCounter == 4)
            {
                g.DrawImage(tileManager.getImage("YellowFull").bitmap, beginTaskBar + menuItemWidth * 14, topMargin, menuItemWidth, menuItemWidth);
                g.DrawImage(tileManager.getImage("YellowFull").bitmap, beginTaskBar + menuItemWidth * 15, topMargin, menuItemWidth, menuItemWidth);
                g.DrawImage(tileManager.getImage("EmptyBar").bitmap, beginTaskBar + menuItemWidth * 16, topMargin, menuItemWidth, menuItemWidth);
            }

            else if (gemCounter == 5)
            {
                g.DrawImage(tileManager.getImage("YellowFull").bitmap, beginTaskBar + menuItemWidth * 14, topMargin, menuItemWidth, menuItemWidth);
                g.DrawImage(tileManager.getImage("YellowFull").bitmap, beginTaskBar + menuItemWidth * 15, topMargin, menuItemWidth, menuItemWidth);
                g.DrawImage(tileManager.getImage("YellowHalf").bitmap, beginTaskBar + menuItemWidth * 16, topMargin, menuItemWidth, menuItemWidth);
            }

            else if (gemCounter == 6)
            {
                g.DrawImage(tileManager.getImage("YellowFull").bitmap, beginTaskBar + menuItemWidth * 14, topMargin, menuItemWidth, menuItemWidth);
                g.DrawImage(tileManager.getImage("YellowFull").bitmap, beginTaskBar + menuItemWidth * 15, topMargin, menuItemWidth, menuItemWidth);
                g.DrawImage(tileManager.getImage("YellowFull").bitmap, beginTaskBar + menuItemWidth * 16, topMargin, menuItemWidth, menuItemWidth);
                g.DrawImage(tileManager.getImage("Key").bitmap, beginTaskBar + menuItemWidth * 17 + 10, topMargin, menuItemWidth, menuItemWidth);
            }
        }

        private void LabyrinthDisplay(Graphics g)
        {
            //Calls Lab from another thread, lock may be needed
            int[,] labyrinth = controller.GetLabyrinth();


            for (int i = 0; i < labyrinth.GetLength(0); i++)
            {
                for (int j = 0; j < labyrinth.GetLength(1); j++)
                {
                    if (labyrinth[i, j] == 1)
                    {
                        //g.DrawImage(tileManager.getImage("Wall").bitmap, brickSize * j + leftMargin, brickSize * i + topMargin + brickSize, brickSize, brickSize);
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
                        coord c = new coord
                        {
                            x = i,
                            y = j
                        };
                        ToxicDisplay(g, c);
                    }
                    else if (labyrinth[i, j] == 5)
                    {
                        g.DrawImage(tileManager.getImage("MiniSlime").bitmap, brickSize * j + leftMargin + brickMiddle, brickSize * i + topMargin + brickSize + brickMiddle, brickSize / 2, brickSize / 2);
                    }
                }

            }
        }

        private void PlayerDisplay(Graphics g)
        {
            //Display player, independant from the maze
            spriteState playerStatus = controller.GetPlayer().GetCurrentRenderInfo();

            //g.DrawImage(tileManager.getImage(controller.GetPlayer().GetImageName()).bitmap, playerStatus.spriteCoord.x, playerStatus.spriteCoord.y, brickSize, brickSize);
        }

        private void ToxicDisplay(Graphics g, coord c)
        {
            ToxicSprite t = new ToxicSprite(c, topMargin, leftMargin, brickSize);
            g.DrawImage(tileManager.getImage("ToxicDown1").bitmap, brickSize * c.y + leftMargin, brickSize * c.x  + topMargin + brickSize, brickSize, brickSize);
           
        }


        private void BubbleDisplay(Graphics g,BubbleSprite bubble)
        {
            spriteState bubbleStatus = bubble.GetCurrentRenderInfo();
            //Console.WriteLine(bubbleStatus.spriteCoord.x);
            // BubbleSprite b = new BubbleSprite(coordbubble, topMargin, leftMargin, brickSize);
            g.DrawImage(tileManager.getImage("Bubble2").bitmap, (int)(brickSize * bubble.GetGridPosition().x + leftMargin), (int)(brickSize * bubble.GetGridPosition().y + topMargin + brickSize), brickSize, brickSize);
            //g.DrawImage(tileManager.getImage("Bubble2").bitmap, bubbleStatus.spriteCoord.x, bubbleStatus.spriteCoord.y, brickSize, brickSize);
        }
        






        private void StatusBarDisplay(Graphics g, PaintEventArgs e)
        {
            using (Brush blackBrush = new SolidBrush(Color.Black))
            {
                e.Graphics.FillRectangle(blackBrush, new Rectangle(leftMargin + brickSize * 41 / 4, topMargin + brickSize * 62 / 50, brickSize * 11 / 20, brickSize * 11 / 20));
            }

            using (Font font = new Font("Arial", 14))
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

            //Selection of the text displayed
            if (controller.GetState() is PausedState) statusText = "PAUSE";
            else if (controller.GetState() is ResumeState) statusText = "Resume (" + ((int)(4000 - controller.GetTransitionTime()) / 1000).ToString() + ")";
            else if (controller.GetState() is StopState) statusText = "VICTORY";
            else statusText = "PLAY";

            using (Font font = new Font("Arial", 14))
            using (Brush brush = new SolidBrush(Color.Yellow))
            {
                SizeF textSize = g.MeasureString(statusText, font);
                float x = (leftMargin + brickSize / 3) + (brickSize * 7 / 3 - textSize.Width) / 2;
                float y = (topMargin + brickSize * 6 / 5) + (brickSize * 3 / 5 - textSize.Height) / 2;
                g.DrawString(statusText, font, brush, x, y);
            }
        }

        private void GameRenderer(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(Color.Black);

            //TODO: Change this method to loop with the RenderThread object
            List<Renderable> renderPermanentItems = render.GetPermanentItems();
            foreach (Renderable r in renderPermanentItems) r.Render(g);

            List<Renderable> renderItems = render.GetItems();
            foreach (Renderable r in renderItems) r.Render(g);


            //Old ways to render, TODO: remove all
            TaskBarDisplay(g);
            LabyrinthDisplay(g); //Walls are rendered by the new way
            //PlayerDisplay(g);
            StatusBarDisplay(g, e);
            /*
            if (controller.GetBubbles().Count !=0)
            {
                foreach (BubbleSprite element in controller.GetBubbles())
                {
                    BubbleDisplay(g, element);
                }
            }
            */
                       
        }

        private void KeyDownEvent(object sender, PreviewKeyDownEventArgs e)
        {
            controller.ViewKeyPressedEvent(e);
        }

        private void CloseWindowEvent(object sender, FormClosingEventArgs e)
        {
            controller.ViewCloseEvent();
        }

        private void LostFocusEvent(object sender, EventArgs e)
        {
            controller.ProcessLostFocus();
        }

        private void GotFocusEvent(object sender, EventArgs e)
        {
            if (!isFirstLoad) controller.EndProcessLostFocus(); //Prevent the game to pause on start up          
        }

        private void FirstLoadEvent(object sender, EventArgs e)
        {
            oGameForm.ActiveControl = null;
            isFirstLoad = false;
        }

        private void SizeChangedEvent(object sender, EventArgs e)
        {
            if (oGameForm.WindowState == FormWindowState.Minimized)
            {
                controller.ProcessMinimize();
            }
            else
            {
                int[,] labyrinth = controller.GetLabyrinth();
                displayHeight = oGameForm.Size.Height;
                displayWidth = oGameForm.Size.Width;
                minSize = Math.Min(displayHeight, displayWidth); // Smaller size is the priority
                brickSize = (int)((minSize / 900.0) * 50); // Adapting brick sizes
                leftMargin = (int)((displayWidth - labyrinth.GetLength(1) * (brickSize + 3 / 2)) / 2);
                topMargin = (int)((displayHeight - (labyrinth.GetLength(0) * (brickSize + 3 / 2) + brickSize * 3 / 2)) / 2);
                brickMiddle = (int)(brickSize / 4);



                beginTaskBar = brickSize * 2 + leftMargin + 2 * (brickSize / 2);
                afterTaskBar = brickSize / 2 + leftMargin;
                taskBarWidth = displayWidth - beginTaskBar - afterTaskBar; // Size of the taskbar (without the title, margins)
                menuItemWidth = (int)(taskBarWidth / 18); // Size of each "item" of the taskbar

                Console.WriteLine("menuItemWidth = " + menuItemWidth + ", beginTaskBar = " + beginTaskBar + ", afterTaskBar = ", afterTaskBar);
                controller.PositionUpdate();
            }

            //TODO: Reset permament items for RenderThread (all sizes have changed, so do the items) and refill the permanent list
        }

       


    }
}