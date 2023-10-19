using ExplorusE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExplorusE.Threads
{
    internal interface IRenderListReader
    {
        List<Renderable> Flush();

        List<Renderable> GetList();

        void ClearList();
    }
}
