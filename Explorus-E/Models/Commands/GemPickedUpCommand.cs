using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExplorusE.Models.Commands
{
    internal class GemPickedUpCommand : IGameCommand
    {
        public GemPickedUpCommand() { }
        public void Execute(GameModel model)
        {
            model.SetCounter(model.GetCounter() + 1);
        }
        public void Undo(GameModel model)
        {
            model.SetCounter(model.GetCounter() - 1);
        }
        public bool IsHistoryAction()
        {
            return true;
        }

    }
}
