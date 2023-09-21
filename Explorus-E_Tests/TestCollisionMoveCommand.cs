using ExplorusE.Constants;
using ExplorusE.Models.Commands;
using ExplorusE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    [TestClass]
    public class TestCollisionMoveCommand
    {
        //Test with wall
        [TestMethod]
        public void MoveRightWithWall_WithSpritePosition()
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
                //x = 25,
                //y = 45
                x = 4,
                y = 7
            };
            //Controller c = new Controller();
            Sprite s = new Sprite(start, 33, 19, 50);
            GameModel gm = new GameModel(null);

            gm.SetGridPosX(4);
            gm.SetGridPosY(7);
            gm.InitPlayer(s);

            coord dest = new coord()
            {
                x = 5,
                y = 7
            };
            MoveCommand com = new MoveCommand(Direction.RIGHT, start, dest);
            gm.InvokeCommand(com);

            //gm.MoveRight(5,5,5);
            s.Update(500);
            Assert.AreEqual(start, gm.GetGridCoord());
        }
        [TestMethod]
        public void MoveDownWithWall_WithSpritePosition()
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
                //x = 25,
                //y = 45
                x = 4,
                y = 7
            };
            //Controller c = new Controller();
            Sprite s = new Sprite(start, 33, 19, 50);
            GameModel gm = new GameModel(null);

            gm.SetGridPosX(4);
            gm.SetGridPosY(7);
            gm.InitPlayer(s);

            coord dest = new coord()
            {
                x = 4,
                y = 8
            };
            MoveCommand com = new MoveCommand(Direction.DOWN, start, dest);
            gm.InvokeCommand(com);

            //gm.MoveRight(5,5,5);
            s.Update(500);
            Assert.AreEqual(start, gm.GetGridCoord());
        }
        [TestMethod]
        public void MoveLeftWithWall_WithSpritePosition()
        {
            int[,] labyrinth = {
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},  // 0 = nothing (free to go)
                {1, 0, 0, 4, 0, 0, 0, 0, 0, 0, 1},  // 1 = display wall
                {1, 0, 1, 1, 1, 0, 1, 1, 1, 0, 1},  // 2 = display door
                {1, 0, 0, 0, 0, 0, 1, 5, 1, 0, 1},  // 3 = display Slimus
                {1, 0, 1, 0, 1, 1, 1, 2, 1, 0, 1},  // 4 = display gem
                {1, 0, 1, 0, 1, 4, 0, 0, 0, 0, 1},  // 5 = display mini-slime
                {1, 0, 1, 0, 1, 1, 0, 1, 1, 0, 1},
                {1, 3, 0, 0, 0, 1, 0, 0, 4, 0, 1},
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
            };
            coord start = new coord()
            {
                //x = 25,
                //y = 45
                x = 1,
                y = 7
            };
            //Controller c = new Controller();
            Sprite s = new Sprite(start, 33, 19, 50);
            GameModel gm = new GameModel(null);

            gm.SetGridPosX(1);
            gm.SetGridPosY(7);
            gm.InitPlayer(s);

            coord dest = new coord()
            {
                x = 0,
                y = 7
            };
            MoveCommand com = new MoveCommand(Direction.LEFT, start, dest);
            gm.InvokeCommand(com);

            //gm.MoveRight(5,5,5);
            s.Update(500);
            Assert.AreEqual(start, gm.GetGridCoord());
        }
        [TestMethod]
        public void MoveUpWithWall_WithSpritePosition()
        {
            coord start = new coord()
            {
                //x = 25,
                //y = 45
                x = 4,
                y = 7
            };
            //Controller c = new Controller();
            Sprite s = new Sprite(start, 33, 19, 50);
            GameModel gm = new GameModel(null);

            gm.SetGridPosX(4);
            gm.SetGridPosY(7);
            gm.InitPlayer(s);

            coord dest = new coord()
            {
                x = 4,
                y = 6
            };
            MoveCommand com = new MoveCommand(Direction.UP, start, dest);
            gm.InvokeCommand(com);

            //gm.MoveRight(5,5,5);
            s.Update(500);
            Assert.AreEqual(start, gm.GetGridCoord());
        }
        [TestMethod]
        public void TestExecute_MoveCommand_WithGem()
        {
            int[,] labyrinth = {
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},  // 0 = nothing (free to go)
                {1, 0, 0, 4, 3, 0, 0, 0, 0, 0, 1},  // 1 = display wall
                {1, 0, 1, 1, 1, 0, 1, 1, 1, 0, 1},  // 2 = display door
                {1, 0, 0, 0, 0, 0, 1, 5, 1, 0, 1},  // 3 = display Slimus
                {1, 0, 1, 0, 1, 1, 1, 2, 1, 0, 1},  // 4 = display gem
                {1, 0, 1, 0, 1, 4, 0, 0, 0, 0, 1},  // 5 = display mini-slime
                {1, 0, 1, 0, 1, 1, 0, 1, 1, 0, 1},
                {1, 0, 0, 0, 0, 1, 0, 0, 4, 0, 1},
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
            };
            coord start = new coord()
            {
                x = 4,
                y = 1
            };

            Sprite s = new Sprite(start, 33, 19, 50);
            ControllerMOC mocController = new ControllerMOC();
            GameModel gm = new GameModel(mocController);

            gm.SetGridPosX(4);
            gm.SetGridPosY(1);
            gm.InitPlayer(s);

            coord dest = new coord()
            {
                x = 3,
                y = 1
            };
            MoveCommand com = new MoveCommand(Direction.LEFT, start, dest);
            gm.InvokeCommand(com);
            s.Update(500);

            Assert.AreEqual(1, gm.GetCounter());
        }
        [TestMethod]
        public void TestUndo_MoveCommand_WithGem()
        {
            int[,] labyrinth = {
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},  // 0 = nothing (free to go)
                {1, 0, 0, 4, 3, 0, 0, 0, 0, 0, 1},  // 1 = display wall
                {1, 0, 1, 1, 1, 0, 1, 1, 1, 0, 1},  // 2 = display door
                {1, 0, 0, 0, 0, 0, 1, 5, 1, 0, 1},  // 3 = display Slimus
                {1, 0, 1, 0, 1, 1, 1, 2, 1, 0, 1},  // 4 = display gem
                {1, 0, 1, 0, 1, 4, 0, 0, 0, 0, 1},  // 5 = display mini-slime
                {1, 0, 1, 0, 1, 1, 0, 1, 1, 0, 1},
                {1, 0, 0, 0, 0, 1, 0, 0, 4, 0, 1},
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
            };
            coord start = new coord()
            {
                x = 4,
                y = 1
            };

            Sprite s = new Sprite(start, 33, 19, 50);
            ControllerMOC mocController = new ControllerMOC();
            GameModel gm = new GameModel(mocController);

            gm.SetGridPosX(4);
            gm.SetGridPosY(1);
            gm.InitPlayer(s);

            coord dest = new coord()
            {
                x = 3,
                y = 1
            };
            MoveCommand com = new MoveCommand(Direction.LEFT, start, dest);
            gm.InvokeCommand(com);
            s.Update(500);
            gm.UndoLastCommand();

            Assert.AreEqual(0, gm.GetCounter());
        }
        [TestMethod]
        public void TestExecute_MoveCommand_WithLockedDoor()
        {
            int[,] labyrinth = {
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},  // 0 = nothing (free to go)
                {1, 0, 0, 4, 0, 0, 0, 0, 0, 0, 1},  // 1 = display wall
                {1, 0, 1, 1, 1, 0, 1, 1, 1, 0, 1},  // 2 = display door
                {1, 0, 0, 0, 0, 0, 1, 5, 1, 0, 1},  // 3 = display Slimus
                {1, 0, 1, 0, 1, 1, 1, 2, 1, 0, 1},  // 4 = display gem
                {1, 0, 1, 0, 1, 4, 0, 3, 0, 0, 1},  // 5 = display mini-slime
                {1, 0, 1, 0, 1, 1, 0, 1, 1, 0, 1},
                {1, 0, 0, 0, 0, 1, 0, 0, 4, 0, 1},
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
            };
            coord start = new coord()
            {
                x = 7,
                y = 5
            };

            Sprite s = new Sprite(start, 33, 19, 50);
            ControllerMOC mocController = new ControllerMOC();
            GameModel gm = new GameModel(mocController);

            gm.SetGridPosX(7);
            gm.SetGridPosY(5);
            gm.InitPlayer(s);

            coord dest = new coord()
            {
                x = 7,
                y = 4
            };
            MoveCommand com = new MoveCommand(Direction.LEFT, start, dest);
            gm.InvokeCommand(com);
            s.Update(500);
            coord finalPos = new coord()
            {
                x = (int)s.GetGridPosition().x,
                y = (int)s.GetGridPosition().y
            };

            Assert.AreEqual(start, finalPos);
        }
        [TestMethod]
        public void TestExecute_MoveCommand_WithUnlockedDoor()
        {
            int[,] labyrinth = {
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},  // 0 = nothing (free to go)
                {1, 0, 0, 4, 0, 0, 0, 0, 0, 0, 1},  // 1 = display wall
                {1, 0, 1, 1, 1, 0, 1, 1, 1, 0, 1},  // 2 = display door
                {1, 0, 0, 0, 0, 0, 1, 5, 1, 0, 1},  // 3 = display Slimus
                {1, 0, 1, 0, 1, 1, 1, 2, 1, 0, 1},  // 4 = display gem
                {1, 0, 1, 0, 1, 4, 0, 3, 0, 0, 1},  // 5 = display mini-slime
                {1, 0, 1, 0, 1, 1, 0, 1, 1, 0, 1},
                {1, 0, 0, 0, 0, 1, 0, 0, 4, 0, 1},
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
            };
            coord start = new coord()
            {
                x = 7,
                y = 5
            };

            Sprite s = new Sprite(start, 33, 19, 50);
            ControllerMOC mocController = new ControllerMOC();
            GameModel gm = new GameModel(mocController);

            gm.SetGridPosX(7);
            gm.SetGridPosY(5);
            gm.InitPlayer(s);

            coord dest = new coord()
            {
                x = 7,
                y = 4
            };
            gm.SetCounter(3);
            MoveCommand com = new MoveCommand(Direction.UP, start, dest);
            gm.InvokeCommand(com);
            s.Update(500);
            coord finalPos = new coord()
            {
                x = (int)s.GetGridPosition().x,
                y = (int)s.GetGridPosition().y
            };

            Assert.AreEqual(dest, finalPos);
        }
        [TestMethod]
        public void TestUndo_MoveCommand_WithUnlockedDoor()
        {
            int[,] labyrinth = {
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},  // 0 = nothing (free to go)
                {1, 0, 0, 4, 0, 0, 0, 0, 0, 0, 1},  // 1 = display wall
                {1, 0, 1, 1, 1, 0, 1, 1, 1, 0, 1},  // 2 = display door
                {1, 0, 0, 0, 0, 0, 1, 5, 1, 0, 1},  // 3 = display Slimus
                {1, 0, 1, 0, 1, 1, 1, 2, 1, 0, 1},  // 4 = display gem
                {1, 0, 1, 0, 1, 4, 0, 3, 0, 0, 1},  // 5 = display mini-slime
                {1, 0, 1, 0, 1, 1, 0, 1, 1, 0, 1},
                {1, 0, 0, 0, 0, 1, 0, 0, 4, 0, 1},
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
            };
            coord start = new coord()
            {
                x = 7,
                y = 5
            };

            Sprite s = new Sprite(start, 33, 19, 50);
            ControllerMOC mocController = new ControllerMOC();
            GameModel gm = new GameModel(mocController);
            gm.SetLabyrinth(labyrinth);

            gm.SetGridPosX(7);
            gm.SetGridPosY(5);
            gm.InitPlayer(s);

            coord dest = new coord()
            {
                x = 7,
                y = 4
            };
            gm.SetCounter(3);
            MoveCommand com = new MoveCommand(Direction.UP, start, dest);
            gm.InvokeCommand(com);
            s.Update(500);
            gm.UndoLastCommand();
            coord finalPos = new coord()
            {
                x = (int)s.GetGridPosition().x,
                y = (int)s.GetGridPosition().y
            };

            Assert.AreEqual(labyrinth, gm.GetLabyrinth());
        }
    }
}
