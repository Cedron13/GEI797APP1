using ExplorusE.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ExplorusE.Threads
{
    internal class RenderThread
    {
        private IControllerModel controller;
        public double ms {  get; set; }
        private bool render = false;
        private bool isRunning = true;
        private readonly object lockObj = new object();

        public RenderThread(IControllerModel controller)
        {
            this.controller = controller;
        }

        public void changeRender(bool render)
        {
            lock(lockObj)
            {
                this.render = render;
            }
        }

        public void Stop()
        {
            isRunning = false;
        }


        public void Run()
        {
            while(isRunning)
            {
                lock(lockObj)
                {
                    if (render)
                    {
                        controller.EngineRenderEvent();
                        render = !render;
                    }
                } 
                Thread.Sleep(1);
            }      
        }
    }
}
