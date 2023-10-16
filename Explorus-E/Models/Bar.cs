using ExplorusE.Constants;
using ExplorusE.Models.Sprites;
using ExplorusE.Observer;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExplorusE.Models
{
    internal class Bar : Renderable, IResizeEventSubscriber
    {

        private List<NotInGridSprite> itemList;
        private int size;
        private int count;
        private coord gridPos;
        private coordF tilePos;
        private string spriteName;
        private int topMargin;
        private int leftMargin;
        private int brickSize;
        private float brickScale;
        private bool half;

        public Bar(coord gridPos, coordF tilePos, bool half, int size, BarType type, int top, int left, int brick, float brickScale)
        {
            this.gridPos = gridPos;
            this.tilePos = tilePos;
            this.size = size;
            this.topMargin = top;
            this.leftMargin = left;
            this.brickSize = brick;
            this.brickScale = brickScale;
            this.half = half;
            switch (type)
            {
                case (BarType.HEALTH): spriteName = Constants.Constants.HEALTHBAR_SPRITE_NAME; break;
                case (BarType.BUBBLE): spriteName = Constants.Constants.BUBBLEBAR_SPRITE_NAME; break;
                case (BarType.COIN): spriteName = Constants.Constants.COINBAR_SPRITE_NAME; break;
            }
            InitBar();
        }

        private Bar(List<NotInGridSprite> itemList)
        {
            this.itemList = itemList;
        }

        private void InitBar()
        {
            itemList = new List<NotInGridSprite>();
            NotInGridSprite start = new NotInGridSprite(gridPos, tilePos, Constants.Constants.BEGINBAR_SPRITE_NAME, topMargin, leftMargin, brickSize, brickScale);
            float startBarScaleToBrick = start.GetScaleWidth();
            Console.WriteLine(startBarScaleToBrick);
            itemList.Add(start);
            int itemNumber;
            itemNumber = half ? size / 2 : size;
            for(int i = 0; i < itemNumber; i++)
            {
                itemList.Add(new NotInGridSprite(gridPos, new coordF()
                {
                    x = tilePos.x + brickScale * (i+1) + startBarScaleToBrick,
                    y = tilePos.y
                }, Constants.Constants.EMPTYBAR_SPRITE_NAME, topMargin, leftMargin, brickSize, brickScale));
            }
            itemList.Add(new NotInGridSprite(gridPos, new coordF()
            {
                x = tilePos.x + brickScale * (itemNumber + 1) + startBarScaleToBrick,
                y = tilePos.y
            }, Constants.Constants.ENDBAR_SPRITE_NAME, topMargin, leftMargin, brickSize, brickScale));
        }

        public void SetProgression(int count)
        {
            if(count <= size)
            {
                if (!half)
                {
                    for (int i = 1; i < count + 1; i++) itemList[i].SetImageName(spriteName + "Full");
                    for (int i = count + 1; i < size + 1; i++) itemList[i].SetImageName(Constants.Constants.EMPTYBAR_SPRITE_NAME);
                }
                else
                {
                    int itemNumber;
                    itemNumber = size / 2;
                    int full = count / 2;
                    bool needHalf = count % 2 == 1;
                    for (int i = 1; i < full + 1; i++) itemList[i].SetImageName(spriteName + "Full");
                    if (needHalf) itemList[full + 1].SetImageName(spriteName + "Half");
                    for (int i = full + 2; i < itemNumber + 1; i++) itemList[i].SetImageName(Constants.Constants.EMPTYBAR_SPRITE_NAME);
                }
            }
        }


        public void NotifyResize(int top, int left, int brick)
        {
            foreach (NotInGridSprite sprite in itemList) sprite.NotifyResize(top, left, brick);
        }

        public void Render(Graphics g)
        {
            foreach(NotInGridSprite sprite in itemList) sprite.Render(g);
        }

        public Renderable CopyForRender()
        {
            List<NotInGridSprite> list = new List<NotInGridSprite>();
            foreach (NotInGridSprite item in itemList) list.Add((NotInGridSprite) item.CopyForRender());
            return new Bar(list);
        }
    }
}
