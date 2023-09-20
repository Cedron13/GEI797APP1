/* EXPLORUS-E
 * Alexis BLATRIX (blaa1406)
 * Cédric CHARRON (chac0902)
 * Audric DAVID (dava1302)
 * Matthieu JEHANNE (jehm1701)
 * Cloé LEGLISE (legc1001)
 */

namespace ExplorusE.Constants
{
    public struct RenderData
    {
        public int[,] lab;
    }

    public struct coord
    {
        public int x { get; set; }
        public int y { get; set; }
    }

    public struct coordF //float version of coord
    {
        public double x { get; set; }
        public double y { get; set; }
    }

    public struct spriteState
    {
        public int imageIndex { get; set; }
        public coord spriteCoord { get; set; }
    }
}
