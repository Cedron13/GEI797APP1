using ExplorusE.Controllers.States;
using ExplorusE.Models;
using System.Collections.Generic;

namespace Tests
{
    [TestClass]
    public class TestControllerState
    {
        [TestMethod]
        public void TestPause()
        {
            ControllerMOC moc = new ControllerMOC();
            GameModel gm = new GameModel(moc);
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
            GameModel gm = new GameModel(moc);
            IState currentState;
            PausedState pause = new PausedState(moc);
            currentState = pause;
            pause.PrepareNextState(ExplorusE.Constants.GameStates.RESUME);
            currentState = pause.GetNextState();
            Assert.IsTrue(currentState is ResumeState);
        }
    }
}
