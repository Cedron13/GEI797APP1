using ExplorusE.Constants;
using ExplorusE.Models;
using ExplorusE.Models.Sprites;
using ExplorusE.Threads;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
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

        [TestMethod]
        public void TestAddingNonPermanentitem()
        {
            Text deadText = new  Text("", new SizeF()
            {
                Width = (float)8.6,
                Height = (float)6.6
            }, "Arial", Color.Black, Color.Black, new coord()
            {
                x = 4,
                y = 4
            }, new coordF()
            {
                x = 0.2,
                y = 0.2
            },
            0, 0, 0);
            

            int sizein = oRenderThread.GetNonPermanentList().GetList().Count();
            
            oRenderThread.GetQueue().AskForNewItem(deadText, RenderItemType.NonPermanent);
            
            TestStart();

            int sizefin = oRenderThread.GetNonPermanentList().GetList().Count();
            Assert.AreNotEqual(sizein, sizefin);
            Assert.AreEqual(sizein, oRenderThread.GetPermanentList().GetList().Count());
        }
        [TestMethod]
        public void TestAddingPermanentitem()
        {
            Text deadText = new Text("", new SizeF()
            {
                Width = (float)8.6,
                Height = (float)6.6
            }, "Arial", Color.Black, Color.Black, new coord()
            {
                x = 4,
                y = 4
            }, new coordF()
            {
                x = 0.2,
                y = 0.2
            },
            0, 0, 0);

            int sizein = oRenderThread.GetPermanentList().GetList().Count();

            oRenderThread.GetQueue().AskForNewItem(deadText, RenderItemType.Permanent);

            TestStart();

            int sizefin = oRenderThread.GetPermanentList().GetList().Count();
            Assert.AreNotEqual(sizein, sizefin);
            Assert.AreEqual(sizein, oRenderThread.GetNonPermanentList().GetList().Count());
        }
        

    }
}
