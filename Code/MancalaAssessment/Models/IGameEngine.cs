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
    public interface IGameEngine
    {
        /// <summary>
        /// Makes turn for active player
        /// </summary>
        /// <param name="game"><see cref="IGame"/></param>
        /// <param name="pitIndex">Selected pit, must be valid pit index <see cref="IGame.GetValidPitIndexes"/></param>
        /// <returns></returns>
        IGame Turn(IGame game, int pitIndex);

        /// <summary>
        /// Starts new Game
        /// </summary>
        /// <returns></returns>
        IGame NewGame();
    }
}
