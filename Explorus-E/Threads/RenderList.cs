using ExplorusE.Models;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;

/* EXPLORUS-E
 * Alexis BLATRIX (blaa1406)
 * Cédric CHARRON (chac0902)
 * Audric DAVID (dava1302)
 * Matthieu JEHANNE (jehm1701)
 * Cloé LEGLISE (legc1001)
 */

namespace ExplorusE.Threads
{
    internal class RenderList : IRenderListReader
    {
        private readonly List<Renderable> renderList = new List<Renderable>();

        public bool Add(Renderable item)
        {
            bool isAdded = false;

            if (!renderList.Contains(item))
            {
                renderList.Add(item);
                isAdded = true;
            }
            return isAdded;
        }

        public List<Renderable> Flush()
        {
            lock (this)
            {
                List<Renderable> list = new List<Renderable>();
                foreach (Renderable item in renderList) list.Add(item);
                renderList.Clear();
                return list;
            }
            
        }

        public List<Renderable> GetList()
        {
            lock (this)
            {
                List<Renderable> list = new List<Renderable>();
                foreach (Renderable item in renderList) list.Add(item);
                return list;
            }
        }

        public void ClearList()
        {
            lock (this)
            {
                renderList.Clear();
            }
        }
    }
}
