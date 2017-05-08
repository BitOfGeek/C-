using Microsoft.Practices.Prism.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ColumnarTransposition.ViewModel
{
    public class ColumnarTranspositionViewModel : INotifyPropertyChanged
    {
        #region Declarations

        private string textToEncrypt;
        private string encryptedText;
        private string textToDecrypt;
        private string decryptedText;
        private char padChar = '_';
        private string key;
        public DelegateCommand<object> encryptCommand;
        public DelegateCommand<object> decryprtCommand;

        #endregion

        #region Constructor

        public ColumnarTranspositionViewModel()
        {
        }

        #endregion

        #region Properties

        public string TextToEncrypt
        {
            get
            {
                return textToEncrypt;
            }

            set
            {
                if (textToEncrypt == value) return;

                textToEncrypt = value;
                NotifyPropertyChanged("TextToEncrypt");
            }
        }

        public string EncryptedText
        {
            get
            {
                return encryptedText;
            }

            set
            {
                if (encryptedText == value) return;

                encryptedText = value;
                NotifyPropertyChanged("EncryptedText");
            }
        }

        public string TextToDecrypt
        {
            get
            {
                return textToDecrypt;
            }

            set
            {
                if (textToDecrypt == value) return;

                textToDecrypt = value;
                NotifyPropertyChanged("TextToDecrypt");
            }
        }

        public string DecryptedText
        {
            get
            {
                return decryptedText;
            }

            set
            {
                if (decryptedText == value) return;

                decryptedText = value;
                NotifyPropertyChanged("DecryptedText");
            }
        }

        #endregion

        #region Commands

        public DelegateCommand<object> EncryptCommand
        {
            get
            {
                if (encryptCommand == null)
                {
                    encryptCommand = new DelegateCommand<object>(Encrypt);
                }

                return encryptCommand;
            }
        }

        public DelegateCommand<object> DecryptCommand
        {
            get
            {
                if (decryprtCommand == null)
                {
                    decryprtCommand = new DelegateCommand<object>(Decrypt);
                }

                return decryprtCommand;
            }
        }

        #endregion

        #region Methods

        private int[] GetIndexes(string key)
        {
            int keyLength = key.Length;
            int[] indexes = new int[keyLength];
            List<KeyValuePair<int, char>> sortedKey = new List<KeyValuePair<int, char>>();

            for (int i = 0; i < keyLength; ++i)
            {
                sortedKey.Add(new KeyValuePair<int, char>(i, key[i]));
            }

            sortedKey.Sort(delegate (KeyValuePair<int, char> pair1, KeyValuePair<int, char> pair2)
            {
                return pair1.Value.CompareTo(pair2.Value);
            });

            for (int i = 0; i < keyLength; ++i)
            {
                indexes[sortedKey[i].Key] = i;
            }

            return indexes;
        }

        private void GenerateNewKey()
        {
            string allowedChars = "абвгдежзийклмнопрстуфхцчшщъьюяabcdefghijklmnopqrstuvwxyz0123456789._-:";
            Random rng = new Random();
            char[] chars = new char[6];
            int setLength = allowedChars.Length;

            for (int i = 0; i < 6; ++i)
            {
                chars[i] = allowedChars[rng.Next(setLength)];
            }


            key = new string(chars);
        }

        private void Encrypt(object o)
        {
            GenerateNewKey();

            TextToEncrypt = TextToEncrypt.Replace(" ", "_");

            TextToEncrypt = (TextToEncrypt.Length % key.Length == 0)
                ? TextToEncrypt : TextToEncrypt.PadRight(TextToEncrypt.Length
                    - (TextToEncrypt.Length % key.Length) + key.Length, padChar);

            StringBuilder output = new StringBuilder();
            int totalChars = TextToEncrypt.Length;
            int totalColumns = key.Length;
            int totalRows = (int)Math.Ceiling((double)totalChars / totalColumns);
            char[,] rowChars = new char[totalRows, totalColumns];
            char[,] colChars = new char[totalColumns, totalRows];
            char[,] sortedColChars = new char[totalColumns, totalRows];
            int currentRow, currentColumn;
            int[] shiftIndexes = GetIndexes(key);

            for (int i = 0; i < totalChars; ++i)
            {
                currentRow = i / totalColumns;
                currentColumn = i % totalColumns;
                rowChars[currentRow, currentColumn] = textToEncrypt[i];
            }

            for (int i = 0; i < totalRows; ++i)
                for (int j = 0; j < totalColumns; ++j)
                    colChars[j, i] = rowChars[i, j];

            for (int i = 0; i < totalColumns; ++i)
                for (int j = 0; j < totalRows; ++j)
                    sortedColChars[shiftIndexes[i], j] = colChars[i, j];

            for (int i = 0; i < totalChars; ++i)
            {
                currentRow = i / totalRows;
                currentColumn = i % totalRows;
                output.Append(sortedColChars[currentRow, currentColumn]);
            }

            EncryptedText = output.ToString();
        }

        private void Decrypt(object o)
        {
            StringBuilder output = new StringBuilder();
            int totalChars = TextToDecrypt.Length;
            int totalColumns = (int)Math.Ceiling((double)totalChars / key.Length);
            int totalRows = key.Length;
            char[,] rowChars = new char[totalRows, totalColumns];
            char[,] colChars = new char[totalColumns, totalRows];
            char[,] unsortedColChars = new char[totalColumns, totalRows];
            int currentRow, currentColumn, i, j;
            int[] shiftIndexes = GetIndexes(key);

            for (i = 0; i < totalChars; ++i)
            {
                currentRow = i / totalColumns;
                currentColumn = i % totalColumns;
                rowChars[currentRow, currentColumn] = TextToDecrypt[i];
            }

            for (i = 0; i < totalRows; ++i)
                for (j = 0; j < totalColumns; ++j)
                    colChars[j, i] = rowChars[i, j];

            for (i = 0; i < totalColumns; ++i)
                for (j = 0; j < totalRows; ++j)
                    unsortedColChars[i, j] = colChars[i, shiftIndexes[j]];

            for (i = 0; i < totalChars; ++i)
            {
                currentRow = i / totalRows;
                currentColumn = i % totalRows;
                output.Append(unsortedColChars[currentRow, currentColumn]);
            }

            DecryptedText = output.ToString();
        }

        #endregion

        #region NotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }
}
