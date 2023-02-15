using MancalaAssessment.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MancalaAssessment.Models
{
    public interface IGame
    {
        /// <summary>
        /// Get indexes of pits which could be used for the current turn
        /// </summary>
        /// <returns>Array of indexes with maximum length 6</returns>
        int[] GetValidPitIndexes();
        /// <summary>
        /// Get store for provided board side
        /// </summary>
        /// <param name="boardSide"><see cref="BoardSide"/> </param>
        /// <returns>Amount of stones in the store</returns>
        int GetStore(BoardSide boardSide);
        /// <summary>
        /// Get pits for provided board side
        /// </summary>
        /// <param name="boardSide"><see cref="BoardSide"/></param>
        /// <returns>Array of pits with length equals 6</returns>
        int[] GetPits(BoardSide boardSide);
        int[] BoardState { get; }
        /// <summary>
        /// Retruns active player <see cref="Player"/> for the current turn
        /// </summary>
        Player ActivePlayer { get; }
        /// <summary>
        /// Retruns passive player <see cref="Player"/> for the current turn 
        /// </summary>
        Player PassivePlayer { get; }
        /// <summary>
        /// Retruns current game state<see cref="GameState"/>
        /// </summary>
        GameState State { get; }
    }
}
