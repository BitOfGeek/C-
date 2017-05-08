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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Beverages.View
{
    /// <summary>
    /// Interaction logic for ProductEditView.xaml
    /// </summary>
    public partial class ProductView : Window
    {
        public ProductView()
        {
            this.Language = XmlLanguage.GetLanguage("bg-BG");
            InitializeComponent();
            this.DataContext = new Beverages.ViewModel.ProductViewModel();
        }
    }
}
