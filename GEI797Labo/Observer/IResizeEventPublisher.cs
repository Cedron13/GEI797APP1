using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEI797Labo.Observer
{
    internal interface IResizeEventPublisher
    {
        void AddSubscriber(IResizeEventSubscriber subscriber);
    }
}
