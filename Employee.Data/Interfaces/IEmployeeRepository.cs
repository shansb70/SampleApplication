using Employee.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Employee.Data.Interfaces
{
    public interface IEmployeeRepository
    {
        public string CreateEmployeeDetails(EmployeeData employeeData);
   

    }
}
