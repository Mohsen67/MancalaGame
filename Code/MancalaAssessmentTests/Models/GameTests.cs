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
    public class GameTests
    {
        [TestMethod()]
        public void GetBottomPitsTest()
        {
            int[] expected = new int[6] { 1, 2, 3, 4, 5, 6 };

            int[] pits = new int[14] { 1, 2, 3, 4, 5, 6, 1, 1, 1, 1, 1, 1, 1, 1 };
            Game g = new Game(pits, new Player(BoardSide.Bottom), new Player(BoardSide.Top), GameState.InProgress);
            int[] bottom = g.GetPits(BoardSide.Bottom);
            CollectionAssert.AreEqual(expected, bottom);
        }

        [TestMethod]
        public void GetTopPitsTest()
        {
            int[] expected = new int[6] { 1, 2, 3, 4, 5, 6 };

            int[] pits = new int[14] { 1, 1, 1, 1, 1, 1, 1, 1, 2, 3, 4, 5, 6, 1 };
            Game g = new Game(pits, new Player(BoardSide.Bottom), new Player(BoardSide.Top), GameState.InProgress);
            int[] top = g.GetPits(BoardSide.Top);
            CollectionAssert.AreEqual(expected, top);
        }

        [TestMethod]
        public void GetTopStoreTest()
        {
            int expected = 3;

            int[] pits = new int[14] { 1, 1, 1, 1, 1, 1, 6, 1, 1, 2, 1, 4, 5, 3 };
            Game g = new Game(pits, new Player(BoardSide.Bottom), new Player(BoardSide.Top), GameState.InProgress);

            int topStore = g.GetStore(BoardSide.Top);

            Assert.AreEqual(expected, topStore);

        }


        [TestMethod]
        public void GetBottomStoreTest()
        {
            int expected = 3;

            int[] pits = new int[14] { 1, 1, 1, 1, 1, 1, 3, 1, 1, 2, 1, 4, 5, 1 };
            Game g = new Game(pits, new Player(BoardSide.Bottom), new Player(BoardSide.Top), GameState.InProgress);

            int bottomStore = g.GetStore(BoardSide.Bottom);

            Assert.AreEqual(expected, bottomStore);

        }

        [TestMethod()]
        public void GetValidTopPitIndexesNoValidTest()
        {
            int[] expected = Array.Empty<int>();
            int[] pits = new int[14] { 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0, 0, 1 };

            Game g = new Game(pits, new Player(BoardSide.Top), new Player(BoardSide.Bottom), GameState.InProgress);
            int[] validIndexesForTop = g.GetValidPitIndexes();

            CollectionAssert.AreEqual(expected, validIndexesForTop);
        }

        [TestMethod()]
        public void GetValidBottomPitIndexesNoValidTest()
        {
            int[] expected = Array.Empty<int>();
            int[] pits = new int[14] { 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0, 0, 1 };

            Game g = new Game(pits, new Player(BoardSide.Bottom), new Player(BoardSide.Bottom), GameState.InProgress);
            int[] validIndexesForTop = g.GetValidPitIndexes();

            CollectionAssert.AreEqual(expected, validIndexesForTop);
        }



        [TestMethod()]
        public void GetValidTopPitIndexesTest()
        {
            int[] expected = new int[1] { 1 };
            int[] pits = new int[14] { 1, 0, 4, 0, 0, 0, 3, 0, 3, 0, 0, 0, 0, 1 };

            Game g = new Game(pits, new Player(BoardSide.Top), new Player(BoardSide.Bottom), GameState.InProgress);
            int[] validIndexesForTop = g.GetValidPitIndexes();

            CollectionAssert.AreEqual(expected, validIndexesForTop);
        }

        [TestMethod()]
        public void GetValidBottomPitIndexesTest()
        {
            int[] expected = new int[2] { 0, 2 };
            int[] pits = new int[14] { 1, 0, 4, 0, 0, 0, 3, 0, 3, 0, 0, 0, 0, 1 };

            Game g = new Game(pits, new Player(BoardSide.Bottom), new Player(BoardSide.Top), GameState.InProgress);
            int[] validIndexesForBottom = g.GetValidPitIndexes();

            CollectionAssert.AreEqual(expected, validIndexesForBottom);
        }
    }
}