using ExplorusE.Constants;
using ExplorusE.Models;
using ExplorusE.Threads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    public class TestAudioThread
    {

        private AudioThread oAudioThread;
        //private GameModel gm;
        private Thread oTestAudioThread;
        private AudioList aList;
        private int TIMER_JOIN = 500;
        private int PROCESS_DELAY = 500;

        [TestInitialize]
        public void TestInit()
        {
            /*ControllerMOC moc = new ControllerMOC();*/
            aList = new AudioList();
            //gm = new GameModel(null, null, null);
            StopThread();
            oAudioThread = new AudioThread("Audio Thread",aList);
            oTestAudioThread = new Thread(oAudioThread.Run);


            //oTestRenderThread.Join();
            //Assert.IsTrue(oTestRenderThread.IsAlive);


        }
        [TestCleanup]
        public void TestCleanup()
        {
            if (oAudioThread.IsStopped())
            {
                StopThread();

            }
            Assert.IsFalse(oAudioThread.IsStopped());
        }
        private void StopThread()
        {
            if (oAudioThread != null && oTestAudioThread.IsAlive)
            {
                oAudioThread.Stop();
                oTestAudioThread.Join(TIMER_JOIN);

                Assert.IsFalse(oAudioThread.IsStopped());

                if (oTestAudioThread.IsAlive)
                    oTestAudioThread.Abort();

                Assert.IsFalse(oTestAudioThread.IsAlive);
            }
        }
        [TestMethod]
        public void TestStart()
        {
            oTestAudioThread.Start();
            oTestAudioThread.Join(TIMER_JOIN);
            Assert.IsTrue(oTestAudioThread.IsAlive);
        }

        [TestMethod]
        public void TestRemove()
        {
            oTestAudioThread.Start();
            aList.Add("test");
            oTestAudioThread.Join(TIMER_JOIN);         
            List <String> list = new List<String>();
            Assert.AreEqual(aList.GetList().Count(), list.Count());

        }

        [TestMethod]
         public void TestStopped()
        {
            TestStart();
            //oTestAudioThread.Start();
            oAudioThread.Stop();
            oTestAudioThread.Join(TIMER_JOIN);
            Assert.IsFalse(oTestAudioThread.IsAlive);
        }
    }
    }