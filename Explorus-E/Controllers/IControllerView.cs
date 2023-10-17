using ExplorusE.Controllers.States;
using ExplorusE.Models;
using System.Collections.Generic;
using System.Windows.Forms;

/* EXPLORUS-E
 * Alexis BLATRIX (blaa1406)
 * Cédric CHARRON (chac0902)
 * Audric DAVID (dava1302)
 * Matthieu JEHANNE (jehm1701)
 * Cloé LEGLISE (legc1001)
 */

namespace ExplorusE.Controllers
{
    internal interface IControllerView
    {
        void ViewKeyPressedEvent(PreviewKeyDownEventArgs e);

        void ViewCloseEvent();

        void ProcessMinimize();

        void ProcessLostFocus();

        void EndProcessLostFocus();

        int[,] GetLabyrinth();

        bool IsPaused { get; set; }
        bool IsDeadOnce { get; set; }
        bool IsDeadTwice { get; set; }

        Sprite GetPlayer();

        IState GetState();

        int GetTransitionTime();

        void PositionUpdate();
        List<BubbleSprite> GetBubbles();
        
    }
}
