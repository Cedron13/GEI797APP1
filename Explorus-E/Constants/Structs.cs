/* EXPLORUS-E
 * Alexis BLATRIX (blaa1406)
 * Cédric CHARRON (chac0902)
 * Audric DAVID (dava1302)
 * Matthieu JEHANNE (jehm1701)
 * Cloé LEGLISE (legc1001)
 */

using ExplorusE.Models;

namespace ExplorusE.Constants
{
    internal struct RenderData
    {
        public int[,] lab;
    }

    internal struct coord
    {
        public int x { get; set; }
        public int y { get; set; }
    }

    internal struct coordF //float version of coord
    {
        public double x { get; set; }
        public double y { get; set; }
    }

    internal struct spriteState
    {
        public int imageIndex { get; set; }
        public coord spriteCoord { get; set; }
    }

    internal struct RenderElement
    {
        public Renderable element { get; set; }
        public RenderItemType type { get; set; }
    }
}
