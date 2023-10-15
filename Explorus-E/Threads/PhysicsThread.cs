using ExplorusE.Constants;
using ExplorusE.Controllers;
using ExplorusE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Collections.Concurrent;


namespace ExplorusE.Threads
{
    internal class PhysicsThread
    {
        private string threadName;
        GameModel model;
        Sprite player;
        List<BubbleSprite> bubbles;
        ConcurrentBag<ToxicSprite> toxicSlimes;
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

            Console.WriteLine("START - " + threadName);

            while (isRunning)
            {
                player = model.GetPlayer();
                toxicSlimes = model.GetToxicSlimes();
                bubbles = model.GetBubbles();

                try
                {
                    do
                    {
                        model.checkCollision();
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
