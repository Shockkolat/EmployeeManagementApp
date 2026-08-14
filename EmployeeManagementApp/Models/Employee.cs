using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManagementApp.Models
{
    public class Employee
    {
        public int Employee_ID { get; set; }
        public int Department_ID { get; set; }
        public string Employee_First_Name { get; set; } = string.Empty;
        public string Employee_Last_Name { get; set; } = string.Empty;
        public string? Gender { get; set; }
        public DateTime? Date_of_Birth { get; set; }
        public DateTime? Date_Joined { get; set; }
        public string? Employee_Address { get; set; }
        public string? Photo { get; set; }

        public string FullName => $"{Employee_First_Name} {Employee_Last_Name}";

        public string FullPhotoUrl => !string.IsNullOrEmpty(Photo)
            ? $"http://10.0.2.2:5264{Photo}" // 10.0.2.2  localhost ให้ Android Emu
            : "dotnet_bot.png";
    }
}
