﻿using ExplorusE.Constants;
using ExplorusE.Controllers;
using ExplorusE.Models;
using ExplorusE.Models.Commands;
using ExplorusE.Threads;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
/* EXPLORUS-E
 * Alexis BLATRIX (blaa1406)
 * Cédric CHARRON (chac0902)
 * Audric DAVID (dava1302)
 * Matthieu JEHANNE (jehm1701)
 * Cloé LEGLISE (legc1001)
 */

namespace Tests
{
    [TestClass]
    public class TestGameModel
    {
        private AudioList a= new AudioList();

        //Test without wall
        [TestMethod]
        public void MoveLeftWithoutWall_WithSpritePosition()
        {
            coord start = new coord()
            {
                x = 4,
                y = 7
            };
            coord dest = new coord()
            {
                x = 3,
                y = 7
            };
            PlayerSprite s = new PlayerSprite(start, 33, 19, 50);
            GameModel gm = new GameModel(null, null, a);
            gm.SetGridPosX(4);
            gm.SetGridPosY(7);
            gm.InitPlayer(s);
            MoveCommand com = new MoveCommand(Direction.LEFT, start, dest);
            gm.InvokeCommand(com);
            s.Update(500);

            Assert.AreEqual(dest, gm.GetGridCoord());
        }

        [TestMethod]
        public void MoveRightWithoutWall_WithSpritePosition()
        {
            coord start = new coord()
            {
                x = 3,
                y = 7
            };
            coord dest = new coord()
            {
                x = 4,
                y = 7
            };
            PlayerSprite s = new PlayerSprite(start, 33, 19, 50);
            GameModel gm = new GameModel(null, null, a);
            gm.SetGridPosX(3);
            gm.SetGridPosY(7);
            gm.InitPlayer(s);
            MoveCommand com = new MoveCommand(Direction.RIGHT, start, dest);
            gm.InvokeCommand(com);
            s.Update(500);

            Assert.AreEqual(dest, gm.GetGridCoord());
        }
        [TestMethod]
        public void MoveUpWithoutWall_WithSpritePosition()
        {
            coord start = new coord()
            {
                x = 3,
                y = 7
            };
            coord dest = new coord()
            {
                x = 3,
                y = 6
            };
            PlayerSprite s = new PlayerSprite(start, 33, 19, 50);
            GameModel gm = new GameModel(null, null, a);
            gm.SetGridPosX(3);
            gm.SetGridPosY(7);
            gm.InitPlayer(s);
            MoveCommand com = new MoveCommand(Direction.UP, start, dest);
            gm.InvokeCommand(com);
            s.Update(500);

            Assert.AreEqual(dest, gm.GetGridCoord());
        }
        [TestMethod]
        public void MoveDownWithoutWall_WithSpritePosition()
        {
            coord start = new coord()
            {
                x = 3,
                y = 6
            };
            coord dest = new coord()
            {
                x = 3,
                y = 7
            };
            PlayerSprite s = new PlayerSprite(start, 33, 19, 50);
            GameModel gm = new GameModel(null, null, a);
            gm.SetGridPosX(3);
            gm.SetGridPosY(6);
            gm.InitPlayer(s);
            MoveCommand com = new MoveCommand(Direction.DOWN, start, dest);
            gm.InvokeCommand(com);
            s.Update(500);

            Assert.AreEqual(dest, gm.GetGridCoord());
        }

