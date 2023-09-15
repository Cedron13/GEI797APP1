using System.Drawing;
using System.Windows.Forms;

/* EXPLORUS-E
 * Alexis BLATRIX (blaa1406)
 * Cédric CHARRON (chac0902)
 * Audric DAVID (dava1302)
 * Matthieu JEHANNE (jehm1701)
 * Cloé LEGLISE (legc1001) 
 */

namespace GEI797Labo
{
    public partial class GameForm : Form
    {
        private int beginWidth = 600;
        private int beginHeight = 600;
        public GameForm()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.KeyPreview = true;
            this.Size = new Size(beginWidth, beginHeight); //Init display dimensions 600*600 px
            this.MinimumSize = new Size(beginWidth, beginHeight);
            this.Text = "Explorus-E";

        }
    }
}
