using EmployeeManagementApp.Models;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;

namespace EmployeeManagementApp.Services
{
    public class EmployeeService
    {
        private readonly HttpClient _httpClient;

        private readonly string _baseUrl = DeviceInfo.Platform == DevicePlatform.Android
            ? "http://10.0.2.2:5264/api/employees"
            : "http://localhost:5264/api/employees";

        public EmployeeService()
        {
            _httpClient = new HttpClient();
        }

        // Get Employees
        public async Task<List<Employee>> GetEmployeesAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<List<Employee>>(_baseUrl);
            return response ?? new List<Employee>();
        }

        // Get Department
        public async Task<List<Department>> GetDepartmentsAsync()
        {
            var deptUrl = DeviceInfo.Platform == DevicePlatform.Android
                ? "http://10.0.2.2:5264/api/departments"
                : "http://localhost:5264/api/departments";

            var response = await _httpClient.GetFromJsonAsync<List<Department>>(deptUrl);
            return response ?? new List<Department>();
        }

        // UploadPhoto
        public async Task<string?> UploadPhotoAsync(FileResult file)
        {
            using var stream = await file.OpenReadAsync();
            using var content = new MultipartFormDataContent();
            content.Add(new StreamContent(stream), "file", file.FileName);

            var response = await _httpClient.PostAsync($"{_baseUrl}/upload", content);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<UploadResponse>();
                return result?.PhotoUrl;
            }
            return null;
        }

        // Add
        public async Task<bool> AddEmployeeAsync(Employee employee)
        {
            var response = await _httpClient.PostAsJsonAsync(_baseUrl, employee);
            return response.IsSuccessStatusCode;
        }

        // Delete
        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_baseUrl}/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateEmployeeAsync(Employee employee)
        {
            var url = DeviceInfo.Platform == DevicePlatform.Android
                ? $"http://10.0.2.2:5264/api/employees/{employee.Employee_ID}"
                : $"http://localhost:5264/api/employees/{employee.Employee_ID}";

            var response = await _httpClient.PutAsJsonAsync(url, employee);
            return response.IsSuccessStatusCode;
        }
    }

    public class UploadResponse
    {
        public string PhotoUrl { get; set; } = string.Empty;
    }
}
