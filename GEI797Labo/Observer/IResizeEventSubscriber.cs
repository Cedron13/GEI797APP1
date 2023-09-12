using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEI797Labo.Observer
{
    internal interface IResizeEventSubscriber
    {
        void NotifyResize(int top, int left, int brick);
    }
}