        [TestMethod]
        public void moverightwithwall_withlabyrinth()
        {
            int[,] labyrinth = {
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},  // 0 = nothing (free to go)
                    {1, 0, 0, 4, 0, 0, 0, 0, 0, 0, 1},  // 1 = display wall
                    {1, 0, 1, 1, 1, 0, 1, 1, 1, 0, 1},  // 2 = display door
                    {1, 0, 0, 0, 0, 0, 1, 5, 1, 0, 1},  // 3 = display slimus
                    {1, 0, 1, 0, 1, 1, 1, 2, 1, 0, 1},  // 4 = display gem
                    {1, 0, 1, 0, 1, 4, 0, 0, 0, 0, 1},  // 5 = display mini-slime
                    {1, 0, 1, 0, 1, 1, 0, 1, 1, 0, 1},
                    {1, 0, 0, 0, 3, 1, 0, 0, 4, 0, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                };

            coord start = new coord()
            {
                x = 4,
                y = 7
            };
            coord dest = new coord()
            {
                x = 5,
                y = 7
            };
            PlayerSprite s = new PlayerSprite(start, 33, 19, 50);
            GameModel gm = new GameModel(null, null, a);
            gm.SetLabyrinth(labyrinth);
            gm.SetGridPosX(4);
            gm.SetGridPosY(7);
            gm.InitPlayer(s);
            MoveCommand com = new MoveCommand(Direction.RIGHT, start, dest);
            gm.InvokeCommand(com);
            s.Update(500);

            Assert.AreEqual(labyrinth[7, 4], 3);
        }




