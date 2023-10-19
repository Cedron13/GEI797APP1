/* EXPLORUS-E
 * Alexis BLATRIX (blaa1406)
 * Cédric CHARRON (chac0902)
 * Audric DAVID (dava1302)
 * Matthieu JEHANNE (jehm1701)
 * Cloé LEGLISE (legc1001)
 */

namespace ExplorusE.Constants
{
    public enum GameStates
    {
        UNKNOWN,
        PLAY,
        PAUSE,
        RESUME,
        STOP,
        MENU,
        HELP
    }

    public enum ImageType
    {
        Wall,
        Player,
        Collectible,
        Other
    }

    public enum Direction
    {
        DOWN,
        RIGHT,
        UP,
        LEFT,
        IDLE
    }

    public enum RenderItemType
    {
        Permanent,
        NonPermanent
    }

    public enum BarType
    {
        HEALTH,
        BUBBLE,
        COIN
    }

    public enum SpriteType
    {
        PLAYER,
        ENEMY,
        BUBBLE
    }
}
