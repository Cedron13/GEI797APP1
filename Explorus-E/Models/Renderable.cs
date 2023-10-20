using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* EXPLORUS-E
 * Alexis BLATRIX (blaa1406)
 * Cédric CHARRON (chac0902)
 * Audric DAVID (dava1302)
 * Matthieu JEHANNE (jehm1701)
 * Cloé LEGLISE (legc1001)
 */

namespace ExplorusE.Models
{
    internal interface Renderable
    {
        /// <summary>
        /// Uses the g parameter to draw the item on screen
        /// </summary>
        /// <param name="g">Graphics object from Window</param>
        void Render(Graphics g);

        /// <summary>
        /// Makes a copy of the object
        /// </summary>
        /// <returns>Copy of the object</returns>
        Renderable CopyForRender();
    }
}
