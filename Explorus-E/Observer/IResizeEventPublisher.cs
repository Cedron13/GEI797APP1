﻿/* EXPLORUS-E
 * Alexis BLATRIX (blaa1406)
 * Cédric CHARRON (chac0902)
 * Audric DAVID (dava1302)
 * Matthieu JEHANNE (jehm1701)
 * Cloé LEGLISE (legc1001)
 */

namespace ExplorusE.Observer
{
    internal interface IResizeEventPublisher
    {
        void AddSubscriber(IResizeEventSubscriber subscriber);
    }
}
