using ExplorusE.Constants;
using ExplorusE.Controllers;
using ExplorusE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Collections.Concurrent;

/* EXPLORUS-E
 * Alexis BLATRIX (blaa1406)
 * Cédric CHARRON (chac0902)
 * Audric DAVID (dava1302)
 * Matthieu JEHANNE (jehm1701)
 * Cloé LEGLISE (legc1001)
 */

/*
 * 
GAMELOOP = (update -> GAMELOOP).
PHYSICSTHREAD = (checkCollisions -> playerGemCollision -> PHYSICSTHREAD 
				| checkCollisions -> toxicPlayerCollision -> PHYSICSTHREAD
				| checkCollisions -> toxicBubbleCollision -> PHYSICSTHREAD).

||THREADS = (GAMELOOP||PHYSICSTHREAD).

 */

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
        public bool IsStopped() {
            return isRunning;
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
                        Thread.Sleep(50);
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
