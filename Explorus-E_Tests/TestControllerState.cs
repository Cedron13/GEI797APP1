using ExplorusE.Controllers.States;
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
    public class TestControllerState
    {
        [TestMethod]
        public void TestPause()
        {
            ControllerMOC moc = new ControllerMOC();
            GameModel gm = new GameModel(moc, null,null);
            IState currentState;
            PlayState play = new PlayState(moc);
            currentState = play;
            play.PrepareNextState(ExplorusE.Constants.GameStates.PAUSE);
            currentState = play.GetNextState();
            Assert.IsTrue(currentState is PausedState);
        }
        [TestMethod]
        public void TestResume()
        {
            ControllerMOC moc = new ControllerMOC();
            GameModel gm = new GameModel(moc, null,null);
            IState currentState;
            PausedState pause = new PausedState(moc);
            currentState = pause;
            pause.PrepareNextState(ExplorusE.Constants.GameStates.RESUME);
            currentState = pause.GetNextState();
            Assert.IsTrue(currentState is ResumeState);
        }
    }
}
