﻿using ExplorusE.Observer;
using ExplorusE.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

/* EXPLORUS-E
 * Alexis BLATRIX (blaa1406)
 * Cédric CHARRON (chac0902)
 * Audric DAVID (dava1302)
 * Matthieu JEHANNE (jehm1701)
 * Cloé LEGLISE (legc1001)
 */

namespace ExplorusE.Models
{
    internal abstract class Sprite : IResizeEventSubscriber, Renderable
    {
        protected coord initialPos;
        protected coordF currentPos;
        protected coord destinationPos;
        protected int imageIndex;
        protected Direction dir;
        protected double timeToMove = 500; //Time in ms
        protected double timeElapsed = 0; //Time in ms
        protected string name = "Anonymous";
        protected readonly object coordlock = new object();

        protected double boundingRadius;

        protected int topMargin;
        protected int leftMargin;
        protected int brickSize;
        protected TileManager tileManager;

        protected int transparency;
        protected float brickScale;

        public Sprite(coord gridPos, int top, int left, int brick) : this(gridPos, top, left, brick, 0, 1.0f)
        {

        }

        public Sprite(coord gridPos, int top, int left, int brick, float brickScale) : this(gridPos, top, left, brick, 0, brickScale)
        {

        }

        public Sprite(coord gridPos, int top, int left, int brick, int transparency) : this(gridPos, top, left, brick, transparency, 1.0f)
        {

        }

        public Sprite(coord gridPos, int top, int left, int brick, int transparency, float brickScale)
        {
            currentPos = new coordF()
            {
                x = gridPos.x,
                y = gridPos.y
            };
            destinationPos = new coord()
            {
                x = gridPos.x,
                y = gridPos.y
            };
            topMargin = top;
            leftMargin = left;
            brickSize = brick;
            boundingRadius = 0.40;
            tileManager = TileManager.GetInstance();
            this.transparency = transparency;
            this.brickScale = brickScale;
        }

        public void SetTransparency(int value)
        {
            transparency = value;
        }

        public void setName(string n)
        {
            name = n;
        }
        public string GetName()
        {
            return name;
        }
        //IResizeEventSubscriber
        public void NotifyResize(int top, int left, int brick)
        {
            topMargin = top;
            leftMargin = left;
            brickSize = brick;
        }

        //Sprite
        public void StartMovement(coord finalGridPos, Direction d)
        {
            lock (coordlock)
            {
                dir = d;
                initialPos.x = (int)currentPos.x;
                initialPos.y = (int)currentPos.y;
                currentPos.x = (int)currentPos.x;
                currentPos.y = (int)currentPos.y;
                destinationPos = finalGridPos;
                timeElapsed = 0;
            }
        }
        public spriteState GetCurrentRenderInfo()
        {
            lock (coordlock)
            {
                spriteState state = new spriteState();
                state.imageIndex = imageIndex;
                state.spriteCoord = GetPixelPosition();
                return state;
            }
        }
        public bool IsMovementOver()
        {
            lock (coordlock)
            {
                return currentPos.x == destinationPos.x && currentPos.y == destinationPos.y;
            }
        }

        public abstract void Update(int elapsedMs);


        public coordF GetGridPosition()
        {
            lock (coordlock)
            {
                coordF temp = new coordF()
                {
                    x = currentPos.x,
                    y = currentPos.y
                };
                return temp;
            }
        }

        protected void SetGridPosition(coordF pos)
        {
            currentPos = new coordF()
            {
                x = pos.x,
                y = pos.y
            };
        }

        protected virtual coord GetPixelPosition()
        {
            coord playerCurrentPixelCoord = new coord()
            {
                x = (int)(leftMargin + brickSize * currentPos.x + brickSize / 2 - (brickSize * brickScale) / 2),
                y = (int)(topMargin + brickSize * (currentPos.y + 1) + brickSize / 2 - (brickSize * brickScale) / 2)
            };
            return playerCurrentPixelCoord;
        }
        public Direction GetDirection() => dir;

        public void SetDirection(Direction d)
        {
            dir = d;
        }
        

        public abstract String GetImageName();
        
        public double GetBoundingRadius()
        {
            return boundingRadius;
        }

        public int GetActualBricksize()
        {
            return brickSize;
        }
        public int GetActualTop()
        {
            return topMargin;
        }
        public int GetActualLeft() 
        { 
            return leftMargin;
        }

        //Renderable Interface
        public virtual void Render(Graphics g)
        {
            g.DrawImage(tileManager.getImage(GetImageName()).bitmap, GetPixelPosition().x, GetPixelPosition().y, (int)(brickSize * brickScale), (int)(brickSize * brickScale));
            using (Brush transparencyBrush = new SolidBrush(Color.FromArgb(transparency, Color.Black)))
            {
                g.FillRectangle(transparencyBrush, new Rectangle(GetPixelPosition().x, GetPixelPosition().y, (int)(brickSize * brickScale), (int)( brickSize * brickScale)));
            }
        }

        public abstract Renderable CopyForRender();
    }
}
