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


        [MethodImpl(MethodImplOptions.Synchronized)] // Un seul thread accède à la fois a la fonciton.. Clé partagée pour toute la classe
        public bool Add(string sound)
        {
            bool isAdded = false;

            if (oListData.Count < MAX_NUMBER_SOUND && sound!=null) // 5 --> max sound in list
            {
                oListData.Add(sound);
                isAdded = true;
            }

            return isAdded;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public string Remove()
        {
            string sound = "";
            if (oListData.Count > 0) // on vérifie non vide
            {
                sound = oListData[0]; //on cherche élément en tête de liste FIFO
                oListData.RemoveAt(0); //Enleve élément position 0 et tout les autres vont monter de 1  FIFO
            }

            return sound;
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
