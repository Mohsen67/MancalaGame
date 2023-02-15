using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MancalaAssessment.Models.Enums;

namespace MancalaAssessment.Models.Tests
{
    [TestClass()]
    public class GameEngineTests
    {
        [TestMethod]
        public void NewGameTest()
        {
            int[] expectedPits = new int[6] { 4, 4, 4, 4, 4, 4 };
            int expectedStore = 0;
            GameState expectedGameState = GameState.InProgress;
            GameEngine ge = new GameEngine();
            IGame g = ge.NewGame();
            Assert.IsNotNull(g);
            Assert.AreEqual(expectedGameState, g.State);

            var bottomPits = g.GetPits(BoardSide.Bottom);
            CollectionAssert.AreEqual(expectedPits, bottomPits);

            var topPits = g.GetPits(BoardSide.Top);
            CollectionAssert.AreEqual(expectedPits, topPits);

            var bottomStore = g.GetStore(BoardSide.Bottom);
            Assert.AreEqual(expectedStore, bottomStore);

            var topStore = g.GetStore(BoardSide.Top);

            Assert.AreEqual(expectedStore, topStore);
        }



        [TestMethod]
        public void TurnChangesToTopPlayerTest()
        {
            Player expectedPlayer = new Player(BoardSide.Top);
            int[] pits = new int[14] { 3, 3, 3, 3, 3, 3, 0, 3, 3, 3, 3, 3, 3, 0 };

            IGame g = new Game(pits, new Player(BoardSide.Bottom), expectedPlayer, GameState.InProgress);
            GameEngine ge = new GameEngine();
            Assert.AreNotEqual(expectedPlayer.boardSide, g.ActivePlayer.boardSide);
            g = ge.Turn(g, 5);
            Assert.AreEqual(expectedPlayer.boardSide, g.ActivePlayer.boardSide);
        }

        [TestMethod]
        public void TurnDoesNotChangeBottomPlayerTest()
        {
            Player expectedPlayer = new Player(BoardSide.Bottom);
            int[] pits = new int[14] { 3, 3, 3, 3, 3, 3, 0, 3, 3, 3, 3, 3, 3, 0 };

            IGame g = new Game(pits, expectedPlayer, new Player(BoardSide.Top), GameState.InProgress);
            GameEngine ge = new GameEngine();
            Assert.AreEqual(expectedPlayer.boardSide, g.ActivePlayer.boardSide);
            g = ge.Turn(g, 3);
            Assert.AreEqual(expectedPlayer.boardSide, g.ActivePlayer.boardSide);
        }

        [TestMethod]
        public void TurnChangesToBottomPlayerTest()
        {
            Player expectedPlayer = new Player(BoardSide.Bottom);
            int[] pits = new int[14] { 3, 3, 3, 3, 3, 3, 0, 3, 3, 3, 3, 3, 3, 0 };

            IGame g = new Game(pits, new Player(BoardSide.Top), expectedPlayer, GameState.InProgress);
            GameEngine ge = new GameEngine();
            Assert.AreNotEqual(expectedPlayer, g.ActivePlayer);
            g = ge.Turn(g, 5);
            Assert.AreEqual(expectedPlayer, g.ActivePlayer);
        }

        [TestMethod]
        public void TurnDoesNotChangeTopPlayerTest()
        {
            Player expectedPlayer = new Player(BoardSide.Top);

            int[] pits = new int[14] { 3, 3, 3, 3, 3, 3, 0, 3, 3, 3, 3, 3, 3, 0 };
            IGame g = new Game(pits, expectedPlayer, new Player(BoardSide.Bottom), GameState.InProgress);
            GameEngine ge = new GameEngine();

            Assert.AreEqual(expectedPlayer, g.ActivePlayer);
            g = ge.Turn(g, 3);
            Assert.AreEqual(expectedPlayer, g.ActivePlayer); ;
        }

        [TestMethod]
        public void TurnMovesOppositeStonesBottomTest()
        {

            int[] expected = new int[14] { 3, 3, 3, 3, 0, 0, 4, 0, 3, 3, 3, 3, 3, 0 };
            GameEngine ge = new GameEngine();

            int[] pits = new int[14] { 3, 3, 3, 3, 1, 0, 0, 3, 3, 3, 3, 3, 3, 0 };
            IGame g = new Game(pits, new Player(BoardSide.Bottom), new Player(BoardSide.Top), GameState.InProgress);

            g = ge.Turn(g, 4);
            CollectionAssert.AreEqual(expected, g.BoardState);
        }

        [TestMethod]
        public void TurnMovesOppositeStonesTopTest()
        {

            int[] expected = new int[14] { 3, 0, 3, 3, 3, 3, 0, 3, 3, 3, 0, 0, 3, 7 };
            GameEngine ge = new GameEngine();

            int[] pits = new int[14] { 3, 3, 3, 3, 3, 3, 0, 3, 3, 3, 1, 0, 3, 3 };
            IGame g = new Game(pits, new Player(BoardSide.Top), new Player(BoardSide.Bottom), GameState.InProgress);

            g = ge.Turn(g, 3);
            CollectionAssert.AreEqual(expected, g.BoardState);
        }

        [TestMethod]
        public void GameFinishedDrawTest()
        {
            GameState expected = GameState.Draw;
            GameEngine ge = new GameEngine();

            int[] pits = new int[14] { 3, 3, 3, 0, 4, 4, 1, 0, 0, 0, 0, 0, 1, 0 };
            IGame g = new Game(pits, new Player(BoardSide.Top), new Player(BoardSide.Bottom), GameState.InProgress);

            g = ge.Turn(g, 5);

            Assert.AreEqual(expected, g.State);
        }

        [TestMethod]
        public void GameFinishedBottomWonTest()
        {
            GameState expected = GameState.BottomPlayerWon;
            GameEngine ge = new GameEngine();

            int[] pits = new int[14] { 0, 0, 0, 0, 0, 0, 4, 0, 0, 0, 0, 0, 1, 0 };
            IGame g = new Game(pits, new Player(BoardSide.Top), new Player(BoardSide.Bottom), GameState.InProgress);

            g = ge.Turn(g, 5);

            Assert.AreEqual(expected, g.State);
        }

        [TestMethod]
        public void GameFinishedTopWonTest()
        {
            GameState expected = GameState.TopPlayerWon;
            GameEngine ge = new GameEngine();

            int[] pits = new int[14] { 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 3 };
            IGame g = new Game(pits, new Player(BoardSide.Bottom), new Player(BoardSide.Top), GameState.InProgress);

            g = ge.Turn(g, 5);

            Assert.AreEqual(expected, g.State);
        }
    }
}