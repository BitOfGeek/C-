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
    /// Interaction logic for AllStudents.xaml
    /// </summary>
    public partial class AllStudents : Window
    {
        public AllStudents()
        {
            InitializeComponent();
            this.DataContext = this;

            gridStudents.ItemsSource = StudentTeacherFunctions.students.Values;
            
        }
    }
}
