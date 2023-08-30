using System;
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
        private int[,]
            labyrinth =
                {
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, // 0 = Aucune image (zone de déplacement)
                {1, 4, 0, 0, 0, 4, 0, 0, 0, 1 }, // 1 = Afficher un mur
                {1, 0, 1, 1, 1, 0, 1, 1, 0, 1 }, // 2 = Afficher une porte
                {1, 3, 1, 5, 2, 0, 0, 0, 4, 1 }, // 3 = Afficher Slimus
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1 } // 4 = Afficher une gemme
                };                              // 5 = Afficher un mini-slime



        public GameView()
        {
            oGameForm = new GameForm();
            oGameForm.Paint += GameRenderer;
            tileManager = new TileManager();
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
            // Mettre l'arrière-plan en noir
            e.Graphics.Clear(Color.Black);

            /*
            // Afficher un carré jaune de 20x20px
            using (Brush yellowBrush = new SolidBrush(Color.Yellow))
            {
                e.Graphics.FillRectangle(yellowBrush, new Rectangle((int)playerPosX, (int)playerPosY, 20, 20));
            }
            */


            /*e.Graphics.DrawImage(tileManager.getImage("Right1").bitmap, 0, 0);*/
           
            for (int i = 0; i < labyrinth.GetLength(1); i++)
            {
                for (int j = 0; j < labyrinth.GetLength(0); j++)
                {
                    if (labyrinth[j,i] == 1)
                    {
                        e.Graphics.DrawImage(tileManager.getImage("Wall").bitmap, 96 * i, 96*j);
                    }
                    else if (labyrinth[j, i] == 2)
                    {
                        e.Graphics.DrawImage(tileManager.getImage("Wall").bitmap, 96 * i, 96 * j); //TODO : modify transparency
                    }
                    else if (labyrinth[j, i] == 3)
                    {
                        e.Graphics.DrawImage(tileManager.getImage("Down1").bitmap, 96 * i, 96 * j);
                    }
                    else if (labyrinth[j, i] == 4)
                    {
                        e.Graphics.DrawImage(tileManager.getImage("Gem").bitmap, 96 * i + 24, 96 * j + 24);
                    }
                    else if (labyrinth[j, i] == 5)
                    {
                        e.Graphics.DrawImage(tileManager.getImage("MiniSlime").bitmap, 96 * i + 24, 96 * j + 24);
                    }
                }

            }


        }
        public void moveRight(double elapsedTime)
        {
            playerPosX += elapsedTime / 1000 * playerVelocity;
        }
    }
}