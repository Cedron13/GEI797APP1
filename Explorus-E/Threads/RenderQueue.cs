using ExplorusE.Constants;
using ExplorusE.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ExplorusE.Threads
{
    internal class RenderQueue : IRenderQueueAsker
    {
        private ConcurrentQueue<RenderElement> queue = new ConcurrentQueue<RenderElement>();

        /// <summary>
        /// Asks to add a new item to the queue
        /// </summary>
        /// <param name="item">Item to add</param>
        /// <param name="type">Type of the item</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AskForNewItem(Renderable item, RenderItemType type)
        {
            queue.Enqueue(new RenderElement()
            {
                element = item.CopyForRender(),
                type = type
            });
        }

        public ConcurrentQueue<RenderElement> GetQueue() => queue;
    }
}
