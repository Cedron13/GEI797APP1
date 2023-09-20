/* EXPLORUS-E
 * Alexis BLATRIX (blaa1406)
 * Cédric CHARRON (chac0902)
 * Audric DAVID (dava1302)
 * Matthieu JEHANNE (jehm1701)
 * Cloé LEGLISE (legc1001)
 */

namespace ExplorusE.Models.Commands
{
    internal class StartGameCommand : IGameCommand
    {
        public void Execute(GameModel model) { }
        public void Undo(GameModel model) { }
        public bool IsHistoryAction() {
            return true;
        }
    }
}
