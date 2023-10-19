using ExplorusE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ExplorusE.Threads
{
    internal class AudioThread
    {
        private readonly object lockObj = new object();
        private const int WAIT_BUFFER = 100;
        private const int SLEEP_TIMER = 100;
        private string threadName;
        AudioList oList = new AudioList();


        private volatile bool isRunning = true;

        public AudioThread(string name, AudioList list)
        {
            threadName = name;
            oList = list;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Stop()
        {
            isRunning = false;
            lock (oList)
            {
                Monitor.Pulse(oList);
            }
            
        }


        public void Run()
        {
            isRunning = true;
            Console.WriteLine("START Producer Thread : " + this.threadName);

            while (isRunning)
            {
                try
                {
                    do // ICI ON BOUCLE JUSQU A CE QUE L'AJOUT SOIT FAIT
                    {
                        if (oList.GetList().Count == 0) // L'ajout a échoué c'est comme si on disait == false 
                        {
                            lock (oList)
                            {
                                Monitor.Wait(oList, WAIT_BUFFER); // IL attend s'il a échoué
                            }
                        }
                        else
                        {
                            Console.WriteLine("élément dans liste" + oList.GetList().Count);
                            oList.GetList().ElementAt(0).Play();
                            oList.Remove();
                            //Thread.Sleep(1);
                            break;
                        }
                    }
                    while (isRunning);
                }
                catch (ThreadInterruptedException ex)
                {
                    Console.WriteLine(ex.Message);
                    isRunning = false;
                }


                Console.WriteLine("STOP Producer Thread : " + this.threadName);
            }
        }
        }

    }

