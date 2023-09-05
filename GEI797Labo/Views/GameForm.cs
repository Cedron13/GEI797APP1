using System.Drawing;
using System.Windows.Forms;

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
            this.Size = new Size(beginWidth, beginHeight); //Init display dimensions 600*600
            this.MinimumSize = new Size(beginWidth, beginHeight);
            this.Text = "Explorus-E";

        }
    }
}
