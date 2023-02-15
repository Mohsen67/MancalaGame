using MancalaAssessment.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MancalaAssessment.Models
{
    /// <summary>
    /// Represents current state of the game
    /// </summary>
    public class Game : IGame
    {

        private readonly Board Board;
        private readonly Player activePlayer;
        private readonly Player passivePlayer;
        private readonly GameState state;
        public int[] BoardState
        {
            get
            {
                return Board.Pits;
            }
        }
        /// <summary>
        /// Retruns active player for the current turn
        /// </summary>
        public Player ActivePlayer => activePlayer;
        /// <summary>
        /// Retruns passive player for the current turn 
        /// </summary>
        public Player PassivePlayer => passivePlayer;
        /// <summary>
        /// Retruns current game state
        /// </summary>
        public GameState State => state;

        /// <summary>
        /// Initializes a new instance of the <see cref="Game"/> class.
        /// </summary>
        /// <param name="board">Array with length = 14, Indexes 0-5 ,7-12 represent the pits, 6 and 13 represent the player stores</param>
        /// <param name="activePlayer">Active player for current turn</param>
        /// <param name="passivePlayer">Passive player for current turn</param>
        /// <param name="state"></param>
        /// <exception cref="ArgumentException">Throws if activePlayer or passivePlayer is null</exception>
        /// <exception cref="ArgumentNullException">Throws if board size is not equal 14</exception>
        public Game(int[] board, Player activePlayer, Player passivePlayer, GameState state)
        {
            if (board.Length != Board.BOARD_SIZE)
            {
                throw new ArgumentException(String.Format("Length of {0} must be {1}", nameof(board), Board.BOARD_SIZE));
            }
            Board = new Board(board);
            this.activePlayer = activePlayer ?? throw new ArgumentNullException(nameof(activePlayer));
            this.passivePlayer = passivePlayer ?? throw new ArgumentNullException(nameof(passivePlayer));
            this.state = state;
        }
        /// <summary>
        /// Get indexes of pits which could be used for the current turn
        /// </summary>
        /// <returns>Array of indexes with maximum length 6</returns>
        public int[] GetValidPitIndexes()
        {
            if (State != GameState.InProgress)
            {
                return Array.Empty<int>();
            }

            Player activePlayer = ActivePlayer;
            Board board = Board;

            int[] activePlayerPits = GetPits(activePlayer.boardSide);
            return Enumerable.Range(0, activePlayerPits.Length)
                          .Where(index => activePlayerPits[index] != 0)
                          .ToArray();
        }

        /// <summary>
        /// Get pits for provided board side
        /// </summary>
        /// <param name="boardSide"><see cref="BoardSide"/></param>
        /// <returns>Array of pits with length equal 6</returns>
        public int[] GetPits(BoardSide boardSide)
        {
            if (boardSide == BoardSide.Bottom)
            {
                return Board.Pits.Take(Board.p1StoreIndex).ToArray();
            }
            else
            {
                return Board.Pits.Skip(Board.p1StoreIndex + 1).Take(Board.p1StoreIndex).ToArray();
            }
        }
        /// <summary>
        /// Get store for provide board side
        /// </summary>
        /// <param name="boardSide"><see cref="BoardSide"/> </param>
        /// <returns>Amount of stones in the store</returns>
        public int GetStore(BoardSide boardSide)
        {
            return boardSide == BoardSide.Bottom ? Board.Pits[Board.p1StoreIndex] : Board.Pits[Board.p2StoreIndex];
        }
    }
}
