using System;
using System.Collections.Generic;
using System.Linq;
using Employees.Models;
using Xunit;

namespace Tests
{
    public class EmployeeServiceTests
    {
        private readonly EmployeeService _employeeService = new EmployeeService();

        [Fact]
        public void CalculateOverlapWithNodaTime_OverlappingPeriods_ReturnsCorrectOverlap()
        {
            // Arrange
            DateTime start1 = new DateTime(2023, 01, 01);
            DateTime end1 = new DateTime(2023, 01, 10);
            DateTime start2 = new DateTime(2023, 01, 05);
            DateTime end2 = new DateTime(2023, 01, 15);

            // Act
            var overlap = _employeeService.CalculateOverlapWithNodaTime(start1, end1, start2, end2);

            // Assert
            Assert.Equal(6, overlap); // Overlap is from Jan 5 to Jan 10 inclusive
        }

        [Fact]
        public void CalculateOverlapWithNodaTime_NonOverlappingPeriods_ReturnsZero()
        {
            // Arrange
            DateTime start1 = new DateTime(2023, 01, 01);
            DateTime end1 = new DateTime(2023, 01, 10);
            DateTime start2 = new DateTime(2023, 01, 11);
            DateTime end2 = new DateTime(2023, 01, 20);

            // Act
            var overlap = _employeeService.CalculateOverlapWithNodaTime(start1, end1, start2, end2);

            // Assert
            Assert.Equal(0, overlap); // No overlap
        }

        [Fact]
        public void FindCommonProjects_NoCommonProjectsFound_ReturnsEmptyList()
        {
            // Arrange
            var projects = new List<EmployeeProject>
            {
                new EmployeeProject { EmpID = 143, ProjectID = 10, DateFrom = new DateTime(2009, 01, 01), DateTo = new DateTime(2011, 04, 27) },
                new EmployeeProject { EmpID = 218, ProjectID = 12, DateFrom = new DateTime(2013, 11, 01), DateTo = new DateTime(2014, 01, 05) }
            };

            // Act
            var commonProjects = _employeeService.FindCommonProjects(projects);

            // Assert
            Assert.Empty(commonProjects);
        }
    }
}
