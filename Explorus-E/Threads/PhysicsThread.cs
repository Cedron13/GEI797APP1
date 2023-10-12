using ExplorusE.Constants;
using ExplorusE.Controllers;
using ExplorusE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ExplorusE.Threads
{
    internal class PhysicsThread
    {
        private string threadName;
        GameModel model;
        private bool isRunning = true;
        private readonly object lockObj = new object();

        public PhysicsThread(string name, GameModel gm)
        {
            threadName = name;
            model = gm;
        }

        public void Stop()
        {
            isRunning = false;
        }


        public void Run()
        {
            isRunning = true;
            int index = 1;

            Console.WriteLine("START - " + threadName);

            while (isRunning)
            {
                string value = threadName + " " + index;

                try
                {
                    do
                    {
                        
                    }
                    while (isRunning);
                }
                catch (ThreadInterruptedException e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            Console.WriteLine("STOP - " + threadName);
        }
    }
}
