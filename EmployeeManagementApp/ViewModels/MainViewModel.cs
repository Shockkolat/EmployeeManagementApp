using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EmployeeManagementApp.Models;
using EmployeeManagementApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace EmployeeManagementApp.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly EmployeeService _service;

        [ObservableProperty]
        private ObservableCollection<Employee> _employees = new();
        [ObservableProperty] 
        private ObservableCollection<Department> _departments = new();
        [ObservableProperty] 
        private Department? _selectedDepartment;

        [ObservableProperty]
        private bool _isBusy;

        [ObservableProperty] private string _firstName = string.Empty;
        [ObservableProperty] private string _lastName = string.Empty;
        [ObservableProperty] private int _departmentId = 1;
        [ObservableProperty] private string _gender = "Male";
        [ObservableProperty] private DateTime _dateOfBirth = new DateTime(1995, 1, 1);
        [ObservableProperty] private DateTime _dateJoined = DateTime.Now;
        [ObservableProperty] private string _address = string.Empty;
        [ObservableProperty] private string? _photoUrl;
        [ObservableProperty] private FileResult? _selectedFile;
        [ObservableProperty] private string? _originalPhotoPath;

        [ObservableProperty] private bool _isEditing;
        [ObservableProperty] private int _editingEmployeeId;
        public string SaveButtonText => _isEditing ? "อัปเดตข้อมูล" : "บันทึกข้อมูล";
        public string FormTitleText => _isEditing ? "แก้ไขข้อมูลพนักงาน" : "เพิ่มพนักงานใหม่";

        public MainViewModel()
        {
            _service = new EmployeeService();
            _ = InitDataAsync();
        }
        private async Task InitDataAsync()
        {
            IsBusy = true;
            await LoadDepartmentsAsync();
            await LoadEmployeesAsync();
            IsBusy = false;
        }

        public async Task LoadDepartmentsAsync()
        {
            try
            {
                var depts = await _service.GetDepartmentsAsync();
                Departments.Clear();
                foreach (var d in depts)
                {
                    Departments.Add(d);
                }

                if (Departments.Count > 0)
                    SelectedDepartment = Departments[0];
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading departments: {ex.Message}");
            }
        }

        [RelayCommand]
        public async Task LoadEmployeesAsync()
        {
            try
            {
                IsBusy = true;
                var data = await _service.GetEmployeesAsync();
                Employees.Clear();
                foreach (var emp in data)
                {
                    Employees.Add(emp);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading employees: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        public async Task PickImageAsync()
        {
            var result = await FilePicker.Default.PickAsync(new PickOptions
            {
                PickerTitle = "เลือกรูปภาพพนักงาน",
                FileTypes = FilePickerFileType.Images
            });

            if (result != null)
            {
                SelectedFile = result;
                PhotoUrl = result.FullPath;
            }
        }

        [RelayCommand]
        public async Task SaveEmployeeAsync()
        {
            if (string.IsNullOrWhiteSpace(FirstName) || string.IsNullOrWhiteSpace(LastName))
            {
                await Shell.Current.DisplayAlertAsync("เตือน", "กรุณากรอกชื่อและนามสกุล", "ตกลง");
                return;
            }

            if (SelectedDepartment == null)
            {
                await Shell.Current.DisplayAlertAsync("เตือน", "กรุณาเลือกแผนก", "ตกลง");
                return;
            }

            IsBusy = true;

            string? photoToSave;

            if (SelectedFile != null)
            {
                photoToSave = await _service.UploadPhotoAsync(SelectedFile);
            }
            else if (IsEditing)
            {
                photoToSave = OriginalPhotoPath;
            }
            else
            {
                photoToSave = null;
            }

            var empData = new Employee
            {
                Employee_ID = IsEditing ? EditingEmployeeId : 0,
                Department_ID = SelectedDepartment.Department_ID,
                Employee_First_Name = FirstName,
                Employee_Last_Name = LastName,
                Gender = Gender,
                Date_of_Birth = DateOfBirth,
                Date_Joined = DateJoined,
                Employee_Address = Address,
                Photo = photoToSave
            };

            bool success;
            if (IsEditing)
            {
                success = await _service.UpdateEmployeeAsync(empData);
            }
            else
            {
                success = await _service.AddEmployeeAsync(empData);
            }

            if (success)
            {
                ResetForm();
                await LoadEmployeesAsync();
            }
            else
            {
                await Shell.Current.DisplayAlertAsync("ข้อผิดพลาด", "ไม่สามารถบันทึกข้อมูลได้", "ตกลง");
            }
            IsBusy = false;
        }

        [RelayCommand]
        public void EditEmployee(Employee employee)
        {
            if (employee == null) return;

            IsEditing = true;
            EditingEmployeeId = employee.Employee_ID;

            OriginalPhotoPath = employee.Photo;

            FirstName = employee.Employee_First_Name;
            LastName = employee.Employee_Last_Name;
            Gender = string.IsNullOrEmpty(employee.Gender) ? "Male" : employee.Gender;
            DateOfBirth = employee.Date_of_Birth ?? new DateTime(1995, 1, 1);
            DateJoined = employee.Date_Joined ?? DateTime.Now;
            Address = employee.Employee_Address ?? string.Empty;
            PhotoUrl = employee.FullPhotoUrl;
            SelectedFile = null;

            SelectedDepartment = Departments.FirstOrDefault(d => d.Department_ID == employee.Department_ID);

            OnPropertyChanged(nameof(SaveButtonText));
            OnPropertyChanged(nameof(FormTitleText));
        }

        [RelayCommand]
        public void CancelEdit()
        {
            ResetForm();
        }

        private void ResetForm()
        {
            IsEditing = false;
            EditingEmployeeId = 0;
            FirstName = string.Empty;
            LastName = string.Empty;
            Address = string.Empty;
            SelectedFile = null;
            PhotoUrl = null;
            if (Departments.Count > 0) SelectedDepartment = Departments[0];

            OnPropertyChanged(nameof(SaveButtonText));
            OnPropertyChanged(nameof(FormTitleText));
        }

        [RelayCommand]
        public async Task DeleteEmployeeAsync(Employee employee)
        {
            if (employee == null) return;
            var confirm = await Shell.Current.DisplayAlert("ยืนยัน", $"ต้องการลบ {employee.FullName} ใช่หรือไม่?", "ใช่", "ยกเลิก");
            if (confirm)
            {
                await _service.DeleteEmployeeAsync(employee.Employee_ID);
                await LoadEmployeesAsync();
            }
        }
    }
}
