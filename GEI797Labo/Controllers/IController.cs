using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GEI797Labo.Models.GameModel;

namespace GEI797Labo.Controllers
{
    public struct RenderData
    {
        public int[,] lab;
        //playerSprite
        //enemiesSpriteArray
    }
    internal interface IController
    {
        void renderEvent(RenderData data);

    }
}
