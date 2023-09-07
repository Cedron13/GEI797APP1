using GEI797Labo;

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
            });
            coord dest = new coord()
            {
                x = 10,
                y = 5
            };
            s.StartMovement(dest, Direction.RIGHT);
            s.Update(500);
            Assert.AreEqual(dest, s.GetPosition());
        }

        [TestMethod]
        public void TestMovementFinished_WithIsMovementOver_MoreThanNecessary()
        {
            Sprite s = new Sprite(new coord()
            {
                x = 5,
                y = 5
            });
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
            });
            coord dest = new coord()
            {
                x = 10,
                y = 5
            };
            s.StartMovement(dest, Direction.RIGHT);
            s.Update(450);
            Assert.AreNotEqual(dest, s.GetPosition());
        }
        [TestMethod]
        public void TestMovementNotFinished_WithIsMovementOver()
        {
            Sprite s = new Sprite(new coord()
            {
                x = 5,
                y = 5
            });
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
        public void TestMovementAnimation()
        {
            Sprite s = new Sprite(new coord()
            {
                x = 5,
                y = 5
            });
            coord dest = new coord()
            {
                x = 10,
                y = 5
            };
            s.StartMovement(dest, Direction.RIGHT);
            bool success = true;
            int expectedIndex = 0;
            string expectedImage = { "Right1", "Right2", "Right3", "Right2", "Right1" };
            string image = "";
            s.Update(50);
            while (updateVal < 500)
            {
                expectedIndex++;
                s.Update(100);
                image = s.GetImageName();
                if(image != expectedImage[expectedIndex])
                {
                    success = false;
                }
            }
            Assert.IsTrue(success);
        }
    }
}