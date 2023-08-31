using GEI797Labo.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;


namespace GEI797Labo
{
    internal class GameView
    {
        private GameForm oGameForm; // Supposons que GameForm est une classe existante
        private TileManager tileManager;
        private Controller controller;
        private GameModel model;


        public GameView()
        {
            oGameForm = new GameForm();
            oGameForm.Paint += GameRenderer;
            oGameForm.PreviewKeyDown += KeyDownEvent;
            oGameForm.FormClosing += CloseWindowEvent;
            tileManager = new TileManager();
            controller = new Controller();
            model = new GameModel();

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

            g.DrawImage(tileManager.getImage("Title").bitmap, 20, 48);

            int[,] labyrinth = model.GetLabyrinth();


            for (int i = 0; i < labyrinth.GetLength(1); i++)
            {
                for (int j = 0; j < labyrinth.GetLength(0); j++)
                {
                    if (labyrinth[j,i] == 1)
                    {
                        g.DrawImage(tileManager.getImage("Wall").bitmap, 96 * i+20, 96*j+144);
                    }
                    else if (labyrinth[j, i] == 2)
                    {
                        g.DrawImage(tileManager.getImage("Wall").bitmap, 96 * i + 20, 96 * j + 144);
                        //TODO : modify transparency
                        using (Brush yellowBrush = new SolidBrush(Color.FromArgb(150, Color.Black)))
                        {
                            g.FillRectangle(yellowBrush, new Rectangle(96 * i + 20, 96 * j + 144, 96, 96));
                        }
                    }
                    else if (labyrinth[j, i] == 3)
                    {
                        g.DrawImage(tileManager.getImage("Down1").bitmap, 96 * i + 20, 96 * j + 144);
                    }
                    else if (labyrinth[j, i] == 4)
                    {
                        g.DrawImage(   tileManager.getImage("Gem").bitmap, 96 * i + 20 + 24, 96 * j + 144 + 24);
                    }
                    else if (labyrinth[j, i] == 5)
                    {
                        g.DrawImage(tileManager.getImage("MiniSlime").bitmap, 96 * i + 20 + 24, 96 * j + 144 + 24);
                    }
                }

            }
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

    }
}