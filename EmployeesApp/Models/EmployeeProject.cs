using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employees.Models
{
    public class EmployeeProject
    {
        public int EmpID { get; set; }
        public int ProjectID { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
    }

}
