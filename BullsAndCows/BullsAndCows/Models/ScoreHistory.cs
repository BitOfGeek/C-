using BullsAndCows.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BullsAndCows.Models
{
    public class ScoreHistory : NotificationService
    {
        #region Declarations
        private int number;
        private string guess;
        private Score score;
        #endregion

        #region Constructor
        public ScoreHistory(int paramNumber, string paramGuess, Score paramScore)
        {
            Number = paramNumber;
            Guess = paramGuess;
            Score = paramScore;
        }

        public ScoreHistory()
        {
        }
        #endregion

        #region Properties
        public int Number
        {
            get
            {
                return number;
            }

            set
            {
                if (number == value) return;

                number = value;
                NotifyPropertyChanged("Number");
            }
        }

        public string Guess
        {
            get
            {
                return guess;
            }

            set
            {
                if (guess == value) return;

                guess = value;
                NotifyPropertyChanged("Guess");
            }
        }

        public Score Score
        {
            get
            {
                return score;
            }

            set
            {
                if (score == value) return;

                score = value;
                NotifyPropertyChanged("Score");
            }
        }
        #endregion

    }
}
