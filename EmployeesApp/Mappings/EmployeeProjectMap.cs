using CsvHelper.Configuration;
using Employees.Models;

public sealed class EmployeeProjectMap : ClassMap<EmployeeProject>
{
    public EmployeeProjectMap()
    {
        Map(m => m.EmpID).Name("EmpID");
        Map(m => m.ProjectID).Name("ProjectID");
        Map(m => m.DateFrom).Name("DateFrom").TypeConverter<DateConverter>();
        Map(m => m.DateTo).Name("DateTo").TypeConverter<DateConverter>();
    }
}
