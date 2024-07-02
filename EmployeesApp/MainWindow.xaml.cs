using Microsoft.Win32;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace EmployeesApp
{
    public partial class MainWindow : Window
    {
        private readonly EmployeeService _employeeService = new EmployeeService();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "CSV files (*.csv)|*.csv",
                Title = "Open CSV File"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                var filePath = openFileDialog.FileName;
                var projects = _employeeService.LoadData(filePath);

                // Debug: Log loaded projects
                foreach (var project in projects)
                {
                    MessageBox.Show($"Loaded Project: EmpID={project.EmpID}, ProjectID={project.ProjectID}, DateFrom={project.DateFrom}, DateTo={project.DateTo}");
                }

                var commonProjects = _employeeService.FindCommonProjects(projects);

                // Debug: Log common projects
                foreach (var project in commonProjects)
                {
                    MessageBox.Show($"Common Project: EmpID1={project.EmpID1}, EmpID2={project.EmpID2}, ProjectID={project.ProjectID}, DaysWorked={project.DaysWorked}");
                }

                ResultsGrid.ItemsSource = commonProjects;
            }
        }
    }

    public class CommonProject
    {
        public int EmpID1 { get; set; }
        public int EmpID2 { get; set; }
        public int ProjectID { get; set; }
        public int DaysWorked { get; set; }
    }
}
