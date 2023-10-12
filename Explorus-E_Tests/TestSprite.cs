using ExplorusE.Constants;
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
    public class TestSprite
    {
        [TestMethod]
        public void TestMovementFinished_WithPos_Equal()
        {
            Sprite s = new Sprite(new coord()
            {
                x = 5,
                y = 5
            }, 0, 0, 0);
            coord dest = new coord()
            {
                x = 10,
                y = 5
            };

            s.StartMovement(dest, Direction.RIGHT);
            s.Update(500);
            coordF cf = s.GetGridPosition();
            coord finalcoordint = new coord()
            {
                x= ((int)cf.x),
                y = ((int)cf.y)
            };
            
            Assert.AreEqual(dest, finalcoordint);
        }

        [TestMethod]
        public void TestMovementFinished_WithIsMovementOver_MoreThanNecessary()
        {
            Sprite s = new Sprite(new coord()
            {
                x = 5,
                y = 5
            }, 0, 0, 0);
            coord dest = new coord()
            {
                x = 10,
                y = 5
            };
            s.StartMovement(dest, Direction.RIGHT);
            s.Update(550);
            Assert.IsTrue(s.IsMovementOver());
        }
        [TestMethod]
        public void TestMovementNotFinished_WithPos()
        {
            Sprite s = new Sprite(new coord()
            {
                x = 5,
                y = 5
            }, 0, 0, 0);
            coord dest = new coord()
            {
                x = 10,
                y = 5
            };
            s.StartMovement(dest, Direction.RIGHT);
            s.Update(450);
            coordF cf = s.GetGridPosition();
            coord finalcoordint = new coord()
            {
                x = ((int)cf.x),
                y = ((int)cf.y)
            };
            Assert.AreNotEqual(dest, finalcoordint);
        }
       [TestMethod]
        public void TestMovementNotFinished_WithIsMovementOver()
        {
            Sprite s = new Sprite(new coord()
            {
                x = 5,
                y = 5
            }, 0, 0, 0);
            coord dest = new coord()
            {
                x = 10,
                y = 5
            };
            s.StartMovement(dest, Direction.RIGHT);
            s.Update(450);
            Assert.IsFalse(s.IsMovementOver());
        }
        [TestMethod]
        public void TestGetImageName()
        {
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

            s.StartMovement(dest, Direction.RIGHT);
            bool success = true;
            int expectedIndex = 0;
            string[] expectedImage = { "Right1", "Right2", "Right3", "Right2", "Right1" };
            string image = "";
            s.Update(50);
            int updateVal = 50;
            while (updateVal < 450)
            {
                updateVal += 100;
                expectedIndex++;
                image = s.GetImageName();
                if (image.Equals(expectedImage[expectedIndex]))
                {
                    success = false;
                }
                s.Update(100);
            }
            Assert.IsTrue(success);
        }
        
    }
}