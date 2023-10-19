using ExplorusE.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExplorusE.Controllers
{
    internal interface IControllerMenu
    {

        void AddSubscriber(IResizeEventSubscriber subscriber);


        void NewGame();
    }
}
