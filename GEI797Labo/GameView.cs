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
        private GameModel model;

        public GameView()
        {
            oGameForm = new GameForm();
            oGameForm.Paint += GameRenderer;
            tileManager = new TileManager();
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

            // Mettre l'arrière-plan en noir
            e.Graphics.Clear(Color.Black);

            e.Graphics.DrawImage(tileManager.getImage("Title").bitmap, 20, 48);

            int[,] labyrinth = model.GetLabyrinth();


            for (int i = 0; i < labyrinth.GetLength(1); i++)
            {
                for (int j = 0; j < labyrinth.GetLength(0); j++)
                {
                    if (labyrinth[j,i] == 1)
                    {
                        e.Graphics.DrawImage(tileManager.getImage("Wall").bitmap, 96 * i+20, 96*j+144);
                    }
                    else if (labyrinth[j, i] == 2)
                    {
                        e.Graphics.DrawImage(tileManager.getImage("Wall").bitmap, 96 * i + 20, 96 * j + 144);
                        //TODO : modify transparency
                        using (Brush yellowBrush = new SolidBrush(Color.FromArgb(150, Color.Black)))
                        {
                            e.Graphics.FillRectangle(yellowBrush, new Rectangle(96 * i + 20, 96 * j + 144, 96, 96));
                        }
                    }
                    else if (labyrinth[j, i] == 3)
                    {
                        e.Graphics.DrawImage(tileManager.getImage("Down1").bitmap, 96 * i + 20, 96 * j + 144);
                    }
                    else if (labyrinth[j, i] == 4)
                    {
                        e.Graphics.DrawImage(   tileManager.getImage("Gem").bitmap, 96 * i + 20 + 24, 96 * j + 144 + 24);
                    }
                    else if (labyrinth[j, i] == 5)
                    {
                        e.Graphics.DrawImage(tileManager.getImage("MiniSlime").bitmap, 96 * i + 20 + 24, 96 * j + 144 + 24);
                    }
                }

            }


        }
    }
}