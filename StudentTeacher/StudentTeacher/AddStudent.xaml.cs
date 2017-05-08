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
using StudentTeacher;

namespace StudentTeacher
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class AddStudent : Window
    {
        public AddStudent()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private void Button_SaveNew(object sender, MouseButtonEventArgs e)
        {
            String gender = null;
            if (Male.IsChecked == true)
            {
                gender = "male";
            }
            else if (Female.IsChecked == true)
            {
                gender = "female";
            }

            if (!StudentTeacherFunctions.CheckInput(EnterFacNum.Text))
            {
                EnterFacNum.Text= "Invalid input: must contain 10 numbers";
            }
            else if(!StudentTeacherFunctions.CheckInput(EnterEGN.Text))
            {
                EnterEGN.Text = "Invalid input: must contain 10 numbers";
            }
            else if (StudentTeacherFunctions.KeyExistsStudent(int.Parse(EnterEGN.Text)))
            {
                EnterEGN.Text = "EGN Already exists";
            }
            else
            {
                StudentTeacherFunctions.AddNewStudent(EnterName.Text, int.Parse(EnterFacNum.Text), int.Parse(EnterEGN.Text),
                 EnterFaculty.Text, EnterSpecialty.Text, gender);
                this.Close();  
            }
        }
    }
}
