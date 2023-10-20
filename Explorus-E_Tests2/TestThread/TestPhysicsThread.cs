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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;

/* EXPLORUS-E
 * Alexis BLATRIX (blaa1406)
 * Cédric CHARRON (chac0902)
 * Audric DAVID (dava1302)
 * Matthieu JEHANNE (jehm1701)
 * Cloé LEGLISE (legc1001)
 */

namespace TestThread
{
    [TestClass]
    public class TestPhysicsThread
    {
        private PhysicsThread oPhysicsThread;
        private Thread oTestPhysicsThread;
        private GameModel gm;
        private int TIMER_JOIN = 500;
        private int PROCESS_DELAY = 500;
        [TestInitialize]
        public void TestInit()
        {
            /*ControllerMOC moc = new ControllerMOC();*/
            gm = new GameModel(null, null, null);
            StopThread();
            oPhysicsThread = new PhysicsThread("Physics Thread", gm);
            oTestPhysicsThread = new Thread(oPhysicsThread.Run);


            //oTestRenderThread.Join();
            //Assert.IsTrue(oTestRenderThread.IsAlive);


        }
        [TestCleanup]
        public void TestCleanup()
        {
            if (oPhysicsThread.IsStopped())
            {
                StopThread();

            }
            Assert.IsFalse(oPhysicsThread.IsStopped());
        }
        private void StopThread()
        {
            if (oPhysicsThread != null && oTestPhysicsThread.IsAlive)
            {
                oPhysicsThread.Stop();
                oTestPhysicsThread.Join(TIMER_JOIN);

                Assert.IsFalse(oPhysicsThread.IsStopped());

                if (oTestPhysicsThread.IsAlive)
                    oTestPhysicsThread.Abort();

                Assert.IsFalse(oTestPhysicsThread.IsAlive);
            }
        }
        [TestMethod]
        public void TestStart()
        {
            oTestPhysicsThread.Start();
            oTestPhysicsThread.Join(TIMER_JOIN);
            Assert.IsTrue(oTestPhysicsThread.IsAlive);
        }
        [TestMethod]
        public void TestStopped()
        {
            TestStart();
            //oTestRenderThread.Start();
            oPhysicsThread.Stop();
            oTestPhysicsThread.Join(TIMER_JOIN);
            Assert.IsFalse(oTestPhysicsThread.IsAlive);
        }


    }
}
