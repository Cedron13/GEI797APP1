﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace GEI797Labo
{
    internal class GameView
    {
        private GameForm oGameForm; // Supposons que GameForm est une classe existante
        private double playerPosX = 0;
        private double playerPosY = 0;
        private float playerVelocity = 10; // in pixels per second
        private TileManager tileManager;
        private Controller controller;

        public GameView()
        {
            oGameForm = new GameForm();
            oGameForm.Paint += GameRenderer;
            oGameForm.PreviewKeyDown += KeyDownEvent;
            oGameForm.FormClosing += CloseWindowEvent;
            tileManager = new TileManager();
            controller = new Controller();
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

            // Afficher un carré jaune de 20x20px
            using (Brush yellowBrush = new SolidBrush(Color.Yellow))
            {
                e.Graphics.FillRectangle(yellowBrush, new Rectangle((int)playerPosX, (int)playerPosY, 20, 20));
            }
            g.DrawImage(tileManager.getImage("Wall").bitmap, 0, 0);
        }

        private void KeyDownEvent(object sender, PreviewKeyDownEventArgs e)
        {
            controller.SendKeyPressedEvent(e);
        }

        private void CloseWindowEvent(object sender, FormClosingEventArgs e)
        {
            controller.CloseEvent();
            Console.WriteLine("Close");
        }

        public void moveRight(double elapsedTime)
        {
            playerPosX += elapsedTime/1000 * playerVelocity;
        }
    }
}