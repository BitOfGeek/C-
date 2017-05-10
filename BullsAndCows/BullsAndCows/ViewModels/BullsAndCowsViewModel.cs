using BullsAndCows.Models;
using BullsAndCows.Services;
using Microsoft.Practices.Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace BullsAndCows.ViewModels
{
    public class BullsAndCowsViewModel : NotificationService
    {
        #region Declarations
        private bool isPlayersTurn;
        private bool isComputersTurn;
        private int computersNumber;
        private int playersGuess;
        private int computersGuess;
        private int turn;
        private int lenght;
        private Score computerCurentResult;
        private Random range;
        private List<int> availableNumbers;
        private List<ScoreHistory> playersScore;
        private List<ScoreHistory> computersScore;
        private DelegateCommand<object> guessCommand;
        private DelegateCommand<object> submitCommand;
        private DelegateCommand<object> newGameCommand;
        private DelegateCommand<object> changeNumberLenghtCommand;
        private DelegateCommand<object> closeCommand;
        #endregion

        public BullsAndCowsViewModel()
        {
            Lenght = 4;
            isPlayersTurn = true;
            range = new Random();
            availableNumbers = GenerateNumbers();
            computersNumber = availableNumbers[range.Next(0, availableNumbers.Count)];
            turn = 0;        
        }

        #region Properies
        public bool IsPlayersTurn
        {
            get
            {
                return isPlayersTurn;
            }

            set
            {
                if (isPlayersTurn == value) return;

                isPlayersTurn = value;
                NotifyPropertyChanged("IsPlayersTurn");
            }
        }

        public bool IsComputersTurn
        {
            get
            {
                return isComputersTurn;
            }

            set
            {
                if (isComputersTurn == value) return;

                isComputersTurn = value;

                if(isComputersTurn == true)
                {
                   ComputersTurn();
                }
                
                NotifyPropertyChanged("IsComputersTurn");
            }
        }

        public int Lenght
        {
            get
            {
                return lenght;
            }

            set
            {
                if (lenght == value) return;

                lenght = value;
                NotifyPropertyChanged("Lenght");
            }
        }

        public int PlayersGuess
        {
            get
            {
                return playersGuess;
            }

            set
            {
                if (playersGuess == value) return;

                playersGuess = value;
                NotifyPropertyChanged("PlayersGuess");
            }
        }

        public int ComputersGuess
        {
            get
            {
                return computersGuess;
            }

            set
            {
                if (computersGuess == value) return;

                computersGuess = value;
                NotifyPropertyChanged("ComputersGuess");
            }
        }

        public Score ComputerCurentResult
        {
            get
            {
                if(computerCurentResult == null)
                {
                    computerCurentResult = new Score();
                }

                return computerCurentResult;
            }

            set
            {
                if (computerCurentResult == value) return;

                computerCurentResult = value;
                NotifyPropertyChanged("ComputerCurentResult");
            }
        }

        public List<ScoreHistory> PlayersScore
        {
            get
            {
                if (playersScore == null)
                {
                    playersScore = new List<ScoreHistory>();
                }
                return playersScore;
            }

            set
            {
                if (playersScore == value) return;

                playersScore = value;
                NotifyPropertyChanged("PlayersScore");
            }
        }

        public List<ScoreHistory> ComputersScore
        {
            get
            {
                if (computersScore == null)
                {
                    computersScore = new List<ScoreHistory>();
                }
                return computersScore;
            }

            set
            {
                if (computersScore == value) return;

                computersScore = value;
                NotifyPropertyChanged("ComputersScore");
            }
        }
        #endregion

        #region Commands
        public DelegateCommand<object> GuessCommand
        {
            get
            {
                if (guessCommand == null)
                {
                    guessCommand = new DelegateCommand<object>(PlayersTurn);
                }

                return guessCommand;
            }
        }

        public DelegateCommand<object> SubmitCommand
        {
            get
            {
                if (submitCommand == null)
                {
                    submitCommand = new DelegateCommand<object>(ComputersResult);
                }

                return submitCommand;
            }
        }

        public DelegateCommand<object> NewGameCommand
        {
            get
            {
                if (newGameCommand == null)
                {
                    newGameCommand = new DelegateCommand<object>(NewGame);
                }

                return newGameCommand;
            }
        }

        public DelegateCommand<object> ChangeNumberLenghtCommand
        {
            get
            {
                if (changeNumberLenghtCommand == null)
                {
                    changeNumberLenghtCommand = new DelegateCommand<object>(ChangeNumberLenght);
                }

                return changeNumberLenghtCommand;
            }
        }

        public DelegateCommand<object> CloseCommand
        {
            get
            {
                if (closeCommand == null)
                {
                    closeCommand = new DelegateCommand<object>(CloseApplication);
                }

                return closeCommand;
            }
        }
        #endregion

        #region Methods

        private void ComputersResult(object obj)
        {
            Score tempScore = new Score(ComputerCurentResult.Bulls, ComputerCurentResult.Cows);
            List<ScoreHistory> tempHistory = ComputersScore;
            tempHistory.Add(new ScoreHistory(turn, ComputersGuess.ToString(), tempScore));
            ComputersScore = null;
            ComputersScore = tempHistory;

            if (ComputerCurentResult.Bulls == lenght)
            {
                MessageBox.Show("Sorry! Computer Won!", "GameOver", MessageBoxButton.OK);
                return;
            }

            ClearArray(ComputerCurentResult, ComputersGuess);
            IsPlayersTurn = true;
            IsComputersTurn = false;
        }

        public void PlayersTurn(object obj)
        {
            turn++;
            Score temp = GetScore(PlayersGuess.ToString(), computersNumber.ToString());
            
            List<ScoreHistory> tempHistory = PlayersScore;
            tempHistory.Add(new ScoreHistory(turn, PlayersGuess.ToString(), temp));
            PlayersScore = null;
            PlayersScore = tempHistory;

            if(temp.Bulls == lenght)
            {
                MessageBox.Show("Congratulations! You Won!", "GameOver", MessageBoxButton.OK);
                return;
            }

            IsPlayersTurn = false;
            IsComputersTurn = true;
        }

        public void ComputersTurn()
        {
            try
            {
                ComputersGuess = availableNumbers[range.Next(0, availableNumbers.Count)];
            }
            catch(Exception ex)
            {
                MessageBox.Show("Something is not right! \nCheating is not ok", "Error", MessageBoxButton.OK);
            }
            
        }

        public Score GetScore(string guess, string number)
        {
            int cowsCount = 0;
            int bulsCount = 0;
            for (int i = 0; i < lenght; i++)
            {
                for (int j = 0; j < lenght; j++)
                {
                    if (guess[i] == number[j])
                    {
                        if (i == j) bulsCount++;
                        else cowsCount++;
                    }
                }
            }
            return new Score(bulsCount, cowsCount);
        }

        private void ClearArray(Score score, int guess)
        {
            List<int> numbers = new List<int>(availableNumbers);
            foreach (var item in numbers)
            {
                var newScore = GetScore(guess.ToString(), item.ToString());
                if (newScore.Bulls != score.Bulls || newScore.Cows != score.Cows)
                {
                    availableNumbers.Remove(item);
                }
            }
        }

        private List<int> GenerateNumbers()
        {
            List<int> result = new List<int>();

            if (Lenght == 3)
            {
                for (int i = 102; i <= 999; i++)
                {
                    bool[] usedNumbers = new bool[10];
                    bool isValidNumber = true;
                    int number = i;
                    for (int j = 0; j < lenght; j++)
                    {
                        if (usedNumbers[number % 10])
                        {
                            isValidNumber = false;
                            break;
                        }
                        else
                        {
                            usedNumbers[number % 10] = true;
                            number /= 10;
                        }
                    }
                    if (isValidNumber)
                    {
                        result.Add(i);
                    }
                }
            }

            if(Lenght == 4)
            {
                for (int i = 1023; i <= 9999; i++)
                {
                    bool[] usedNumbers = new bool[10];
                    bool isValidNumber = true;
                    int number = i;
                    for (int j = 0; j < lenght; j++)
                    {
                        if (usedNumbers[number % 10])
                        {
                            isValidNumber = false;
                            break;
                        }
                        else
                        {
                            usedNumbers[number % 10] = true;
                            number /= 10;
                        }
                    }
                    if (isValidNumber)
                    {
                        result.Add(i);
                    }
                }
            
            }

            if (Lenght == 5)
            {
                for (int i = 10234; i <= 99999; i++)
                {
                    bool[] usedNumbers = new bool[10];
                    bool isValidNumber = true;
                    int number = i;
                    for (int j = 0; j < lenght; j++)
                    {
                        if (usedNumbers[number % 10])
                        {
                            isValidNumber = false;
                            break;
                        }
                        else
                        {
                            usedNumbers[number % 10] = true;
                            number /= 10;
                        }
                    }
                    if (isValidNumber)
                    {
                        result.Add(i);
                    }
                }
            }

            return result;
        }

        private void NewGame(object o)
        {
            isPlayersTurn = true;
            range = new Random();
            availableNumbers = GenerateNumbers();
            computersNumber = availableNumbers[range.Next(0, availableNumbers.Count)];
            turn = 0;

            PlayersGuess = 0;
            ComputersGuess = 0;
            PlayersScore = null;
            ComputersScore = null;
            ComputerCurentResult = null;
        }

        private void ChangeNumberLenght(object o)
        {
            Lenght = int.Parse(o.ToString());
            Application.Current.Windows[2].Close();
            NewGame(null);
        }

        private void CloseApplication(object o)
        {
            Application.Current.Shutdown();
        }
        #endregion    
    }
}
