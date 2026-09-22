using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EmployeeManagementApp.Models;
using System.Collections.ObjectModel;

namespace EmployeeManagementApp.ViewModels;

public partial class Welfare_E_List_ViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<WithdrawalItem> _items = new();

    public Welfare_E_List_ViewModel()
    {
        LoadDummyData();
    }

    private void LoadDummyData()
    {
        _items = new ObservableCollection<WithdrawalItem>
        {
            new WithdrawalItem
            {
                Code = "TRV-2569-00110",
                Title = "ปฏิบัติงานนอกสถานที่",
                Description = "สถานที่ปฏิบัติงาน โรงแรมการ์เด้นเชียงใหม่",
                ItemsCountText = "3 รายการเบิก",
                FilesCountText = "4 ไฟล์แนบ",
                DateText = "07/07/2569(12.34)",
                AmountText = "2,500 บาท"
            },
            new WithdrawalItem
            {
                Code = "TRV-2569-00110",
                Title = "ปฏิบัติงานนอกสถานที่",
                Description = "สถานที่ปฏิบัติงาน สิงคโปร์",
                ItemsCountText = "5 รายการเบิก",
                FilesCountText = "4 ไฟล์แนบ",
                DateText = "07/07/2569(12.34)",
                AmountText = "5,520 บาท"
            },
            new WithdrawalItem
            {
                Code = "SSO-2569-00111",
                Title = "สิทธิประกันสังคม",
                Description = "ค่ารักษาพยาบาล",
                ItemsCountText = "1 รายการเบิก",
                FilesCountText = "3 ไฟล์แนบ",
                DateText = "01/07/2569(12.34)",
                AmountText = ""
            },
            new WithdrawalItem
            {
                Code = "UNF-2569-00111",
                Title = "เบิกชุดสำหรับการปฏิบัติงาน",
                Description = "เบิกเสื้อ",
                ItemsCountText = "1 รายการเบิก",
                FilesCountText = "",
                DateText = "01/07/2569(12.34)",
                AmountText = ""
            }
        };
    }

    [RelayCommand]
    private async Task GoToSummaryAsync()
    {
        // คำสั่งเปลี่ยนไปยังหน้า SummaryPage
        await Shell.Current.GoToAsync(nameof(SummaryPage));
    }
}