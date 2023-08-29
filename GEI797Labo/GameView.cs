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

            // Afficher un carré jaune de 20x20px
            using (Brush yellowBrush = new SolidBrush(Color.Yellow))
            {
                e.Graphics.FillRectangle(yellowBrush, new Rectangle((int)playerPosX, (int)playerPosY, 20, 20));
            }
            //e.Graphics.DrawImage(tileManager.getImage("Wall").bitmap, 0, 0);
        }
        public void moveRight(double elapsedTime)
        {
            playerPosX += elapsedTime/1000 * playerVelocity;
        }
    }
}