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
    /// Interaction logic for ViewOrChangeStudent.xaml
    /// </summary>
    public partial class ViewOrChangeStudent : Window
    {
        public int key;
        public Student tempStudent = null;

        public ViewOrChangeStudent()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private void Button_Search(object sender, MouseButtonEventArgs e)
        {
            key = int.Parse(Search.Text);
            if (StudentTeacherFunctions.KeyExistsStudent(key))
            {
                tempStudent = StudentTeacherFunctions.ViewStudents(key);
                EnterName.Text = tempStudent.studentName;
                String facNum = Convert.ToString(tempStudent.facNum);
                EnterFacNum.Text = facNum;
                String egn = Convert.ToString(tempStudent.studentEGN);
                EnterEGN.Text = egn;
                EnterFaculty.Text = tempStudent.faculty;
                EnterSpecialty.Text = tempStudent.specialty;
                if (tempStudent.gender == "male")
                {
                    Male.IsChecked = true;
                }
                else if (tempStudent.gender == "female")
                {
                    Female.IsChecked = true;
                }
            }
            else
            {
                Search.Text = "No such key exists";
                EnterName.Text = "";                
                EnterFacNum.Text = "";
                EnterEGN.Text = "";
                EnterFaculty.Text = "";
                EnterSpecialty.Text = "";
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
                tempStudent = new Student(EnterName.Text, int.Parse(EnterFacNum.Text), int.Parse(EnterEGN.Text),
                  EnterFaculty.Text, EnterSpecialty.Text, gender);
                StudentTeacherFunctions.ChangeStudent(key, tempStudent);
                this.Close();
            } else if (!StudentTeacherFunctions.CheckInput(EnterEGN.Text))
            {
                EnterEGN.Text = "Invalid input: must contain 10 numbers";
            }
            else
            {
                StudentTeacherFunctions.RemoveStudent(key);
                StudentTeacherFunctions.AddNewStudent(EnterName.Text, int.Parse(EnterFacNum.Text), int.Parse(EnterEGN.Text),
                      EnterFaculty.Text, EnterSpecialty.Text, gender);
                this.Close();
            }           
        }

        private void Button_Delete(object sender, MouseButtonEventArgs e)
        {
            StudentTeacherFunctions.RemoveStudent(key);
            this.Close();
        }

    }
}
