using BullsAndCows.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BullsAndCows.Models
{
    public class Score : NotificationService
    {
        private int bulls;
        private int cows;

        public Score(int bulls, int cows)
        {
            this.bulls = bulls;
            this.cows = cows;
        }

        public Score()
        {
        }

        public int Bulls
        {
            get
            {
                return bulls;
            }

            set
            {
                if (bulls == value) return;

                bulls = value;
                NotifyPropertyChanged("Bulls");
            }
        }

        public int Cows
        {
            get
            {
                return cows;
            }

            set
            {
                if (cows == value) return;

                cows = value;
                NotifyPropertyChanged("Cows");
            }
        }
    }
}
