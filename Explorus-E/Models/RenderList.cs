using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace ExplorusE.Models
{
    internal class RenderList
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
            List<Renderable> list = new List<Renderable>();
            foreach (Renderable item in renderList.ToArray()) list.Add(item);
            renderList.Clear();
            return list;
        }

        public List<Renderable> GetList()
        {
            List<Renderable> list = new List<Renderable>();
            foreach (Renderable item in renderList) list.Add(item);
            return list;
        }
    }
}
