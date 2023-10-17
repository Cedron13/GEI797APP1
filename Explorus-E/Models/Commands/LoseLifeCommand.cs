using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExplorusE.Models.Commands
{
    internal class LoseLifeCommand : IGameCommand
    {
        SlimeTypeSprite sprite;
        public LoseLifeCommand(SlimeTypeSprite s) { 
            sprite = s;
        }
        public void Execute(GameModel model) {
            sprite.LoseLife();
        }

        public void Undo(GameModel model) {
            sprite.GainLife();
        }

        public bool IsHistoryAction() => true;
    }
}
