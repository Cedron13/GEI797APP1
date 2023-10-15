using ExplorusE.Constants;
using ExplorusE.Controllers;
using ExplorusE.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ExplorusE.Threads
{
    internal class RenderThread
    {
        private bool isRunning = true;
        private readonly object lockObj = new object();

        private RenderList permanentItems = new RenderList();
        private RenderList items = new RenderList();

        /*
        private Renderable itemQueue;
        private RenderItemType itemQueueType;
        private volatile bool isSomethingInQueue = false;
        */

        private ConcurrentQueue<RenderElement> queue = new ConcurrentQueue<RenderElement>();

        /// <summary>
        /// Stops the RenderThread's thread
        /// </summary>
        public void Stop()
        {
            isRunning = false;
        }

        /// <summary>
        /// Adds an item to the permanent list
        /// </summary>
        /// <param name="item">Item to add</param>
        private void AddPermanentItem(Renderable item)
        {
            permanentItems.Add(item);
        }

        /// <summary>
        /// Adds an item to the list
        /// </summary>
        /// <param name="item">Item to add</param>
        private void AddItem(Renderable item)
        {
            items.Add(item);
        }

        /// <summary>
        /// Asks to the thread to add a new item
        /// </summary>
        /// <param name="item">Item to add</param>
        /// <param name="type">Type of the item</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AskForNewItem(Renderable item, RenderItemType type)
        {
            /*
            while(isSomethingInQueue);
            itemQueue = item.CopyForRender();
            itemQueueType = type;
            isSomethingInQueue = true;*/
            queue.Enqueue(new RenderElement()
            {
                element = item.CopyForRender(),
                type = type
            });

        }

        /// <summary>
        /// Reset the permament item list
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void ResetPermanentItems()
        {
            //while(isSomethingInQueue);
            permanentItems = new RenderList();
        }

        /// <summary>
        /// Returns the list of the permanent items
        /// </summary>
        /// <returns>List of the permanent items</returns>
        public List<Renderable> GetPermanentItems()
        {
            lock (lockObj)
            {
                return permanentItems.GetList();
            }
        }

        /// <summary>
        /// Flushs the list of the items but returns it before
        /// </summary>
        /// <returns>List of the items</returns>
        public List<Renderable> GetItems()
        {
            lock(lockObj)
            {
                return items.Flush();
            }
        }

        /// <summary>
        /// Runs the thread
        /// </summary>
        public void Run()
        {
            while(isRunning)
            {
                /*
                if(isSomethingInQueue)
                {
                    lock(lockObj)
                    {
                        switch(itemQueueType)
                        {
                            case RenderItemType.Permanent: AddPermanentItem(itemQueue); break;
                            case RenderItemType.NonPermanent: AddItem(itemQueue); break; 
                        }
                        isSomethingInQueue = false;
                    }
                }
                */
                RenderElement renderElement = new RenderElement();
                do
                {
                    if (renderElement.element == null) continue;
                    else
                    {
                        switch (renderElement.type)
                        {
                            case RenderItemType.Permanent: AddPermanentItem(renderElement.element); break;
                            case RenderItemType.NonPermanent: AddItem(renderElement.element); break;
                        }
                    }
                } while (queue.TryDequeue(out renderElement));
                //Thread.Sleep(1);
            }      
        }
    }
}
