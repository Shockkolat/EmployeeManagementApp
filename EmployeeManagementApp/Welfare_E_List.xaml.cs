using Microsoft.Maui.Controls;

namespace EmployeeManagementApp.Models
{
    public class WithdrawalItem
    {
        public string Code { get; set; } = string.Empty;           // เช่น TRV-2569-00110
        public string Title { get; set; } = string.Empty;          // เช่น ปฏิบัติงานนอกสถานที่
        public string Description { get; set; } = string.Empty;    // เช่น สถานที่ปฏิบัติงาน โรงแรมการ์เด้นเชียงใหม่
        public string ItemsCountText { get; set; } = string.Empty; // เช่น 3 รายการเบิก
        public string FilesCountText { get; set; } = string.Empty; // เช่น 4 ไฟล์แนบ
        public string DateText { get; set; } = string.Empty;       // เช่น 07/07/2569(12.34)
        public string AmountText { get; set; } = string.Empty;     // เช่น 2,500 บาท
        public string StatusText { get; set; } = "รออนุมัติ";
    }
}

namespace EmployeeManagementApp
{
    public partial class Welfare_E_List : ContentPage
    {
        public Welfare_E_List()
        {
            InitializeComponent();
        }
    }
}
