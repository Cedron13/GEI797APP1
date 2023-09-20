using ExplorusE.Models;
using ExplorusE.Models.Commands;

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
            Sprite s = new Sprite(start,33,19,50);
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
                y = 6
            };
            MoveCommand com = new MoveCommand(Direction.UP, start, dest);
            gm.InvokeCommand(com);

            //gm.MoveRight(5,5,5);
            s.Update(500);
            Assert.AreEqual(start, gm.GetGridCoord());
        }

        //Test without wall
        [TestMethod]
        public void MoveLeftWithoutWall_WithSpritePosition()
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
            Sprite s = new Sprite(start, 33, 19, 50);
            GameModel gm = new GameModel(null);
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
            int[,] labyrinth = {
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},  // 0 = nothing (free to go)
                    {1, 0, 0, 4, 0, 0, 0, 0, 0, 0, 1},  // 1 = display wall
                    {1, 0, 1, 1, 1, 0, 1, 1, 1, 0, 1},  // 2 = display door
                    {1, 0, 0, 0, 0, 0, 1, 5, 1, 0, 1},  // 3 = display Slimus
                    {1, 0, 1, 0, 1, 1, 1, 2, 1, 0, 1},  // 4 = display gem
                    {1, 0, 1, 0, 1, 4, 0, 0, 0, 0, 1},  // 5 = display mini-slime
                    {1, 0, 1, 0, 1, 1, 0, 1, 1, 0, 1},
                    {1, 0, 0, 3, 0, 1, 0, 0, 4, 0, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                };
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
            Sprite s = new Sprite(start, 33, 19, 50);
            GameModel gm = new GameModel(null);
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
            int[,] labyrinth = {
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},  // 0 = nothing (free to go)
                    {1, 0, 0, 4, 0, 0, 0, 0, 0, 0, 1},  // 1 = display wall
                    {1, 0, 1, 1, 1, 0, 1, 1, 1, 0, 1},  // 2 = display door
                    {1, 0, 0, 0, 0, 0, 1, 5, 1, 0, 1},  // 3 = display Slimus
                    {1, 0, 1, 0, 1, 1, 1, 2, 1, 0, 1},  // 4 = display gem
                    {1, 0, 1, 0, 1, 4, 0, 0, 0, 0, 1},  // 5 = display mini-slime
                    {1, 0, 1, 0, 1, 1, 0, 1, 1, 0, 1},
                    {1, 0, 0, 3, 0, 1, 0, 0, 4, 0, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                };
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
            Sprite s = new Sprite(start, 33, 19, 50);
            GameModel gm = new GameModel(null);
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
            int[,] labyrinth = {
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},  // 0 = nothing (free to go)
                    {1, 0, 0, 4, 0, 0, 0, 0, 0, 0, 1},  // 1 = display wall
                    {1, 0, 1, 1, 1, 0, 1, 1, 1, 0, 1},  // 2 = display door
                    {1, 0, 0, 0, 0, 0, 1, 5, 1, 0, 1},  // 3 = display Slimus
                    {1, 0, 1, 0, 1, 1, 1, 2, 1, 0, 1},  // 4 = display gem
                    {1, 0, 1, 0, 1, 4, 0, 0, 0, 0, 1},  // 5 = display mini-slime
                    {1, 0, 1, 3, 1, 1, 0, 1, 1, 0, 1},
                    {1, 0, 0, 0, 0, 1, 0, 0, 4, 0, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                };
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
            Sprite s = new Sprite(start, 33, 19, 50);
            GameModel gm = new GameModel(null);
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
            Sprite s = new Sprite(start, 33, 19, 50);
            GameModel gm = new GameModel(null);
            gm.SetGridPosX(4);
            gm.SetGridPosY(7);
            gm.InitPlayer(s);
            MoveCommand com = new MoveCommand(Direction.RIGHT, start, dest);
            gm.InvokeCommand(com);
            s.Update(500);
            
            Assert.AreEqual(labyrinth[7, 4], gm.GetLabyrinth()[7, 4]);
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
            Sprite s = new Sprite(start, 33, 19, 50);
            GameModel gm = new GameModel(null);
            gm.SetGridPosX(4);
            gm.SetGridPosY(7);
            gm.InitPlayer(s);
            MoveCommand com = new MoveCommand(Direction.LEFT, start, dest);
            gm.InvokeCommand(com);
            s.Update(500);
            Assert.AreNotEqual(labyrinth[7, 4], gm.GetLabyrinth()[7, 4]);
        }
    }
}
