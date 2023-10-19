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
        private int lives;
        private int fps;
        private bool fpsDisplay = false;

        private int taskBarWidth; 
        private int menuItemWidth = 25; 
        private int beginTaskBar = 109;
        private int afterTaskBar = 25 + 19;

        private bool bubbleshoot = false;

        private IRenderListReader items;
        private IRenderListReader permanentItems;
        
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
        public void SetLives(int value)
        {
            lives = value;
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

        public void SetFPS(float value)
        {
            fps = (int)value;
        }

        public bool GetFpsDisplay()
        {
            return fpsDisplay;
        }

        public void SetFpsDisplay(bool value)
        {
            fpsDisplay = value;
        }

        public GameView(IControllerView c, IRenderListReader i, IRenderListReader pi)
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
            items = i;
            permanentItems = pi;

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

        private void GameRenderer(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(Color.Black);

            //TODO: Change this method to loop with the RenderThread object
            List<Renderable> renderPermanentItems = permanentItems.GetList();
            foreach (Renderable r in renderPermanentItems) r.Render(g);

            List<Renderable> renderItems = items.Flush();
            foreach (Renderable r in renderItems) r.Render(g);

            if (fpsDisplay) { oGameForm.Text = "Explorus-E   -   FPS = " + fps.ToString(); }
            else { oGameForm.Text = "Explorus-E"; }

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

                //Console.WriteLine("menuItemWidth = " + menuItemWidth + ", beginTaskBar = " + beginTaskBar + ", afterTaskBar = ", afterTaskBar);
                controller.PositionUpdate();
            }

            //TODO: Reset permament items for RenderThread (all sizes have changed, so do the items) and refill the permanent list
        }

    }
}