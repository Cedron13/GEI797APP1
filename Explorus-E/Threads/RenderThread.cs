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


/*
 * 
 *
const N = 2
range T = 0..N

GAMELOOP = (askForNewItem -> GAMELOOP).
RENDERTHREAD = (waitItemInQueue -> addPermanentItem -> removeItemInQueue -> RENDERTHREAD
				| waitItemInQueue -> addItem -> removeItemInQueue -> RENDERTHREAD).
WINDOWTHREAD = (getList -> WINDOWTHREAD
				| getPermanentList -> WINDOWTHREAD).
QUEUE = ARRAY[0], ARRAY[i:T] =
	(when(i<N) askForNewItem -> ARRAY[i+1]
	|when(i>0) removeItemInQueue -> ARRAY[i-1]).
PERMANENTLIST = ARRAY[0], ARRAY[i:T] =
	(when(i<N) add-> ARRAY[i+1]
	|clear -> ARRAY[0]).
NONPERMANENTLIST = ARRAY[0], ARRAY[i:T] =
	(when(i<N) add-> ARRAY[i+1]
	|clear -> ARRAY[0]).

||THREADS = (GAMELOOP||RENDERTHREAD||WINDOWTHREAD||QUEUE||PERMANENTLIST||NONPERMANENTLIST).
 * 
 * 
 */

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

        private RenderQueue queue = new RenderQueue();

        public IRenderQueueAsker GetQueue() => queue;
        public IRenderListReader GetPermanentList() => permanentItems;
        public IRenderListReader GetNonPermanentList() => items;

        /// <summary>
        /// Stops the RenderThread's thread
        /// </summary>
        public void Stop()
        {
            isRunning = false;
            lock (queue)
            {
                Monitor.PulseAll(queue);
            }
        }
        public bool IsStopped()
        {
            return isRunning;
        }

        
        /// <summary>
        /// Adds an item to the permanent list
        /// </summary>
        /// <param name="item">Item to add</param>
        private void AddPermanentItem(Renderable item)
        {
            lock (permanentItems)
            {
                permanentItems.Add(item);
            }
            
        }

        /// <summary>
        /// Adds an item to the list
        /// </summary>
        /// <param name="item">Item to add</param>
        private void AddItem(Renderable item)
        {
            lock (items)
            {
                items.Add(item);
            }
        }

        /// <summary>
        /// Runs the thread
        /// </summary>
        public void Run()
        {
            Console.WriteLine("START - RenderThread");
            while (isRunning)
            {
                try
                {
                    do
                    {
                        RenderElement renderElement = new RenderElement();
                        do
                        {
                            if (renderElement.element != null)
                            {
                                lock (lockObj)
                                {
                                    switch (renderElement.type)
                                    {
                                        case RenderItemType.Permanent: AddPermanentItem(renderElement.element); break;
                                        case RenderItemType.NonPermanent: AddItem(renderElement.element); break;
                                    }
                                }

                            }
                        } while (queue.GetQueue().TryDequeue(out renderElement));
                    } while (isRunning);
                    
                } catch (ThreadInterruptedException ex)
                {
                    Console.WriteLine(ex.Message);
                    isRunning = false;
                    break;
                }
                
            }
            Console.WriteLine("STOP - RenderThread");
        }
    }
}
