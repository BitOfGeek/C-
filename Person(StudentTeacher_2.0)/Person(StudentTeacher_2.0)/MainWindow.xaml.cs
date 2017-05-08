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

namespace Person_StudentTeacher_2._0_
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

        private bool teacherMode = false;
        private int key = 0;
        private Dictionary<int, Student> students = new Dictionary<int, Student>();
        private Dictionary<int, Teacher> teachers = new Dictionary<int, Teacher>();

        private void TeacherMode(object sender, MouseButtonEventArgs e)
        {
            if (sender == TeacherButton)
            {
                teacherMode = true;
                EnterName.Text = "";
                EnterFacNum.Text = "";
                EnterEGN.Text = "";
                EnterFaculty.Text = "";
                EnterSpecialty.Text = "";
                ComboBoxGender.SelectedItem = null;
                AllStudents.SelectedValue = null;
                AllStudents.Visibility = Visibility.Hidden;
            }
            else
            {
                teacherMode = false;
                EnterName.Text = "";
                EnterFacNum.Text = "";
                EnterEGN.Text = "";
                EnterFaculty.Text = "";
                EnterSpecialty.Text = "";
                ComboBoxGender.SelectedItem = null;
                AllTeachers.SelectedValue = null;
                AllTeachers.Visibility = Visibility.Hidden;
            }

            if (teacherMode == true)
            {
                FacNumLabel.Visibility = Visibility.Collapsed;
                EnterFacNum.Visibility = Visibility.Collapsed;
                FacultyLabel.Content = "Department";
                SpecialtyLabel.Content = "Subject";
            }
            else
            {
                FacNumLabel.Visibility = Visibility.Visible;
                EnterFacNum.Visibility = Visibility.Visible;
                FacultyLabel.Content = "Faculty";
                SpecialtyLabel.Content = "Specialty";
            }

        }
        private void Search(object sender)
        {
            if (!int.TryParse(SearchBox.Text, out key))
            {
                SearchBox.Text = "Must contain only numbers";
            }
            //else if (Search.Text.Length != 10)
            //{
            //    Search.Text = "Must have lenght of 10";
            //}
            else
            {
                
                if (!teacherMode)
                {
                    if (!students.ContainsKey(key))
                    {
                        SearchBox.Text = "No such key exists";
                    }
                    else
                    {
                        Student student = students[key];
                        EnterName.Text = student.Name;
                        EnterFacNum.Text = Convert.ToString(student.FacNum);
                        EnterEGN.Text = Convert.ToString(student.EGN);
                        EnterFaculty.Text = student.Faculty;
                        EnterSpecialty.Text = student.Specialty;
                        ComboBoxGender.SelectedValue = student.Gender;
                    }
                }
                else
                {
                    if (!teachers.ContainsKey(key))
                    {
                        SearchBox.Text = "No such key exists";
                    }
                    else
                    {
                        Teacher teacher = teachers[key];
                        EnterName.Text = teacher.Name;
                        EnterEGN.Text = Convert.ToString(teacher.EGN);
                        EnterFaculty.Text = teacher.Department;
                        EnterSpecialty.Text = teacher.Subject;
                        ComboBoxGender.SelectedValue = teacher.Gender;
                    }
                }

            }
            
        }

        private void SearchButton(object sender, MouseButtonEventArgs e)
        {
            Search(sender);
        }
        private void KeyEnterCheck(object sender, KeyEventArgs e)
            {
            if (e.Key == Key.Enter)
            {
                Search(sender);
            }
        }

        private void SaveButton(object sender, MouseButtonEventArgs e)
        {
            int egn;
            if (!int.TryParse(EnterEGN.Text, out egn))
            {
                EnterEGN.Text = "Must contain only numbers";
            }
            else
            {
                if (key == 0)
                {
                    if (!teacherMode)
                    {
                        AddNewStudent();
                        //MessageBox.Show(ComboBoxGender.SelectedValue.ToString());
                    }
                    else
                    {
                        AddNewTeacher();
                    }
                }
                else
                {
                    if (!teacherMode)
                    {
                        ChangeExisrtingStudent();
                    }
                    else
                    {
                        ChangeExistingTeacher();
                    }
                }
                EnterName.Text = "";
                EnterFacNum.Text = "";
                EnterEGN.Text = "";
                EnterFaculty.Text = "";
                EnterSpecialty.Text = "";
                SearchBox.Text = "";
                ComboBoxGender.SelectedItem = null;
                //AllStudents.SelectedValue = null;
                //AllTeachers.SelectedValue = null;
            }  
            
        }

        private void DeleteButton(object sender, MouseButtonEventArgs e)
        {
            if (key != 0)
            {
                if (!teacherMode)
                {
                    students.Remove(key);
                    AllStudents.Items.Remove(key);
                }
                else
                {
                    teachers.Remove(key);
                    AllTeachers.Items.Remove(key);
                }
            }
            EnterName.Text = "";
            EnterFacNum.Text = "";
            EnterEGN.Text = "";
            EnterFaculty.Text = "";
            EnterSpecialty.Text = "";
            ComboBoxGender.SelectedItem = null;
        }

        private void AddNewStudent()
        {
            string gender =  ComboBoxGender.SelectedValue.ToString();
            students.Add(int.Parse(EnterEGN.Text), new Student(EnterName.Text, int.Parse(EnterFacNum.Text), int.Parse(EnterEGN.Text),
                     EnterFaculty.Text, EnterSpecialty.Text, gender));
            AllStudents.Items.Add(int.Parse(EnterEGN.Text));
        }

        private void AddNewTeacher()
        {
            teachers.Add(int.Parse(EnterEGN.Text), new Teacher(EnterName.Text, int.Parse(EnterEGN.Text),
                    EnterFaculty.Text, EnterSpecialty.Text, ComboBoxGender.SelectedValue.ToString()));
            AllTeachers.Items.Add(int.Parse(EnterEGN.Text));
        }

        private void ChangeExisrtingStudent()
        {
            if (key != int.Parse(EnterEGN.Text))
            {

                students.Remove(key);
                AllStudents.Items.Remove(key);
                AddNewStudent();

            }
            else
            {

                students[key] = new Student(EnterName.Text, int.Parse(EnterFacNum.Text), int.Parse(EnterEGN.Text),
                    EnterFaculty.Text, EnterSpecialty.Text, ComboBoxGender.SelectedValue.ToString());
            }             
        }

        private void ChangeExistingTeacher()
        {
            if (key != int.Parse(EnterEGN.Text))
            {
                teachers.Remove(key);
                AllTeachers.Items.Remove(key);
                AddNewTeacher();
            } else
            {
                teachers[key] = new Teacher(EnterName.Text, int.Parse(EnterEGN.Text),
                    EnterFaculty.Text, EnterSpecialty.Text, ComboBoxGender.SelectedValue.ToString());
            }
        }

        private void ViewAllButton(object sender, MouseButtonEventArgs e)
        {
            if (!teacherMode)
            {
                AllStudents.Visibility = Visibility.Visible;
            }
            else
            {
                AllTeachers.Visibility = Visibility.Visible;
            }
        }

        private void ComboBox_SelectedStudent(object sender, SelectionChangedEventArgs e)
        {
            key = int.Parse(AllStudents.SelectedItem.ToString());
            Student student = students[key];
            EnterName.Text = student.Name;
            EnterFacNum.Text = Convert.ToString(student.FacNum);
            EnterEGN.Text = Convert.ToString(student.EGN);
            EnterFaculty.Text = student.Faculty;
            EnterSpecialty.Text = student.Specialty;
            ComboBoxGender.SelectedValue = student.Gender;
        }

        private void ComboBox_SelectedTeacher(object sender, SelectionChangedEventArgs e)
        {
            key = int.Parse(AllTeachers.SelectedItem.ToString());
            Teacher teacher = teachers[key];
            EnterName.Text = teacher.Name;
            EnterEGN.Text = Convert.ToString(teacher.EGN);
            EnterFaculty.Text = teacher.Department;
            EnterSpecialty.Text = teacher.Subject;
            ComboBoxGender.SelectedValue = teacher.Gender;
        }

        //private void ViewAllButton(object sender, MouseButtonEventArgs e)
        //{
        //    Dispatcher.BeginInvoke(new Action(() =>
        //    {
        //        if (!teacherMode)
        //        {
        //            ViewAllStudents viewStudents = new ViewAllStudents(students);
        //        }
                
        //    }));
        //}
    }
}
