using ExplorusE.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

/* EXPLORUS-E
 * Alexis BLATRIX (blaa1406)
 * Cédric CHARRON (chac0902)
 * Audric DAVID (dava1302)
 * Matthieu JEHANNE (jehm1701)
 * Cloé LEGLISE (legc1001)
 */

namespace ExplorusE.Models.Commands
{
    internal class BubbleCreateCommand : IGameCommand
    {
        private BubbleSprite bubbleSprite;

        public BubbleCreateCommand(BubbleSprite bubble)
        {
            bubbleSprite = bubble;
        }

        //TODO: Add movement to history if there is no collision with wall
        public void Execute(GameModel model)
        {
            model.AddBubble(bubbleSprite);
        }
        public void Undo(GameModel model)
        {
            model.RemoveBubble(bubbleSprite);
        }
        public bool IsHistoryAction() => true;

       

    }
}
