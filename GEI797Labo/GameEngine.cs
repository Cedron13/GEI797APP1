using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;



namespace GEI797Labo
{
    
    internal class GameEngine
    {
        private GameView oView;
        private Sprite player;


        public GameEngine()
        {
            oView = new GameView();

            TileManager tileManager = new TileManager();
            coord coordPlayerInit = new coord
            {
                x = 0,
                y = 0
            };
            player = new Sprite(tileManager.getImage("Wall"), coordPlayerInit, 0, 1000);

            Thread thread = new Thread(new ThreadStart(GameLoop));
            thread.Start();
            oView.Show();
        }

        private void GameLoop()
        {
            double previous = GetCurrentTimeMillis();
            double lag = 0.0;
            float MS_PER_FRAME = 16.67f; // 1000/FPS
            float fps = 1;

            while (true)
            {
                double current = GetCurrentTimeMillis();
                double elapsed = current - previous;
                previous = current;
                lag += elapsed;
                if (lag >= MS_PER_FRAME)
                {
                    fps = (float)(1000f / lag);
                    //Console.WriteLine(fps);

                    ProcessInput();

                    while (lag >= MS_PER_FRAME)
                    {
                        Update(MS_PER_FRAME); //Should be Update(lag)? To verify
                        lag -= MS_PER_FRAME;
                    }
                    Render(lag / MS_PER_FRAME);
                    Thread.Sleep(1);
                }
            }
        }

        private void Render(double frameAhead) {
            oView.Render(); // Appel initial de la méthode Render
        }
        private void Update(double lag)
        {
            oView.moveRight(lag);
        }
        private void ProcessInput()
        {

        }
        private double GetCurrentTimeMillis()
        {
            DateTime epochStart = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            TimeSpan elapsedTime = DateTime.UtcNow - epochStart;
            return elapsedTime.TotalMilliseconds;
        }

        public Sprite GetPlayer() => player;
    }
}
