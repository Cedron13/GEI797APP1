﻿using ExplorusE.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;
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
            Console.WriteLine("START Audio Thread : " + this.threadName);
            MediaPlayer sound = new MediaPlayer();
            while (isRunning)
            {
                try
                {
                    do 
                    {
                        if (oList.GetList().Count == 0) 
                        {
                            lock (oList)
                            {
                                Monitor.Wait(oList, WAIT_BUFFER); 
                            }
                        }
                        else
                        {
                            Console.WriteLine("Yes");
                            Uri uri = new Uri("..\\..\\Resources\\" + oList.GetList().ElementAt(0), UriKind.Relative);
                            sound.Open(uri);
                            sound.Volume = oList.GetVolume()/100f;
                            sound.Play();
                            oList.Remove();
                            Thread.Sleep(SLEEP_TIMER);
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


                
            }
            Console.WriteLine("STOP Audio Thread : " + this.threadName);
        }
        }

    }

