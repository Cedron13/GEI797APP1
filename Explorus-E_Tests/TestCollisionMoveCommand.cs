using ExplorusE.Constants;
using ExplorusE.Models.Commands;
using ExplorusE.Models;



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
                x = 4,
                y = 7
            };
            PlayerSprite s = new PlayerSprite(start, 33, 19, 50);
            GameModel gm = new GameModel(null,null, null);
            gm.SetLabyrinth(labyrinth);
            gm.SetGridPosX(4);
            gm.SetGridPosY(7);
            gm.InitPlayer(s);

            coord dest = new coord()
            {
                x = 5,
                y = 7
            };
            MoveCommandMOC com = new MoveCommandMOC(Direction.RIGHT, start, dest);
            gm.InvokeCommand(com);
            s.Update(500);
            Assert.AreEqual(start, gm.GetGridCoord());
        }
        [TestMethod]
        public void MoveDownWithWall_WithSpritePosition()
        {
            int[,] labyrinth = {
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},  // 0 = nothing (free to go)
                {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},  // 1 = display wall
                {1, 0, 1, 1, 1, 0, 1, 1, 1, 0, 1},  // 2 = display door
                {1, 0, 0, 0, 0, 0, 1, 5, 1, 0, 1},  // 3 = display Slimus
                {1, 0, 1, 0, 1, 1, 1, 2, 1, 0, 1},  // 4 = display gem
                {1, 0, 1, 0, 1, 0, 0, 0, 0, 0, 1},  // 5 = display mini-slime
                {1, 0, 1, 0, 1, 1, 0, 1, 1, 0, 1},
                {1, 0, 0, 0, 3, 1, 0, 0, 0, 0, 1},
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
            };
            coord start = new coord()
            {
                x = 4,
                y = 7
            };
            ControllerMOC moc = new ControllerMOC();
            PlayerSprite s = new PlayerSprite(start, 33, 19, 50);
            s.StartMovement(start, Direction.DOWN);
            GameModel gm = new GameModel(moc, null, null);
            gm.SetLabyrinth(labyrinth);
            gm.SetGridPosX(4);
            gm.SetGridPosY(7);
            gm.InitPlayer(s);

            coord dest = new coord()
            {
                x = 4,
                y = 8
            };
            MoveCommandMOC com = new MoveCommandMOC(Direction.DOWN, start, dest);
            gm.InvokeCommand(com);
            s.Update(500);
            coord final = new coord()
            {
                x = (int)s.GetGridPosition().x,
                y = (int)s.GetGridPosition().y
            };

            Assert.AreEqual(start, final);
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
                x = 1,
                y = 7
            };
            PlayerSprite   s = new PlayerSprite(start, 33, 19, 50);
            GameModel gm = new GameModel(null, null, null);
            //gm.SetLabyrinth(labyrinth);
            gm.SetGridPosX(1);
            gm.SetGridPosY(7);
            gm.InitPlayer(s);

            coord dest = new coord()
            {
                x = 0,
                y = 7
            };
            MoveCommandMOC com = new MoveCommandMOC(Direction.LEFT, start, dest);
            gm.InvokeCommand(com);
            s.Update(500);
            Assert.AreEqual(start, gm.GetGridCoord());
        }
        [TestMethod]
        public void MoveUpWithWall_WithSpritePosition()
        {
            coord start = new coord()
            {
                x = 4,
                y = 7
            };
            PlayerSprite s = new PlayerSprite(start, 33, 19, 50);
            GameModel gm = new GameModel(null,null, null);

            gm.SetGridPosX(4);
            gm.SetGridPosY(7);
            gm.InitPlayer(s);

            coord dest = new coord()
            {
                x = 4,
                y = 6
            };
            MoveCommandMOC com = new MoveCommandMOC(Direction.UP, start, dest);
            gm.InvokeCommand(com);

            s.Update(500);
            Assert.AreEqual(start, gm.GetGridCoord());
        }
        //Collision avec gemme a changer donc a refaire dans physicsthread test
       /* [TestMethod]
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

            PlayerSprite s = new PlayerSprite(start, 33, 19, 50);
            ControllerMOC mocController = new ControllerMOC();
            GameModel gm = new GameModel(mocController, null);
            gm.SetLabyrinth(labyrinth);
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
        }*/
       /* [TestMethod]
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

            PlayerSprite s = new PlayerSprite(start, 33, 19, 50);
            ControllerMOC mocController = new ControllerMOC();
            GameModel gm = new GameModel(mocController, null   );
            gm.SetLabyrinth(labyrinth);
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
        }*/
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

            PlayerSprite s = new PlayerSprite(start, 33, 19, 50);
            ControllerMOC mocController = new ControllerMOC();
            GameModel gm = new GameModel(mocController, null, null);
            gm.SetLabyrinth(labyrinth);
            gm.SetGridPosX(7);
            gm.SetGridPosY(5);
            gm.InitPlayer(s);

            coord dest = new coord()
            {
                x = 7,
                y = 4
            };
            MoveCommandMOC com = new MoveCommandMOC(Direction.LEFT, start, dest);
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

            PlayerSprite s = new PlayerSprite(start, 33, 19, 50);
            ControllerMOC mocController = new ControllerMOC();
            GameModel gm = new GameModel(mocController, null, null);
            gm.SetLabyrinth(labyrinth);
            gm.SetGridPosX(7);
            gm.SetGridPosY(5);
            gm.InitPlayer(s);

            coord dest = new coord()
            {
                x = 7,
                y = 4
            };
            gm.SetCounter(6);
            MoveCommandMOC com = new MoveCommandMOC(Direction.UP, start, dest);
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

            PlayerSprite s = new PlayerSprite(start, 33, 19, 50);
            ControllerMOC mocController = new ControllerMOC();
            GameModel gm = new GameModel(mocController,null, null);
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
            MoveCommandMOC com = new MoveCommandMOC(Direction.UP, start, dest);
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
        [TestMethod]
        public void TestCollisionBubbleWall()
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
            ControllerMOC moc = new ControllerMOC();
            GameModel gm = new GameModel(moc, null, null);
            coord start = new coord()
            {
                x = 3,
                y = 7
            };
            coord dest = new coord()
            {
                x = 19,
                y = 7
            };
            coord fintheo = new coord()
            {
                x = 4,
                y = 7
            };

            gm.SetLabyrinth(labyrinth);
            BubbleSprite bub = new BubbleSprite(start, 33, 19, 50);
            //gm.AddBubble(bub);
            bub.SetDirection(Direction.RIGHT);
            //bub.StartMovement(dest, Direction.RIGHT);
            gm.InvokeCommand(new BubbleCreateCommand(bub));
            gm.InvokeCommand(new BubbleMoveCommand(bub));

            bub.Update(500);
            coord finmes = new coord()
            {
                x = (int)bub.GetGridPosition().x,
                y = (int)bub.GetGridPosition().y
            };

            Assert.AreEqual(fintheo, finmes);
        }
        [TestMethod]
        public void TestCollisionToxicWall()
        {
            int[,] labyrinth = {
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},  // 0 = nothing (free to go)
                    {1, 0, 0, 4, 0, 0, 0, 0, 0, 0, 1},  // 1 = display wall
                    {1, 0, 1, 1, 1, 0, 1, 1, 1, 0, 1},  // 2 = display door
                    {1, 0, 0, 0, 0, 0, 1, 5, 1, 0, 1},  // 3 = display Slimus
                    {1, 0, 1, 0, 1, 1, 1, 2, 1, 0, 1},  // 4 = display gem
                    {1, 0, 1, 0, 1, 4, 0, 0, 0, 0, 1},  // 5 = display mini-slime
                    {1, 0, 1, 0, 1, 1, 0, 1, 1, 0, 1},
                    {1, 0, 0, 0, 0, 1, 0, 0, 4, 0, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                };
            ControllerMOC moc = new ControllerMOC();
            GameModel gm = new GameModel(moc, null, null);
            coord start = new coord()
            {
                x = 4,
                y = 7
            };



            gm.SetLabyrinth(labyrinth);
            ToxicSprite tox = new ToxicSprite(start, 33, 19, 50);
            tox.SetDirection(Direction.RIGHT);


            //bub.StartMovement(dest, Direction.RIGHT);

            gm.InvokeCommand(new ToxicMoveCommand(tox));


            tox.Update(500);
            coord finmes = new coord()
            {
                x = (int)tox.GetGridPosition().x,
                y = (int)tox.GetGridPosition().y
            };

            Assert.AreEqual(start.x - 1, finmes.x);
        }

        [TestMethod]
        public void TestToxicWithoutWall()
        {
            int[,] labyrinth = {
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},  // 0 = nothing (free to go)
                    {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},  // 1 = display wall
                    {1, 0, 1, 1, 1, 0, 1, 1, 1, 0, 1},  // 2 = display door
                    {1, 0, 0, 0, 0, 0, 1, 5, 1, 0, 1},  // 3 = display Slimus
                    {1, 0, 1, 0, 1, 1, 1, 2, 1, 0, 1},  // 4 = display gem
                    {1, 0, 1, 0, 1, 4, 0, 0, 0, 0, 1},  // 5 = display mini-slime
                    {1, 0, 1, 0, 1, 1, 0, 1, 1, 0, 1},
                    {1, 0, 0, 0, 0, 1, 0, 0, 4, 0, 1},
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                };
            ControllerMOC moc = new ControllerMOC();
            GameModel gm = new GameModel(moc, null, null);
            coord start = new coord()
            {
                x = 3,
                y = 2
            };
            coord dest = new coord()
            {
                x = 3,
                y = 3
            };



            gm.SetLabyrinth(labyrinth);
            ToxicSprite tox = new ToxicSprite(start, 33, 19, 50);
            tox.SetDirection(Direction.RIGHT);



            gm.InvokeCommand(new ToxicMoveCommand(tox));


            tox.Update(500);
            coord finmes = new coord()
            {
                x = (int)tox.GetGridPosition().x,
                y = (int)tox.GetGridPosition().y
            };

            Assert.AreNotEqual(dest, finmes);
        }
    }
}
