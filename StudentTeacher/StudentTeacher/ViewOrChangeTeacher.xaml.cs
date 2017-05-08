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
    /// Interaction logic for ViewOrChangeTeacher.xaml
    /// </summary>
    public partial class ViewOrChangeTeacher : Window
    {
        public int key;
        public Teacher tempTeacher = null;
        public ViewOrChangeTeacher()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private void Button_Search(object sender, MouseButtonEventArgs e)
        {
            key = int.Parse(Search.Text);
            if (StudentTeacherFunctions.KeyExistsTeacher(key))
            {
                tempTeacher = StudentTeacherFunctions.ViewTeachers(key);
                EnterName.Text = tempTeacher.teacherName;
                String egn = Convert.ToString(tempTeacher.teacherEGN);
                EnterEGN.Text = egn;
                EnterDepartment.Text = tempTeacher.department;
                if (tempTeacher.gender == "male")
                {
                    Male.IsChecked = true;
                }
                else if (tempTeacher.gender == "female")
                {
                    Female.IsChecked = true;
                }
            }
            else
            {
                Search.Text = "No such key exists";
                EnterName.Text = "";
                EnterEGN.Text = "";
                EnterDepartment.Text = "";
            }
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

            if (int.Parse(EnterEGN.Text) == key)
            {
                tempTeacher = new Teacher(EnterName.Text, int.Parse(EnterEGN.Text), EnterDepartment.Text, gender);
                StudentTeacherFunctions.ChangeTeacher(key, tempTeacher);
                this.Close();
            }
            else if (!StudentTeacherFunctions.CheckInput(EnterEGN.Text))
            {
                EnterEGN.Text = "Invalid input: must contain 10 numbers";
            }
            else
            {
                StudentTeacherFunctions.RemoveTeacher(key);
                StudentTeacherFunctions.AddNewTeacher(EnterName.Text, int.Parse(EnterEGN.Text), EnterDepartment.Text, gender);
                this.Close();
            }           
        }

        private void Button_Delete(object sender, MouseButtonEventArgs e)
        {
            StudentTeacherFunctions.RemoveTeacher(key);
            this.Close();
        }
    }
}
