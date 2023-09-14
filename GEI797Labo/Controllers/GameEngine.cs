using GEI797Labo.Controllers;
using System;
using System.Threading;

/* EXPLORUS-E
 * Alexis BLATRIX (blaa1406)
 * Cédric CHARRON (chac0902)
 * Audric DAVID (dava1302)
 * Matthieu JEHANNE (jehm1701)
 * Cloé LEGLISE (legc1001)
 */

namespace GEI797Labo
{

    internal class GameEngine
    {
        private IController controller;
        private bool isAlive = true;
        private readonly object lockObject = new object();
        private Thread gameThread;
        public GameEngine(IController c)
        

        {
            controller = c;
            gameThread = new Thread(new ThreadStart(GameLoop));
            gameThread.Start();
        }

        public void KillEngine()
        {
            lock (lockObject)
            {
                isAlive = false;
            }
            gameThread.Join();
        }

        private void GameLoop()
        {
            double previous = GetCurrentTimeMillis();
            double lag = 0.0;
            float MS_PER_FRAME = 16.67f; // 1000/FPS
            float fps = 1;

            while (true)
            {
                lock (lockObject)
                {
                    if (!isAlive)
                    {
                        break;
                    }
                }

                double current = GetCurrentTimeMillis();
                double elapsed = current - previous;
                previous = current;
                lag += elapsed;
                if (lag >= MS_PER_FRAME)
                {
                    fps = (float)(1000f / lag);

                    ProcessInput();

                    while (lag >= MS_PER_FRAME)
                    {
                        Update(MS_PER_FRAME); 
                        lag -= MS_PER_FRAME;
                    }
                    Render(lag / MS_PER_FRAME);
                    Thread.Sleep(1);
                }
            }
        }

        private void Render(double frameAhead) {
            controller.EngineRenderEvent(); // Initial call of Render method
        }
        private void Update(double lag)
        {
            controller.EngineUpdateEvent(lag);
        }
        private void ProcessInput()
        {
            controller.EngineProcessInputEvent();
        }
        private double GetCurrentTimeMillis()
        {
            DateTime epochStart = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            TimeSpan elapsedTime = DateTime.UtcNow - epochStart;
            return elapsedTime.TotalMilliseconds;
        }
        
    } 
}
