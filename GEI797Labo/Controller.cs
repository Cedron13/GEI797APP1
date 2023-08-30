using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GEI797Labo
{
    internal class Controller
    {
        public Controller() { }

        public void SendKeyPressedEvent(PreviewKeyDownEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Down:
                    Console.WriteLine("Down");
                    break;
                case Keys.Up:
                    break;
                case Keys.Right:
                    break;
                case Keys.Left:
                    break;

            }
        }
        public void ResizeEvent()
        {

        }
        public void CloseEvent()
        {

        }
    }
}
