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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StudentTeacher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private void Button_AddStudent(object sender, MouseButtonEventArgs e)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                AddStudent addStudent = new AddStudent();
                addStudent.Show();
            }));
        }

        private void Button_CheckEditStudent(object sender, MouseButtonEventArgs e)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                ViewOrChangeStudent viewStudent = new ViewOrChangeStudent();
                viewStudent.Show();
            }));
        }

        private void Button_AddTeacher(object sender, MouseButtonEventArgs e)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                AddTeacher addTeacher = new AddTeacher();
                addTeacher.Show();
            }));       
        }

        private void Button_CheckEditTeacher(object sender, MouseButtonEventArgs e)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                ViewOrChangeTeacher viewTeacher = new ViewOrChangeTeacher();
                viewTeacher.Show();
            }));  
        }

        private void Button_ShowStudents(object sender, MouseButtonEventArgs e)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                AllStudents allStudents = new AllStudents();
                allStudents.Show();
            }));            
        }

        private void Button_ShowTeachers(object sender, MouseButtonEventArgs e)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                AllTeachers allTeachers = new AllTeachers();
                allTeachers.Show();
            })); 
        }
    }
}
