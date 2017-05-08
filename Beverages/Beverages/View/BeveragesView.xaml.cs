using Beverages.Mediator;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Beverages.View
{
    /// <summary>
    /// Interaction logic for BeveragesView.xaml
    /// </summary>
    public partial class BeveragesView : Window
    {
        public BeveragesView()
        {
            this.Language = XmlLanguage.GetLanguage("bg-BG");
            InitializeComponent();
            this.DataContext = new Beverages.ViewModel.BeveragesViewModel();

            if (MessageMediator.CloseAction == null)
            {
                MessageMediator.CloseAction = new Action(this.Close);
            }
                

        }

    }
}
