using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration;
using Employees.Models;
using NodaTime;

public class EmployeeService
{
    public List<EmployeeProject> LoadData(string filePath)
    {
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true,
            Delimiter = ","
        };

        using (var reader = new StreamReader(filePath))
        using (var csv = new CsvReader(reader, config))
        {
            csv.Context.RegisterClassMap<EmployeeProjectMap>();
            var records = csv.GetRecords<EmployeeProject>().ToList();
            return records;
        }
    }

    public int CalculateOverlapWithNodaTime(DateTime start1, DateTime end1, DateTime start2, DateTime end2)
    {
        var start1Local = LocalDate.FromDateTime(start1);
        var end1Local = LocalDate.FromDateTime(end1);
        var start2Local = LocalDate.FromDateTime(start2);
        var end2Local = LocalDate.FromDateTime(end2);

        return CalculateOverlapWithNodaTime(start1Local, end1Local, start2Local, end2Local);
    }

    private int CalculateOverlapWithNodaTime(LocalDate start1, LocalDate end1, LocalDate start2, LocalDate end2)
    {
        var latestStart = LocalDate.Max(start1, start2);
        var earliestEnd = LocalDate.Min(end1, end2);

        var overlap = Period.Between(latestStart, earliestEnd, PeriodUnits.Days).Days;

        return overlap >= 0 ? overlap + 1 : 0;
    }

    public List<CommonProject> FindCommonProjects(List<EmployeeProject> projects)
    {
        var commonProjects = new List<CommonProject>();

        for (int i = 0; i < projects.Count; i++)
        {
            for (int j = i + 1; j < projects.Count; j++)
            {
                var p1 = projects[i];
                var p2 = projects[j];

                if (p1.ProjectID == p2.ProjectID && p1.EmpID != p2.EmpID)
                {
                    var overlapDays = CalculateOverlapWithNodaTime(p1.DateFrom, p1.DateTo, p2.DateFrom, p2.DateTo);

                    if (overlapDays > 0)
                    {
                        commonProjects.Add(new CommonProject
                        {
                            EmpID1 = p1.EmpID,
                            EmpID2 = p2.EmpID,
                            ProjectID = p1.ProjectID,
                            DaysWorked = overlapDays
                        });
                    }
                }
            }
        }

        return commonProjects;
    }
}
