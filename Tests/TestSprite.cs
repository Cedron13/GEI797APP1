using GEI797Labo;

/* EXPLORUS-E
 * Alexis BLATRIX (blaa1406)
 * C�dric CHARRON (chac0902)
 * Audric DAVID (dava1302)
 * Matthieu JEHANNE (jehm1701)
 * Clo� LEGLISE (legc1001)
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
    }
}