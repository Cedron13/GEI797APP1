using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEI797Labo.Models.Commands
{
    internal class MoveCommand: IGameCommand
    {
        private Direction dir;
        public MoveCommand(Direction d) {
            dir = d;
        }
        public void Execute() { }
    }
}
