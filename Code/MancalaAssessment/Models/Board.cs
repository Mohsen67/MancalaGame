using MancalaAssessment.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MancalaAssessment.Models
{
    /// <summary>
    /// Wraps board array
    /// </summary>
    internal class Board
    {
        internal const int BOARD_SIZE = 14;
        internal readonly int[] Pits;
        internal readonly int p1StoreIndex;
        internal readonly int p2StoreIndex;

        /// <summary>
        /// Initializes a new instance of the <see cref="Board"/> class.
        /// </summary>
        /// <param name="pits">Array represents the board, must have length = 14 </param>
        /// <exception cref="ArgumentException">Throws if pits length not equal 14 </exception>
        internal Board(int[] pits)
        {
            if (pits.Length != BOARD_SIZE)
            {
                throw new ArgumentException(String.Format("Length of {0} must be {1}", nameof(pits), BOARD_SIZE));
            }
            Pits = pits;
            this.p1StoreIndex = pits.Length / 2 - 1;
            this.p2StoreIndex = pits.Length - 1;
        }

        /// <summary>
        /// Get board side by pit index
        /// </summary>
        /// <param name="index">Pit index must be within 0 - 13</param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException">Throws if index < 0 or index > 13</exception>
        internal BoardSide GetSideByIndex(int index)
        {
            if (index < 0 || index >= Pits.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            if (index > p1StoreIndex)
            {
                return BoardSide.Top;
            }

            return BoardSide.Bottom;

        }
        /// <summary>
        /// Get index of the player store by board side
        /// </summary>
        /// <param name="boardSide"><see cref="BoardSide"/></param>
        /// <returns>Index of the store</returns>
        internal int GetStoreIndex(BoardSide boardSide)
        {
            return boardSide == BoardSide.Bottom ? p1StoreIndex : p2StoreIndex;
        }

        /// <summary>
        /// Get the opposit pit index
        /// </summary>
        /// <param name="index">Pit index</param>
        /// <returns>Pit index</returns>
        internal int GetOppositIndex(int index)
        {
            return Pits.Length - index - 2;
        }
        /// <summary>
        /// Checks if provided index is player store
        /// </summary>
        /// <param name="index">Pit index must be within 0 - 13</param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException">Throws if index < 0 or index > 13</exception>
        internal bool IsStore(int index)
        {
            if (index < 0 || index >= Pits.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            return index == p1StoreIndex || index == p2StoreIndex;
        }

        internal int ToAbsolutePitIndex(BoardSide boardSide, int relativePitIndex)
        {
            if (boardSide == BoardSide.Bottom)
            {
                return relativePitIndex;
            }

            return relativePitIndex + Board.BOARD_SIZE / 2;
        }

    }
}
