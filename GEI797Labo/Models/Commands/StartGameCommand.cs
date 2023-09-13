using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEI797Labo.Models.Commands
{
    internal class StartGameCommand : IGameCommand
    {
        public void Execute(GameModel model) { }
        public void Undo(GameModel model) { }
        public bool IsHistoryAction() {
            return true;
        }
    }
}