        [TestMethod]
        public void MoveLeftWithoutWall_WithLabyrinth()
        {
            int[,] labyrinth = {
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},  // 0 = nothing (free to go)
                    {1, 0, 0, 4, 0, 0, 0, 0, 0, 0, 1},  // 1 = display wall
                    {1, 0, 1, 1, 1, 0, 1, 1, 1, 0, 1},  // 2 = display door
                    {1, 0, 0, 0, 0, 0, 1, 5, 1, 0, 1},  // 3 = display Slimus
                    {1, 0, 1, 0, 1, 1, 1, 2, 1, 0, 1},  // 4 = display gem
                    {1, 0, 1, 0, 1, 4, 0, 0, 0, 0, 1},  // 5 = display mini-slime
                    {1, 0, 1, 0, 1, 1, 0, 1, 1, 0, 1},
                    {1, 0, 0, 0, 3, 1, 0, 0, 4, 0, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                };
            coord start = new coord()
            {
                x = 4,
                y = 7
            };
            coord dest = new coord()
            {
                x = 3,
                y = 7
            };
            PlayerSprite s = new PlayerSprite(start, 33, 19, 50);
            GameModel gm = new GameModel(null, null, a);
            gm.SetLabyrinth(labyrinth);
            gm.SetGridPosX(4);
            gm.SetGridPosY(7);
            gm.InitPlayer(s);
            MoveCommand com = new MoveCommand(Direction.LEFT, start, dest);
            gm.InvokeCommand(com);
            s.Update(500);
            Assert.AreNotEqual(labyrinth[7, 4], 3);
        }
        [TestMethod]
        public void TestUndoLastCommand_WithMovement()
        {
            coord start = new coord()
            {
                x = 4,
                y = 7
            };
            coord dest = new coord()
            {
                x = 3,
                y = 7
            };
            PlayerSprite s = new PlayerSprite(start, 33, 19, 50);
            GameModel gm = new GameModel(null, null, a);
            gm.SetGridPosX(4);
            gm.SetGridPosY(7);
            gm.InitPlayer(s);
            MoveCommand com = new MoveCommand(Direction.LEFT, start, dest);
            gm.InvokeCommand(com);
            s.Update(500);
            gm.UndoLastCommand();
            s.Update(500);
            coord finalPos = new coord()
            {
                x = (int)s.GetGridPosition().x,
                y = (int)s.GetGridPosition().y
            };

            Assert.AreEqual(finalPos, start);
        }
        [TestMethod]
        public void TestRedoLastCommand_WithUndoBefore()
        {
            coord start = new coord()
            {
                x = 4,
                y = 7
            };
            coord dest = new coord()
            {
                x = 3,
                y = 7
            };
            PlayerSprite s = new PlayerSprite(start, 33, 19, 50);
            GameModel gm = new GameModel(null, null, a);
            gm.SetGridPosX(4);
            gm.SetGridPosY(7);
            gm.InitPlayer(s);
            MoveCommand com = new MoveCommand(Direction.LEFT, start, dest);
            gm.InvokeCommand(com);
            s.Update(500);
            gm.UndoLastCommand();
            s.Update(500);
            gm.RedoNextCommand();
            s.Update(500);
            coord finalPos = new coord()
            {
                x = (int)s.GetGridPosition().x,
                y = (int)s.GetGridPosition().y
            };

            Assert.AreEqual(finalPos, dest);
        }
        [TestMethod]
        public void TestUndoLastCommand_WithoutMoving()
        {
            coord start = new coord()
            {
                x = 4,
                y = 7
            };
            PlayerSprite s = new PlayerSprite(start, 33, 19, 50);
            GameModel gm = new GameModel(null, null, a);
            gm.SetGridPosX(4);
            gm.SetGridPosY(7);
            gm.InitPlayer(s);
            gm.UndoLastCommand();
            s.Update(500);
            coord finalPos = new coord()
            {
                x = (int)s.GetGridPosition().x,
                y = (int)s.GetGridPosition().y
            };

            Assert.AreEqual(finalPos, start);
        }
        [TestMethod]
        public void TestRedoNextCommand_WithoutUndoBefore()
        {
            coord start = new coord()
            {
                x = 4,
                y = 7
            };
            coord dest = new coord()
            {
                x = 3,
                y = 7
            };
            PlayerSprite s = new PlayerSprite(start, 33, 19, 50);
            GameModel gm = new GameModel(null, null, a);
            gm.SetGridPosX(4);
            gm.SetGridPosY(7);
            gm.InitPlayer(s);
            MoveCommand com = new MoveCommand(Direction.LEFT, start, dest);
            gm.InvokeCommand(com);
            s.Update(500);
            gm.RedoNextCommand();
            s.Update(500);
            coord finalPos = new coord()
            {
                x = (int)s.GetGridPosition().x,
                y = (int)s.GetGridPosition().y
            };

            Assert.AreEqual(finalPos, dest);
        }
        [TestMethod]
        public void TestUndo_ToxicSlimes()
        {
            coord start = new coord()
            {
                x = 4,
                y = 7
            };
            coord dest = new coord()
            {
                x = 3,
                y = 7
            };
            ToxicSprite s = new ToxicSprite(start, 33, 19, 50);
            s.SetDirection(Direction.LEFT);
            GameModel gm = new GameModel(null, null, a);
            ToxicMoveCommand com = new ToxicMoveCommand(s);
            gm.InvokeCommand(com);
            s.Update(600);
            coord finalPos = new coord()
            {
                x = (int)s.GetGridPosition().x,
                y = (int)s.GetGridPosition().y
            };

            Assert.AreEqual(finalPos, dest);
        }
        [TestMethod]
        public void TestRedo_ToxicSlimes()
        {
            coord start = new coord()
            {
                x = 4,
                y = 7
            };
            coord dest = new coord()
            {
                x = 3,
                y = 7
            };
            ToxicSprite s = new ToxicSprite(start, 33, 19, 50);
            s.SetDirection(Direction.LEFT);
            GameModel gm = new GameModel(null, null, a);
            ToxicMoveCommand com = new ToxicMoveCommand(s);
            gm.InvokeCommand(com);
            s.Update(600);
            gm.RedoNextCommand();
            s.Update(600);
            coord finalPos = new coord()
            {
                x = (int)s.GetGridPosition().x,
                y = (int)s.GetGridPosition().y
            };

            Assert.AreEqual(finalPos, dest);
        }
        [TestMethod]
        public void TestUndo_Bubbles()
        {
            coord start = new coord()
            {
                x = 4,
                y = 7
            };
            coord dest = new coord()
            {
                x = 3,
                y = 7
            };
            BubbleSprite s = new BubbleSprite(start, 33, 19, 50);
            s.SetDirection(Direction.LEFT);
            GameModel gm = new GameModel(null, null, a);
            BubbleMoveCommand com = new BubbleMoveCommand(s);
            gm.InvokeCommand(com);
            s.Update(600);
            coord finalPos = new coord()
            {
                x = (int)s.GetGridPosition().x,
                y = (int)s.GetGridPosition().y
            };

            Assert.AreEqual(finalPos, dest);
        }
        [TestMethod]
        public void TestRedo_Bubbles()
        {
            coord start = new coord()
            {
                x = 4,
                y = 7
            };
            coord dest = new coord()
            {
                x = 3,
                y = 7
            };
            BubbleSprite s = new BubbleSprite(start, 33, 19, 50);
            s.SetDirection(Direction.LEFT);
            GameModel gm = new GameModel(null, null, a);
            BubbleMoveCommand com = new BubbleMoveCommand(s);
            gm.InvokeCommand(com);
            s.Update(600);
            gm.RedoNextCommand();
            s.Update(600);
            coord finalPos = new coord()
            {
                x = (int)s.GetGridPosition().x,
                y = (int)s.GetGridPosition().y
            };

            Assert.AreEqual(finalPos, dest);
        }
        [TestMethod]
        public void TestCollisionToxicSlimus()
        {
            ControllerMOC moc = new ControllerMOC();
            GameModel gm = new GameModel(moc, null, a);

            coord start = new coord()
            {
                x = 4,
                y = 7
            };
            PlayerSprite s = new PlayerSprite(start, 33, 19, 50);
            ToxicSprite tox = new ToxicSprite(start, 33, 19, 50);
            gm.InitPlayer(s);
            gm.AddToxic(tox);
            gm.checkCollision();
            Assert.AreEqual(s.GetLives(), 2);
        }
        [TestMethod]
        public void TestCollisionSlimusGem()
        {
            ControllerMOC moc = new ControllerMOC();
            GameModel gm = new GameModel(moc, null, a);

            coord start = new coord()
            {
                x = 4,
                y = 7
            };
            PlayerSprite s = new PlayerSprite(start, 33, 19, 50);
            GemSprite gem = new GemSprite(start, 33, 19, 50, "");
            gm.InitPlayer(s);
            gm.AddGem(gem);
            gm.checkCollision();
            Assert.AreEqual(gm.GetCounter(), 1);
        }
        [TestMethod]
        public void TestCollisionToxicGem()
        {
            ControllerMOC moc = new ControllerMOC();
            GameModel gm = new GameModel(moc, null, a  );

            coord start = new coord()
            {
                x = 4,
                y = 7
            };
            coord cslimus = new coord()
            {
                x = 12,
                y = 15
            };
            PlayerSprite s = new PlayerSprite(cslimus, 33, 19, 50);
            ToxicSprite t = new ToxicSprite(start, 33, 19, 50);
            GemSprite gem = new GemSprite(start, 33, 19, 50, "");
            gm.InitPlayer(s);
            gm.AddGem(gem);
            gm.checkCollision();
            Assert.AreEqual(gm.GetCounter(), 0);
        }
        [TestMethod]
        public void TestCollisiontoxicbubble()
        {
            ControllerMOC moc = new ControllerMOC();
            GameModel gm = new GameModel(moc, null, a);
            coord start = new coord()
            {
                x = 4,
                y = 7
            };
            PlayerSprite s = new PlayerSprite(start, 33, 19, 50);
            ToxicSprite tox = new ToxicSprite(start, 33, 19, 50);
            BubbleSprite bub = new BubbleSprite(start, 33, 19, 50);
            gm.InitPlayer(s);
            gm.AddToxic(tox);
            gm.AddBubble(bub);
            gm.checkCollision();
            Assert.AreEqual(tox.GetLives(), 1);
        }
        
    }
}
