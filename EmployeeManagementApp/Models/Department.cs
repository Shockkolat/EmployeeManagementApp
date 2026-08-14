using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManagementApp.Models
{
    public class Department
    {
        public int Department_ID { get; set; }
        public string Department_Name { get; set; } = string.Empty;
        public string? Department_Address { get; set; }
    }
}
