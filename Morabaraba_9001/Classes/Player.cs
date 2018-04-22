using System;
using System.Collections.Generic;
using System.Text;
using Morabaraba_9001.Interfaces;

namespace Morabaraba_9001.Classes
{
    public class Player : IPlayer
    {
        public Color Color { get; private set; }
        private ICowBox Box;

        public Player(Color color, ICowBox box)
        {
            Color = color;
            Box = box;
        }

        public bool Place(int position, IBoard board, IReferee referee, Phase currPhase)
        {
            if (referee.CanPlace(Color, position, currPhase)){
                ICow cow = Box.TakeCow(Color);
                board.Place(cow, position);
                return true;
            }
            else return false;
        }

        public bool Kill(int position, IBoard board, IReferee referee)
        {
            if (referee.CanKill(Color, position)){
                board.Kill(position);
                return true;
            }
            else return false;
        }

        public bool Select(int position, IBoard board, IReferee referee)
        {
            if (referee.CanSelect(Color, board, position)){
                return true;
            }
            else return false;
        }

        public bool Move(int Destination, int secondPosition, IBoard board, IReferee referee, Phase currPhase)
        {
            if (referee.CanMove(Color, Destination, secondPosition, currPhase)){
                board.Move(Destination, secondPosition);
                return true;
            }
            else return false;
        }

        public bool MoveFlying(int Destination, int secondPosition, IBoard board, IReferee referee, Phase currPhase)
        {
            if (referee.CanFlyTo(Color, secondPosition, currPhase))
            {
                board.Move(Destination, secondPosition);
                return true;
            }
            else return false;
        }

        /// <summary>
        /// Checks the board to see if the 
        /// </summary>
        /// <param name="board"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        public bool IsLooser(IBoard board, Color color)
        {
            return (board.canAnyCowMove(color) == false || board.numPlayerCowsOnBoard(color) <= 2) && Box.IsEmpty();     // Checks to see if a player has any moves left
        }
    }
}
