using ExplorusE.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExplorusE.Models.Commands
{
    internal class DestroySpriteCommand : IGameCommand
    {
        Sprite destroyedSprite;
        bool isToxic = false;
        bool isBubble = false;
        bool isGem = false;
        bool needsRedo = false;
        public DestroySpriteCommand(ToxicSprite s)
        {
            destroyedSprite = s;
            isToxic=true;
        }
        public DestroySpriteCommand(BubbleSprite s)
        {
            destroyedSprite = s;
            isBubble=true;
        }
        public DestroySpriteCommand(GemSprite s)
        {
            destroyedSprite = s;
            isGem = true;
        }

        public void Execute(GameModel model)
        {
            if (needsRedo)
            {
                if (isBubble)
                {
                    model.RemoveBubble((BubbleSprite)destroyedSprite);
                }
                if (isToxic)
                {
                    coord pos = new coord()
                    {
                        x = (int)destroyedSprite.GetGridPosition().x,
                        y = (int)destroyedSprite.GetGridPosition().y
                    };
                    model.AddGem(new GemSprite(pos, destroyedSprite.GetActualTop(), destroyedSprite.GetActualLeft(), destroyedSprite.GetActualBricksize(), ((ToxicSprite)destroyedSprite).GetName()));
                    model.RemoveToxic((ToxicSprite)destroyedSprite);
                }
                if (isGem)
                {
                    model.RemoveGem((GemSprite)destroyedSprite);
                }
            }
            
        }
        public void Undo(GameModel model)
        {
            if (isBubble)
            {
                ((BubbleSprite)destroyedSprite).ReCreate();
                model.AddBubble((BubbleSprite)destroyedSprite);
            }
            if (isToxic)
            {
                ((ToxicSprite)destroyedSprite).ReCreate();
                model.AddToxic((ToxicSprite)destroyedSprite);
                model.RemoveGemForToxic(((ToxicSprite)destroyedSprite).GetName());
            }
            if (isGem)
            {
                ((GemSprite)destroyedSprite).ReCreate();
                model.AddGem((GemSprite)destroyedSprite);
            }
            needsRedo = true;
        }
        public bool IsHistoryAction()
        {
            return true;
        }
    }
}
