using BullsAndCows.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BullsAndCows.Views
{
    /// <summary>
    /// Interaction logic for BullsAndCowsGameView.xaml
    /// </summary>
    public partial class BullsAndCowsGameView : Window
    {
        public BullsAndCowsGameView()
        {
            InitializeComponent();
            DataContext = new BullsAndCowsViewModel();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            ChangeNumberLenghtView view = new ChangeNumberLenghtView();
            view.DataContext = this.DataContext;
            view.Show();
        }
    }
}
