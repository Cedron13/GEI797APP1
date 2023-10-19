using ExplorusE.Constants;
using ExplorusE.Models;
using ExplorusE.Threads;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests;

namespace TestThread
{
    [TestClass]
    public class TestRenderThread
    {
        private RenderThread oRenderThread;
        private Thread oTestRenderThread;
        private GameModel gm;
        private int TIMER_JOIN = 500;
        private int PROCESS_DELAY = 500;
        [TestInitialize]
        public void TestInit()
        {
            /*ControllerMOC moc = new ControllerMOC();
            gm = new GameModel(moc, null);*/
            StopThread();
            oRenderThread = new RenderThread();
            oTestRenderThread = new Thread(oRenderThread.Run);
            

            //oTestRenderThread.Join();
            //Assert.IsTrue(oTestRenderThread.IsAlive);
            

        }
        [TestCleanup]
        public void TestCleanup()
        {
            if (oRenderThread.IsStopped())
            {
                StopThread();

            }
            Assert.IsFalse(oRenderThread.IsStopped());
        }
        private void StopThread()
        {
            if (oRenderThread != null && oTestRenderThread.IsAlive)
            {
                oRenderThread.Stop();
                oTestRenderThread.Join(TIMER_JOIN);

                Assert.IsFalse(oRenderThread.IsStopped());

                if (oTestRenderThread.IsAlive)
                    oTestRenderThread.Abort();

                Assert.IsFalse(oTestRenderThread.IsAlive);
            }
        }
        [TestMethod]
        public void TestStart()
        {
            oTestRenderThread.Start();
            oTestRenderThread.Join(TIMER_JOIN);
            Assert.IsTrue(oTestRenderThread.IsAlive);
        }
        [TestMethod]
         public void TestStopped()
        {
            TestStart();
            //oTestRenderThread.Start();
            oRenderThread.Stop();
            oTestRenderThread.Join(TIMER_JOIN);
            Assert.IsFalse(oTestRenderThread.IsAlive);
        }


    }
}
