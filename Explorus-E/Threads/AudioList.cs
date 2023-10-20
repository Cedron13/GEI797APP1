using ExplorusE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ExplorusE.Threads
{
    internal class AudioList
    {
        private const int MAX_NUMBER_SOUND = 2;
        private readonly List<string> oListData = new List<string>();
        private int currentVolume;


        [MethodImpl(MethodImplOptions.Synchronized)] 
        public bool Add(string sound)
        {
            bool isAdded = false;

            if (oListData.Count < MAX_NUMBER_SOUND && sound!=null) 
            {
                oListData.Add(sound);
                isAdded = true;
            }

            return isAdded;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Remove()
        {
            if (oListData.Count > 0) 
            {
                oListData.RemoveAt(0); 
            }

        }
        public void SetVolume(int v)
        {
            lock (this)
            {
                currentVolume = v;
            }
        }
        public int GetVolume()
        {
            lock (this)
            {
                return currentVolume;
            }
        }

        public List<string> GetList()
        {
            lock (this)
            {
                List<string> list = new List<string>();
                foreach (string item in oListData) list.Add(item);
                return list;
            }
        }

        public void ClearList()
        {
            lock (this)
            {
                oListData.Clear();
            }
        }


    }
}
