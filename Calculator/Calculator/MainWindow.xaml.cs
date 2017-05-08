using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Button pressedButton;
        private String buttonContent;
        private bool calculatetResult = false;
        private bool error = false;
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private void Button_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            pressedButton = sender as Button;
            buttonContent = Convert.ToString(pressedButton.Content);

            if (calculatetResult == true)
            {
                DispayInput.Text = "";
                calculatetResult = false;
            }

            if (String.Equals(DispayInput.Text, "0"))
            {
                if (String.Equals(buttonContent, "."))
                {
                    DispayInput.Text = DispayInput.Text + buttonContent;
                }
                else
                {
                    DispayInput.Text = "";
                    DispayInput.Text = DispayInput.Text + buttonContent;
                }
            }
            else
            {
                DispayInput.Text = DispayInput.Text + buttonContent;
            }
        }

        private void Button_Delete(object sender, MouseButtonEventArgs e)
        {
            pressedButton = sender as Button;
            buttonContent = Convert.ToString(pressedButton.Content);

            if (String.Equals(buttonContent, "CE"))
            {
                DispayInput.Text = "0";
            }

            if (String.Equals(buttonContent, "Del"))
            {
                DispayInput.Text = DispayInput.Text.Remove(DispayInput.Text.Length - 1);
            }
        }

        private void DispayInput_MouseClick(object sender, MouseButtonEventArgs e)
        {
            if (String.Equals(DispayInput.Text, "0"))
            {
                DispayInput.Text = DispayInput.Text.Remove(DispayInput.Text.Length - 1);
            }

            if (calculatetResult == true)
            {
                DispayInput.Text = "";
                calculatetResult = false;
            }
        } 

        private void Button_Equals(object sender, MouseButtonEventArgs e)
        {
           Calculate(DispayInput.Text);
        }

        private void DisplayInput_KeyCheck(object sender, KeyEventArgs e)
        {
            if (calculatetResult == true)
            {
                DispayInput.Text = "";
                calculatetResult = false;
            }
            if (e.Key == Key.Enter | e.Key==Key.OemPlus)
            {
                Calculate(DispayInput.Text);
            }

            //TODO: fix invalid input

            //if (!System.Text.RegularExpressions.Regex.IsMatch(e.Key.ToString(), @"[^a-zA-z]"))
            //{
            //    error = true;
            //}

            //if (error == true)
            //{
            //    DispayInput.Text = " is incorect input";
            //    error = false;
            //    calculatetResult = true;
            //}
        }

        private void Calculate(String line)
        {
            double equation = Evaluate(line);
            string result = Convert.ToString(equation);
            DispayInput.Text = result;
            calculatetResult = true;
        }
        public static double Evaluate(string expression)
        {
            //TODO: change data struct
            DataTable table = new DataTable();
            table.Columns.Add("expression", string.Empty.GetType(), expression);
            DataRow row = table.NewRow();
            table.Rows.Add(row);
            return double.Parse((string)row["expression"]);
        }   
    }
}
