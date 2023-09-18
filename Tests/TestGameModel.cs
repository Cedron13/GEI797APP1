using GEI797Labo;
using GEI797Labo.Controllers;
using GEI797Labo.Models;
using GEI797Labo.Models.Commands;
using System.Reflection;

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
            Controller c = new Controller();
            Sprite s = new Sprite(start,33,19,50);
            GameModel gm = new GameModel(c);

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
            Assert.AreEqual(start, gm.GetGridCoord);
        }

    //    [TestMethod]
    //    public void MoveRightWithWall_WithLabyrinth()
    //    {
    //        int[,] labyrinth = {
    //            {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},  // 0 = nothing (free to go)
    //            {1, 0, 0, 4, 0, 0, 0, 0, 0, 0, 1},  // 1 = display wall
    //            {1, 0, 1, 1, 1, 0, 1, 1, 1, 0, 1},  // 2 = display door
    //            {1, 0, 0, 0, 0, 0, 1, 5, 1, 0, 1},  // 3 = display Slimus
    //            {1, 0, 1, 0, 1, 1, 1, 2, 1, 0, 1},  // 4 = display gem
    //            {1, 0, 1, 0, 1, 4, 0, 0, 0, 0, 1},  // 5 = display mini-slime
    //            {1, 0, 1, 0, 1, 1, 0, 1, 1, 0, 1},
    //            {1, 0, 0, 0, 3, 1, 0, 0, 4, 0, 1},
    //            {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
    //        };
    //        coord start = new coord()
    //        {
    //            x = 25,
    //            y = 45
    //        };
    //        Sprite s = new Sprite(start);
    //        GameModel gm = new GameModel();
    //        gm.SetGridPosX(4);
    //        gm.SetGridPosY(7);
    //        gm.InitPlayer(s);
    //        gm.MoveRight(5, 5, 5);
    //        s.Update(500);
    //        Assert.AreEqual(labyrinth[7,4], gm.GetLabyrinth()[7,4]);
    //    }

    //    [TestMethod]
    //    public void MoveLeftWithoutWall_WithSpritePosition()
    //    {
    //        int[,] labyrinth = {
    //            {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},  // 0 = nothing (free to go)
    //            {1, 0, 0, 4, 0, 0, 0, 0, 0, 0, 1},  // 1 = display wall
    //            {1, 0, 1, 1, 1, 0, 1, 1, 1, 0, 1},  // 2 = display door
    //            {1, 0, 0, 0, 0, 0, 1, 5, 1, 0, 1},  // 3 = display Slimus
    //            {1, 0, 1, 0, 1, 1, 1, 2, 1, 0, 1},  // 4 = display gem
    //            {1, 0, 1, 0, 1, 4, 0, 0, 0, 0, 1},  // 5 = display mini-slime
    //            {1, 0, 1, 0, 1, 1, 0, 1, 1, 0, 1},
    //            {1, 0, 0, 0, 3, 1, 0, 0, 4, 0, 1},
    //            {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
    //        };
    //        coord start = new coord()
    //        {
    //            x = 25,
    //            y = 45
    //        };
    //        Sprite s = new Sprite(start);
    //        GameModel gm = new GameModel();
    //        gm.SetGridPosX(4);
    //        gm.SetGridPosY(7);
    //        gm.InitPlayer(s);
    //        gm.MoveLeft(5, 5, 5);
    //        s.Update(500);
    //        Assert.AreNotEqual(start, s.GetPosition());
    //    }

    //    [TestMethod]
    //    public void MoveLeftWithoutWall_WithLabyrinth()
    //    {
    //        int[,] labyrinth = {
    //            {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},  // 0 = nothing (free to go)
    //            {1, 0, 0, 4, 0, 0, 0, 0, 0, 0, 1},  // 1 = display wall
    //            {1, 0, 1, 1, 1, 0, 1, 1, 1, 0, 1},  // 2 = display door
    //            {1, 0, 0, 0, 0, 0, 1, 5, 1, 0, 1},  // 3 = display Slimus
    //            {1, 0, 1, 0, 1, 1, 1, 2, 1, 0, 1},  // 4 = display gem
    //            {1, 0, 1, 0, 1, 4, 0, 0, 0, 0, 1},  // 5 = display mini-slime
    //            {1, 0, 1, 0, 1, 1, 0, 1, 1, 0, 1},
    //            {1, 0, 0, 0, 3, 1, 0, 0, 4, 0, 1},
    //            {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
    //        };
    //        coord start = new coord()
    //        {
    //            x = 25,
    //            y = 45
    //        };
    //        Sprite s = new Sprite(start);
    //        GameModel gm = new GameModel();
    //        gm.SetGridPosX(4);
    //        gm.SetGridPosY(7);
    //        gm.InitPlayer(s);
    //        gm.MoveLeft(5, 5, 5);
    //        s.Update(500);
    //        Assert.AreNotEqual(labyrinth[7,4], gm.GetLabyrinth()[7,4]);
    //    }
    }
}
