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

namespace StudentTeacher
{
    /// <summary>
    /// Interaction logic for AllTeachers.xaml
    /// </summary>
    public partial class AllTeachers : Window
    {
        public AllTeachers()
        {
            InitializeComponent();
            this.DataContext = this;
            gridTeachers.ItemsSource = StudentTeacherFunctions.teachers.Values;
        }
    }
}
