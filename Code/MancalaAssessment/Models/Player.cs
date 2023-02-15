using MancalaAssessment.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MancalaAssessment.Models
{
    /// <summary>
    /// Represents the player
    /// </summary>
    public class Player
    {
        public readonly BoardSide boardSide;

        /// <summary>
        /// Initializes a new instance of the <see cref="Player"/> 
        /// </summary>
        /// <param name="boardSide">Player's side of the board</param>
        public Player(BoardSide boardSide)
        {
            this.boardSide = boardSide;
        }
    }
}
