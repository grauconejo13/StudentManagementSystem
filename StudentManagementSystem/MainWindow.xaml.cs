using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;
using MongoDB.Bson;
using MongoDB.Driver;


namespace StudentManagementSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly StudentManager _dbManager;
        private HashSet<int> existingStudentIds = new HashSet<int>();
        //   public List<Student> StudentList { get; set; }
        private readonly StudentManager studentManager = new();
        private ObservableCollection<Student> students = new();
        //   private IEnumerable studentList;

        public MainWindow()
        {
            InitializeComponent();
            _dbManager = new StudentManager();
            LoadStudentData();
            InitializeDataGridValues();
            DataContext = new();
        }

        //To Display to Datagrid
        private void InitializeDataGridValues()
        {
            // Create a collection of student data.
            students = new ObservableCollection<Student>();

            // Load the initial list of products from the XML file.
            foreach (Student student in _dbManager.GetAllStudents())
            {
                students.Add(student);
            }

            // Set the DataGrid's ItemsSource property to the collection of student data
            viewStudentData.ItemsSource = students;

        }

        //To MongoDB 
        public ObservableCollection<Student> GetAllStudents()
        {
            // Return the list of students
            return students;
        }

        private void LoadStudentData()
        {
            if (_dbManager.IsConnectionValid())
            {
                List<Student> studentList = _dbManager.GetAllStudents();
                viewStudentData.ItemsSource = studentList;

            }
            else
            {
                DisplayMessage("Database connection failed.");
            }
        }

        public void AddStudent(Student student)
        {
            // Add the product to the list of products
            students.Add(student);

            // Save the product to the XML file
            studentManager.InsertStudent(student);
        }

        private void Insert_Click(object sender, RoutedEventArgs e)
        { 
            try
            {
                if (viewStudentData.SelectedItem != null)
                { 
                    //DisplayMessage("Cannot insert when an item is selected. Please clear the selection or use the Update/Delete button.");
                    MessageBox.Show("Cannot insert when an item is selected. Please clear the selection or use the Update/Delete button.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);                   
                    return;
                }

                string mongoId = ObjectId.GenerateNewId().ToString();
                string studentIdString = txtStudentId.Text.Trim();
                string name = txtName.Text.Trim();
                string address = txtAddress.Text.Trim();
                string program = txtProgram.Text.Trim();
                //note: Need to address the formatting of the date how it appears in the datagrid. The DateTime can't be update? The xaml have the format for MM/dd/yyyy
                DateTime birthDate = dpBirthDate.SelectedDate ?? DateTime.Now;
                decimal totalFees = decimal.Parse(txtTotalFees.Text.Trim());

                // Validate other fields are not empty
                if (string.IsNullOrEmpty(studentIdString) || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(address) || string.IsNullOrEmpty(program))
                {
                    //DisplayMessage("All student information fields are required.");
                    MessageBox.Show("All student information fields are required.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                int studentId;                     
              
                // Validate integer ID
                try
                {
                    studentId = int.Parse(studentIdString);
                    //price = double.Parse(priceString);
                }
                catch (Exception)
                {
                    MessageBox.Show("Invalid number. Please fix student ID.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Validate product ID is not negative
                if (studentId < 0)
                {
                    MessageBox.Show("Student ID cannot be negative.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                
                //ValidateInput();*/
                // Inside the Insert_Click method
                if (int.TryParse(studentIdString, out studentId))
                {
                    if (existingStudentIds.Contains(studentId))
                    {
                        DisplayMessage("A student with the same ID already exists.");
                        return;
                    }
                    // If validation passes, add the student ID to the HashSet
                    existingStudentIds.Add(studentId);
                }
                Student student = new Student("default_id", studentId, name, address, program, birthDate, totalFees);
                
                // Add the product to the listbox
                students.Add(student);

                // Save the product to the XML file
                studentManager.InsertStudent(student);

                DisplayMessage("Student information inserted successfully.");

                viewStudentData.Items.Refresh();
                //viewStudentData.ItemsSource = students;

                // Clear the textboxes
                txtStudentId.Text = "";
                txtName.Text = "";
                txtAddress.Text = "";
                txtProgram.Text = "";
                dpBirthDate.Text = "";
                txtTotalFees.Text = "";
                
            }catch(Exception)
            {
                MessageBox.Show("Please select a student data to insert.", "Selection Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            if (viewStudentData.SelectedItem is Student selectedStudent)
            {
                var updateQuestion = MessageBox.Show("Are you sure you want to update this student data?", "Update Student", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (updateQuestion == MessageBoxResult.Yes)
                {
                    //MongoDBId = ObjectId.GenerateNewId().ToString(),
                     string studentIdString = txtStudentId.Text;
                     _= txtName.Text;
                     _= txtAddress.Text;
                     _= txtProgram.Text;
                     _= dpBirthDate.SelectedDate ?? DateTime.Now;
                     string totalFeesString = txtTotalFees.Text.Trim();

                     if (!string.IsNullOrEmpty(txtStudentId.Text) && ValidateInput()) { }

                    int studentId;
                    decimal totalFees;

                    // Validate integer ID
                    if (!int.TryParse(studentIdString, out studentId) || !decimal.TryParse(totalFeesString, out totalFees))
                    {
                        MessageBox.Show("Invalid input for student ID or total fees amount.", "Update Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    selectedStudent.Name = txtName.Text.Trim();
                    selectedStudent.Address = txtAddress.Text.Trim();
                    selectedStudent.Program = txtProgram.Text.Trim();

                    ValidateInput();
                    //UpdateStudentsCollection(student); // Update the student in the ObservableCollection
                    studentManager.UpdateStudent(selectedStudent, students);
                    
                    
                    // Clear the textboxes
                    txtStudentId.Text = "";
                    txtName.Text = "";
                    txtAddress.Text = "";
                    txtProgram.Text = "";
                    dpBirthDate.SelectedDate = null;
                    txtTotalFees.Text = "";

                    viewStudentData.SelectedItem = null;

                    DisplayMessage("Student information updated successfully.");
                    txtStudentId.IsEnabled = true;
                }               
            } 
            else
            {
                MessageBox.Show("Please select a student data to update.", "Update Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (viewStudentData.SelectedItem is Student selectedStudent)
            {
                if (selectedStudent.MongoDBId != null)
                {
                    var deleteQuestion = MessageBox.Show("Are you sure you want to delete student information?", "Delete Student", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (deleteQuestion == MessageBoxResult.Yes)
                    {
                        studentManager.DeleteStudent(selectedStudent.MongoDBId);
                        students.Remove(selectedStudent);
                        viewStudentData.Items.Refresh();
                                              
                        // Clear the textboxes
                        txtStudentId.Text = "";
                        txtName.Text = "";
                        txtAddress.Text = "";
                        txtProgram.Text = "";
                        dpBirthDate.SelectedDate = null;
                        txtTotalFees.Text = "";
                       
                        DisplayMessage("Student information deleted successfully.");
                    }                   
                }
            }
            else
            {
                MessageBox.Show("Please select a student information to delete.", "Delete Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }   

        private void viewStudentData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (viewStudentData.SelectedItem is Student selectedStudent)
            {
                txtStudentId.IsEnabled = false;
                // Populate TextBoxes with selected student's data
                txtStudentId.Text = selectedStudent.StudentId.ToString();
                txtName.Text = selectedStudent.Name;
                txtAddress.Text = selectedStudent.Address;
                txtProgram.Text = selectedStudent.Program;
                dpBirthDate.SelectedDate = selectedStudent.BirthDate;
                txtTotalFees.Text = selectedStudent.TotalFees.ToString();
            }
        }
        public void UpdateStudentsCollection(Student updatedStudent)
        {
            var existingStudent = students.FirstOrDefault(s => s.StudentId == updatedStudent.StudentId);
            if (existingStudent != null)
            {
                existingStudent.StudentId = updatedStudent.StudentId;
                existingStudent.Name = updatedStudent.Name;
                existingStudent.Address = updatedStudent.Address;
                existingStudent.Program = updatedStudent.Program;
                existingStudent.BirthDate = updatedStudent.BirthDate;
                existingStudent.TotalFees = updatedStudent.TotalFees;
            }
        }     
        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                DisplayMessage("Name is required.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtAddress.Text))
            {
                DisplayMessage("Address is required.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtProgram.Text))
            {
                DisplayMessage("Program is required.");
                return false;
            }

            if (!decimal.TryParse(txtTotalFees.Text, out _))
            {
                DisplayMessage("Invalid Total Fees. Please enter a valid number.");
                return false;
            }

            if (dpBirthDate.SelectedDate == null)
            {
                DisplayMessage("Birthdate is required.");
                return false;
            }

            // If all validations pass, return true
            return true;
        }

        private void DisplayMessage(string message)
        {
            txtMessage.Text = message;
        }
    }
}

