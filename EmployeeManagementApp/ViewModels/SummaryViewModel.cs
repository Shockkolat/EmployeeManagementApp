using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EmployeeManagementApp.Models;

namespace EmployeeManagementApp.ViewModels;

public partial class SummaryViewModel : ObservableObject
{
    // ควบคุมการสลับแท็บ
    [ObservableProperty] private bool _isCategoryTabSelected = true;
    [ObservableProperty] private bool _isBenefitTabSelected = false; // 👈 เพิ่มส่วนนี้

    [ObservableProperty] private string _selectedYear = "2569";

    // สำหรับ Filter Dropdown แท็บรายสวัสดิการ
    [ObservableProperty] private List<string> _filterOptions = new() { "ทั้งหมด", "งานและการปฏิบัติงาน", "ทรัพยากรและสวัสดิการ" };
    [ObservableProperty] private string _selectedFilter = "ทั้งหมด";

    // Collections
    [ObservableProperty] private ObservableCollection<MainCategoryGroup> _categories = new();
    [ObservableProperty] private ObservableCollection<BenefitItem> _benefits = new();

    public SummaryViewModel()
    {
        LoadSummaryData();
    }

    [RelayCommand]
    private void SelectTab(string tabName)
    {
        IsCategoryTabSelected = tabName == "Category";
        IsBenefitTabSelected = tabName == "Benefit"; // 👈 อัปเดตสถานะแท็บสวัสดิการ
    }

    public class CategorySubItem
    {
        public string Title { get; set; } = string.Empty;
        public string ValueText { get; set; } = string.Empty;
        public double Progress { get; set; } = 0.5;
        public string Icon { get; set; } = "💼";
    }

    public class MainCategoryGroup
    {
        public string CategoryTitle { get; set; } = string.Empty;
        public string SubTitleText { get; set; } = string.Empty;
        public string MainValueText { get; set; } = string.Empty;
        public string UnitText { get; set; } = string.Empty;
        public string Icon { get; set; } = "💼";

        // โทนสีการ์ดตามหมวดหมู่
        public Color CardBgColor { get; set; } = Colors.White;
        public Color HeaderIconBgColor { get; set; } = Colors.Blue;
        public Color AccentColor { get; set; } = Colors.Blue;
        public Color BadgeBgColor { get; set; } = Colors.LightBlue;

        public List<CategorySubItem> SubItems { get; set; } = new();
    }

    private void LoadSummaryData()
    {
        // ข้อมูลสำหรับ Tab 1: ตามหมวดหมู่
        Categories = new ObservableCollection<MainCategoryGroup>
        {
            // หมวดที่ 1: โทนสีฟ้า (เงินบาท)
            new MainCategoryGroup
            {
                CategoryTitle = "งานและการปฏิบัติงาน",
                SubTitleText = "ใช้ไปแล้วทั้งหมด",
                MainValueText = "5,900",
                UnitText = "บาท",
                Icon = "💼",
                CardBgColor = Color.FromArgb("#F4F8FF"),
                HeaderIconBgColor = Color.FromArgb("#3B82F6"),
                AccentColor = Color.FromArgb("#2563EB"),
                BadgeBgColor = Color.FromArgb("#E0F2FE"),
                SubItems = new List<CategorySubItem>
                {
                    new CategorySubItem { Title = "ปฏิบัติงานนอกสถานที่", ValueText = "1,200 บาท", Progress = 0.6, Icon = "💼" },
                    new CategorySubItem { Title = "ค่าใช้จ่ายการเดินทาง", ValueText = "3,900 บาท", Progress = 0.5, Icon = "🚗" },
                    new CategorySubItem { Title = "การรับรองลูกค้า", ValueText = "800 บาท", Progress = 0.4, Icon = "👥" }
                }
            },
            // หมวดที่ 2: โทนสีส้ม (จำนวนรายการ)
            new MainCategoryGroup
            {
                CategoryTitle = "ทรัพยากรและสวัสดิการในการทำงาน",
                SubTitleText = "ใช้ไปแล้วทั้งหมด",
                MainValueText = "12",
                UnitText = "รายการ",
                Icon = "🎁",
                CardBgColor = Color.FromArgb("#FFF8F5"),
                HeaderIconBgColor = Color.FromArgb("#F97316"),
                AccentColor = Color.FromArgb("#EA580C"),
                BadgeBgColor = Color.FromArgb("#FFEDD5"),
                SubItems = new List<CategorySubItem>
                {
                    new CategorySubItem { Title = "สนับสนุนการทำงาน", ValueText = "3 รายการ", Progress = 0.4, Icon = "🖥️" },
                    new CategorySubItem { Title = "เบิกอุปกรณ์เพื่อการปฏิบัติงาน", ValueText = "4 รายการ", Progress = 0.5, Icon = "💻" },
                    new CategorySubItem { Title = "เบิกชุดเพื่อการปฏิบัติงาน", ValueText = "3 รายการ", Progress = 0.4, Icon = "👔" },
                    new CategorySubItem { Title = "ชุดของขวัญเทศกาล", ValueText = "2 รายการ", Progress = 0.4, Icon = "🎁" }
                }
            }
        };

        // ข้อมูลสำหรับ Tab 2: รายสวัสดิการ
        Benefits = new ObservableCollection<BenefitItem>
        {
            new BenefitItem { Title = "พาหนะ,ขนส่ง", AmountText = "1,200 บาท", CountText = "2 รายการ", Icon = "🚌", Progress = 0.0 },
            new BenefitItem { Title = "ค่าทางด่วน", AmountText = "200 บาท", CountText = "1 รายการ", Icon = "🛣️", Progress = 0.0 },
            new BenefitItem { Title = "ค่าน้ำมัน", AmountText = "1,200 บาท", CountText = "2 รายการ", Icon = "⛽", Progress = 0.6 },
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