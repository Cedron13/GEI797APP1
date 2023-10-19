using ExplorusE.Constants;
using ExplorusE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExplorusE.Threads
{
    internal interface IRenderQueueAsker
    {
        void AskForNewItem(Renderable item, RenderItemType type);
    }
}
