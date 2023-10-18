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
    internal class AudioThreadBubble
    {
        private bool isRunning = true;
        private readonly object lockObj = new object();

        //private RenderList permanentItems = new RenderList();

        /// <summary>
        /// Stops the RenderThread's thread
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Stop()
        {
            isRunning = false;
        }



        /// <summary>
        /// Runs the thread
        /// </summary>
        public void Run()
        {
            Console.WriteLine("START - RenderThread");
            while (isRunning)
            {

                //Thread.Sleep(1);
            }
            Console.WriteLine("STOP - RenderThread");
        }
    }
}