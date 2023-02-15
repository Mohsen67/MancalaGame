using MancalaAssessment.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MancalaAssessment.Models
{
    /// <summary>
    /// Controls the logic of the game
    /// </summary>
    public class GameEngine : IGameEngine
    {
        #region CONSTANTS
        private const int GAME_STONES = 4;
        #endregion

        /// <summary>
        /// Initializes a new instance of the<see cref="GameEngine"/> class.
        /// </summary>
        public GameEngine()
        {
        }
        /// <summary>
        /// Starts new Game
        /// </summary>
        /// <returns><see cref="IGame"></returns>
        public IGame NewGame()
        {
            int[] board = CreateBoard();
            Player p1 = new Player(BoardSide.Bottom);
            Player p2 = new Player(BoardSide.Top);

            return new Game(board, p1, p2, GameState.InProgress);
        }

        /// <summary>
        /// Makes turn for active player
        /// </summary>
        /// <param name="game">Current game state <see cref="IGame"/></param>
        /// <param name="pitIndex">Selected pit, must be valid pit index <see cref="IGame.GetValidPitIndexes"/></param>
        /// <returns>Game state after the turn <see cref="IGame"/></returns>
        /// <exception cref="ArgumentOutOfRangeException">Throws if provided pit index is not valid pit index<see cref="Game.GetValidPitIndexes"/</exception>
        public IGame Turn(IGame game, int pitIndex)
        {
            int[] possibleIndexes = game.GetValidPitIndexes();

            if (!possibleIndexes.Contains(pitIndex))
            {
                throw new ArgumentOutOfRangeException(nameof(pitIndex));
            }
            Board board = new Board(game.BoardState);
            int index = board.ToAbsolutePitIndex(game.ActivePlayer.boardSide, pitIndex);

            index = SpleetStones(board, index);
            TakeOppositPit(game, board, index);

            if (IsFinished(game))
            {
                return new Game(board.Pits, game.ActivePlayer, game.PassivePlayer, GetFinishedState(game));
            }
            if (TurnChangesPlayer(game, board, index))
            {
                return new Game(board.Pits, game.PassivePlayer, game.ActivePlayer, GameState.InProgress);
            }

            return new Game(board.Pits, game.ActivePlayer, game.PassivePlayer, GameState.InProgress);

        }

        private static GameState GetFinishedState(IGame game)
        {
            int topPlayerScore = game.GetStore(BoardSide.Top);
            int bottomPlayerScore = game.GetStore(BoardSide.Bottom);

            if (topPlayerScore == bottomPlayerScore)
            {
                return GameState.Draw;
            }

            return bottomPlayerScore > topPlayerScore ? GameState.BottomPlayerWon : GameState.TopPlayerWon;
        }
        private static bool IsFinished(IGame game)
        {
            return game.GetPits(BoardSide.Bottom).All(i => i == 0) || game.GetPits(BoardSide.Top).All(i => i == 0);
        }

        private static bool TurnChangesPlayer(IGame game, Board board, int index)
        {
            Player activePlayer = game.ActivePlayer;
            return !(board.IsStore(index) && board.GetSideByIndex(index) == activePlayer.boardSide);
        }

        private static void TakeOppositPit(IGame game, Board board, int index)
        {
            Player activePlayer = game.ActivePlayer;
            int[] pits = board.Pits;
            if (index <= pits.Length / 2 && board.GetSideByIndex(index) == activePlayer.boardSide && pits[index] == 1)
            {
                pits[board.GetStoreIndex(activePlayer.boardSide)] = pits[board.GetStoreIndex(activePlayer.boardSide)] + pits[index] + pits[board.GetOppositIndex(index)];
                pits[index] = 0;
                pits[board.GetOppositIndex(index)] = 0;
            }
        }
        private static int SpleetStones(Board board, int index)
        {
            int[] pits = board.Pits;
            int stones = board.Pits[index];

            pits[index] = 0;
            while (stones > 0)
            {
                index++;
                if (index >= pits.Length)
                {
                    index = 0;
                }

                pits[index]++;

                stones--;
            }
            return index;
        }

        private static int[] CreateBoard()
        {
            int[] pits = new int[Board.BOARD_SIZE];

            Array.Fill(pits, GAME_STONES);
            pits[Board.BOARD_SIZE - 1] = 0;
            pits[Board.BOARD_SIZE / 2 - 1] = 0;

            return pits;
        }
    }
}
