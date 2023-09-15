using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEI797Labo.Models.Commands
{
    internal interface IGameCommand
    {
        void Execute(GameModel model);
        void Undo(GameModel model);
        bool IsHistoryAction();
    }
}
