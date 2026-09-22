using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace EmployeeManagementApp.Models;

// รายการย่อยในหมวดหมู่
public class CategorySubItem
{
    public string Title { get; set; } = string.Empty;
    public string ValueText { get; set; } = string.Empty; // เช่น "1,200 บาท" หรือ "3 รายการ"
    public double Progress { get; set; } = 0.5; // ค่า 0.0 - 1.0 สำหรับ Progress Bar
}

// หมวดหมู่หลัก (เช่น งานและการปฏิบัติงาน)
public class MainCategoryGroup
{
    public string CategoryTitle { get; set; } = string.Empty;
    public string SubTitleText { get; set; } = string.Empty;
    public string TotalValueText { get; set; } = string.Empty;
    public string Icon { get; set; } = "💼";
    public string HeaderColor { get; set; } = "#2196F3"; // สีหลักของหมวดหมู่
    public List<CategorySubItem> SubItems { get; set; } = new();
}

// รายการสวัสดิการ (Tab 2)
public class BenefitItem
{
    public string Title { get; set; } = string.Empty;
    public string AmountText { get; set; } = string.Empty;
    public string CountText { get; set; } = string.Empty;
    public string Icon { get; set; } = "🚗";
    public double Progress { get; set; } = 0.0; // ค่า 0.0 จะซ่อน Progress Bar
    public bool HasProgress => Progress > 0;
}

public partial class SummaryViewModel : ObservableObject
{
    // ตัวเลือก Dropdown ด้านบน
    [ObservableProperty] private List<string> _filterOptions = new() { "ทั้งหมด", "งานและการปฏิบัติงาน", "ทรัพยากรและสวัสดิการ" };
    [ObservableProperty] private string _selectedFilter = "ทั้งหมด";

    // ข้อมูลรายสวัสดิการ
    [ObservableProperty] private ObservableCollection<BenefitItem> _benefits = new();

    private void LoadBenefitData()
    {
        Benefits = new ObservableCollection<BenefitItem>
        {
            new BenefitItem { Title = "พาหนะ,ขนส่ง", AmountText = "1,200 บาท", CountText = "2 รายการ", Icon = "🚌", Progress = 0.0 },
            new BenefitItem { Title = "ค่าทางด่วน", AmountText = "200 บาท", CountText = "1 รายการ", Icon = "🛣️", Progress = 0.0 },
            new BenefitItem { Title = "ค่าน้ำมัน", AmountText = "1,200 บาท", CountText = "2 รายการ", Icon = "⛽", Progress = 0.5 },
            new BenefitItem { Title = "ค่าที่จอดรถ", AmountText = "250 บาท", CountText = "1 รายการ", Icon = "🅿️", Progress = 0.0 },
            new BenefitItem { Title = "ค่าเช่ารถ", AmountText = "7,500 บาท", CountText = "3 รายการ", Icon = "🚗", Progress = 0.0 },
            new BenefitItem { Title = "ค่าเสื่อม", AmountText = "2,500 บาท", CountText = "2 รายการ", Icon = "🧮", Progress = 0.0 },
            new BenefitItem { Title = "ค่าอาหาร", AmountText = "2,500 บาท", CountText = "2 รายการ", Icon = "🍱", Progress = 0.5 },
            new BenefitItem { Title = "ค่าเบี้ยเลี้ยง", AmountText = "2,500 บาท", CountText = "2 รายการ", Icon = "👛", Progress = 0.6 },
            new BenefitItem { Title = "ค่าที่พัก", AmountText = "2,500 บาท", CountText = "2 รายการ", Icon = "🏨", Progress = 0.5 },
            new BenefitItem { Title = "ในประเทศ", AmountText = "2,500 บาท", CountText = "2 รายการ", Icon = "🇹🇭", Progress = 0.2 }
        };
    }
}