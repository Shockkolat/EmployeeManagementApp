using System;
using System.Collections.Generic;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Shapes;

namespace EmployeeManagementApp
{
    public partial class SummaryPage : ContentPage
    {
        public SummaryPage()
        {
            InitializeComponent();

            // กำหนดรายการใน Picker ผ่าน C# เพื่อเลี่ยง Bug ของ XAML Compiler
            YearPicker.ItemsSource = new List<string> { "2569", "2568", "2567", "2566" };
            YearPicker.SelectedIndex = 0;

            WelfareFilterPicker.ItemsSource = new List<string> { "ทั้งหมด", "ตามหมวดหมู่" };
            WelfareFilterPicker.SelectedIndex = 0;

            RenderCategoryData();
            RenderWelfareData();
            RenderWelfareGroupCategoryData();
        }

        private void OnYearPickerChanged(object sender, EventArgs e)
        {
            if (YearPicker.SelectedIndex != -1)
            {
                string selectedYear = YearPicker.Items[YearPicker.SelectedIndex];
            }
        }

        private async void OnHeaderBackTapped(object sender, EventArgs e)
        {
            if (ViewSubCategoryDetail.IsVisible || ViewWelfareDetail.IsVisible)
            {
                BackToMainView();
            }
            else
            {
                if (Navigation.NavigationStack.Count > 1)
                {
                    await Navigation.PopAsync();
                }
                else
                {
                    await Shell.Current.GoToAsync("..");
                }
            }
        }

        protected override bool OnBackButtonPressed()
        {
            if (ViewSubCategoryDetail.IsVisible || ViewWelfareDetail.IsVisible)
            {
                BackToMainView();
                return true;
            }
            return base.OnBackButtonPressed();
        }

        private void BackToMainView()
        {
            TabSwitcherBorder.IsVisible = true;

            if (TabWelfareLabel.FontAttributes == FontAttributes.Bold)
            {
                ViewCategory.IsVisible = false;
                ViewWelfare.IsVisible = true;
            }
            else
            {
                ViewCategory.IsVisible = true;
                ViewWelfare.IsVisible = false;
            }

            ViewSubCategoryDetail.IsVisible = false;
            ViewWelfareDetail.IsVisible = false;
        }

        private void OnWelfareFilterChanged(object sender, EventArgs e)
        {
            if (WelfareFilterPicker.SelectedIndex == 1)
            {
                WelfareItemList.IsVisible = false;
                WelfareGroupCategoryList.IsVisible = true;
            }
            else
            {
                WelfareItemList.IsVisible = true;
                WelfareGroupCategoryList.IsVisible = false;
            }
        }

        private void OnCategoryTabTapped(object sender, EventArgs e)
        {
            TabCategoryBorder.BackgroundColor = Color.FromArgb("#2979FF");
            TabCategoryLabel.TextColor = Colors.White;
            TabCategoryLabel.FontAttributes = FontAttributes.Bold;

            TabWelfareBorder.BackgroundColor = Colors.Transparent;
            TabWelfareLabel.TextColor = Color.FromArgb("#5C6BC0");
            TabWelfareLabel.FontAttributes = FontAttributes.None;

            TabSwitcherBorder.IsVisible = true;
            ViewCategory.IsVisible = true;
            ViewWelfare.IsVisible = false;
            ViewSubCategoryDetail.IsVisible = false;
            ViewWelfareDetail.IsVisible = false;
        }

        private void OnWelfareTabTapped(object sender, EventArgs e)
        {
            TabWelfareBorder.BackgroundColor = Color.FromArgb("#2979FF");
            TabWelfareLabel.TextColor = Colors.White;
            TabWelfareLabel.FontAttributes = FontAttributes.Bold;

            TabCategoryBorder.BackgroundColor = Colors.Transparent;
            TabCategoryLabel.TextColor = Color.FromArgb("#5C6BC0");
            TabCategoryLabel.FontAttributes = FontAttributes.None;

            TabSwitcherBorder.IsVisible = true;
            ViewCategory.IsVisible = false;
            ViewWelfare.IsVisible = true;
            ViewSubCategoryDetail.IsVisible = false;
            ViewWelfareDetail.IsVisible = false;
        }

        private void RenderWelfareGroupCategoryData()
        {
            var groupCategories = new List<WelfareGroupCategoryItem>
            {
                new WelfareGroupCategoryItem { Icon = "💼", Title = "งานและการปฏิบัติงาน", BgColor = Color.FromArgb("#F0F5FF"), IconBgColor = Color.FromArgb("#2979FF") },
                new WelfareGroupCategoryItem { Icon = "🎁", Title = "ทรัพยากรและสวัสดิการในการทำงาน", BgColor = Color.FromArgb("#FFF8F0"), IconBgColor = Color.FromArgb("#FF6D00") },
                new WelfareGroupCategoryItem { Icon = "❇️", Title = "สุขภาพและสิทธิประโยชน์", BgColor = Color.FromArgb("#F1F8E9"), IconBgColor = Color.FromArgb("#4CAF50") }
            };

            WelfareGroupCategoryList.Children.Clear();
            foreach (var item in groupCategories)
            {
                var cardBorder = new Border
                {
                    BackgroundColor = item.BgColor,
                    StrokeShape = new RoundRectangle { CornerRadius = 24 },
                    StrokeThickness = 0,
                    Padding = new Thickness(12, 12)
                };

                var tapGesture = new TapGestureRecognizer();
                tapGesture.Tapped += (s, e) => OnWelfareGroupCategoryTapped(item);
                cardBorder.GestureRecognizers.Add(tapGesture);

                var grid = new Grid
                {
                    ColumnDefinitions = { new ColumnDefinition { Width = GridLength.Auto }, new ColumnDefinition { Width = GridLength.Star } },
                    ColumnSpacing = 14
                };

                var iconBorder = new Border
                {
                    BackgroundColor = item.IconBgColor,
                    StrokeShape = new RoundRectangle { CornerRadius = 20 },
                    WidthRequest = 40,
                    HeightRequest = 40,
                    StrokeThickness = 0,
                    Content = new Label { Text = item.Icon, FontSize = 18, HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center }
                };

                var titleLabel = new Label { Text = item.Title, TextColor = Color.FromArgb("#37474F"), FontAttributes = FontAttributes.Bold, FontSize = 14, VerticalOptions = LayoutOptions.Center };

                Grid.SetColumn(iconBorder, 0);
                Grid.SetColumn(titleLabel, 1);

                grid.Children.Add(iconBorder);
                grid.Children.Add(titleLabel);

                cardBorder.Content = grid;
                WelfareGroupCategoryList.Children.Add(cardBorder);
            }
        }

        private void OnWelfareGroupCategoryTapped(WelfareGroupCategoryItem item)
        {
            SubGroupTitleLabel.Text = item.Title;
            SubCategoryIconLabel.Text = item.Icon;
            SubCategoryTitleLabel.Text = item.Title;

            TabSwitcherBorder.IsVisible = false;
            ViewCategory.IsVisible = false;
            ViewWelfare.IsVisible = false;
            ViewSubCategoryDetail.IsVisible = true;
            ViewWelfareDetail.IsVisible = false;

            RenderSubCategoryDetailData();
        }

        private void OnCategoryItemTapped(CategorySummaryItem clickedItem, string groupTitle)
        {
            SubGroupTitleLabel.Text = groupTitle;
            SubCategoryIconLabel.Text = clickedItem.Icon;
            SubCategoryTitleLabel.Text = clickedItem.Title;

            TabSwitcherBorder.IsVisible = false;
            ViewCategory.IsVisible = false;
            ViewWelfare.IsVisible = false;
            ViewSubCategoryDetail.IsVisible = true;
            ViewWelfareDetail.IsVisible = false;

            RenderSubCategoryDetailData();
        }

        private void OnWelfareItemTapped(WelfareItemModel clickedItem)
        {
            WelfareDetailIconLabel.Text = clickedItem.Icon;
            WelfareDetailTitleLabel.Text = clickedItem.Title;
            WelfareDetailTotalAmountLabel.Text = "5,750 บาท";
            WelfareDetailTotalCountLabel.Text = "3 รายการ";

            TabSwitcherBorder.IsVisible = false;
            ViewCategory.IsVisible = false;
            ViewWelfare.IsVisible = false;
            ViewSubCategoryDetail.IsVisible = false;
            ViewWelfareDetail.IsVisible = true;

            RenderWelfareTransactionDetailData();
        }

        private void RenderWelfareTransactionDetailData()
        {
            var transactions = new List<WelfareTransactionModel>
            {
                new WelfareTransactionModel { DateText = "วันที่ 1/7/2569", Title = "รถยนต์(ตัวเอง)", RouteText = "บริษัทพาวเวอร์วิชั่น → สนามบินดอนเมือง", AmountText = "500.00 บาท" },
                new WelfareTransactionModel { DateText = "วันที่ 1/7/2569", Title = "เครื่องบิน", RouteText = "สนามบินดอนเมือง → สนามบินสิงคโปร์", AmountText = "5000.00 บาท" },
                new WelfareTransactionModel { DateText = "วันที่ 3/7/2569", Title = "แท็กซี่(สาธารณะ)", RouteText = "สนามบินดอนเมือง → บริษัทพาวเวอร์วิชั่น", AmountText = "250.00 บาท" }
            };

            WelfareDetailTransactionList.Children.Clear();
            foreach (var item in transactions)
            {
                var cardBorder = new Border
                {
                    BackgroundColor = Colors.White,
                    StrokeShape = new RoundRectangle { CornerRadius = 16 },
                    StrokeThickness = 1,
                    Stroke = Color.FromArgb("#E3F2FD"),
                    Padding = new Thickness(14, 12)
                };

                var grid = new Grid
                {
                    ColumnDefinitions = { new ColumnDefinition { Width = GridLength.Star }, new ColumnDefinition { Width = GridLength.Auto } },
                    RowDefinitions = { new RowDefinition { Height = GridLength.Auto }, new RowDefinition { Height = GridLength.Auto }, new RowDefinition { Height = GridLength.Auto } },
                    RowSpacing = 2
                };

                var dateLabel = new Label { Text = item.DateText, TextColor = Color.FromArgb("#9E9E9E"), FontSize = 12 };
                var amountLabel = new Label { Text = item.AmountText, TextColor = Color.FromArgb("#616161"), FontSize = 13, FontAttributes = FontAttributes.Bold, HorizontalOptions = LayoutOptions.End };
                var titleLabel = new Label { Text = item.Title, TextColor = Color.FromArgb("#212121"), FontSize = 14, FontAttributes = FontAttributes.Bold, Margin = new Thickness(0, 2, 0, 0) };
                var routeLabel = new Label { Text = item.RouteText, TextColor = Color.FromArgb("#757575"), FontSize = 11 };

                Grid.SetRow(dateLabel, 0); Grid.SetColumn(dateLabel, 0);
                Grid.SetRow(amountLabel, 0); Grid.SetColumn(amountLabel, 1);
                Grid.SetRow(titleLabel, 1); Grid.SetColumn(titleLabel, 0); Grid.SetColumnSpan(titleLabel, 2);
                Grid.SetRow(routeLabel, 2); Grid.SetColumn(routeLabel, 0); Grid.SetColumnSpan(routeLabel, 2);

                grid.Children.Add(dateLabel);
                grid.Children.Add(amountLabel);
                grid.Children.Add(titleLabel);
                grid.Children.Add(routeLabel);

                cardBorder.Content = grid;
                WelfareDetailTransactionList.Children.Add(cardBorder);
            }
        }

        private void RenderSubCategoryDetailData()
        {
            var detailItems = new List<WelfareItemModel>
            {
                new WelfareItemModel { Icon = "🚌", Title = "พาหนะ,ขนส่ง", AmountText = "1,200 บาท", CountText = "2 รายการ", ProgressRatio = 0 },
                new WelfareItemModel { Icon = "🛣️", Title = "ค่าทางด่วน", AmountText = "200 บาท", CountText = "1 รายการ", ProgressRatio = 0 },
                new WelfareItemModel { Icon = "⛽", Title = "ค่าน้ำมัน", AmountText = "1,200 บาท", CountText = "2 รายการ", ProgressRatio = 0.5 },
                new WelfareItemModel { Icon = "🅿️", Title = "ค่าที่จอดรถ", AmountText = "250 บาท", CountText = "1 รายการ", ProgressRatio = 0 },
                new WelfareItemModel { Icon = "🚗", Title = "ค่าเช่ารถ", AmountText = "7,500 บาท", CountText = "3 รายการ", ProgressRatio = 0 }
            };

            BuildWelfareCardsUI(SubCategoryDetailList, detailItems);
        }

        private void RenderCategoryData()
        {
            var workItems = new List<CategorySummaryItem>
            {
                new CategorySummaryItem { Icon = "🧳", Title = "ปฏิบัติงานนอกสถานที่", ValueText = "1,200 บาท", ProgressRatio = 0.6, ThemeColor = Color.FromArgb("#2979FF"), BadgeBgColor = Color.FromArgb("#E3F2FD"), BadgeTextColor = Color.FromArgb("#2979FF") },
                new CategorySummaryItem { Icon = "🚘", Title = "ค่าใช้จ่ายการเดินทาง", ValueText = "3,900 บาท", ProgressRatio = 0.8, ThemeColor = Color.FromArgb("#2979FF"), BadgeBgColor = Color.FromArgb("#E3F2FD"), BadgeTextColor = Color.FromArgb("#2979FF") },
                new CategorySummaryItem { Icon = "🧑‍🤝‍🧑", Title = "การรับรองลูกค้า", ValueText = "800 บาท", ProgressRatio = 0.4, ThemeColor = Color.FromArgb("#2979FF"), BadgeBgColor = Color.FromArgb("#E3F2FD"), BadgeTextColor = Color.FromArgb("#2979FF") }
            };

            var resourceItems = new List<CategorySummaryItem>
            {
                new CategorySummaryItem { Icon = "🖥️", Title = "สนับสนุนการทำงาน", ValueText = "3 รายการ", ProgressRatio = 0.5, ThemeColor = Color.FromArgb("#FF6D00"), BadgeBgColor = Color.FromArgb("#FFF3E0"), BadgeTextColor = Color.FromArgb("#FF6D00") },
                new CategorySummaryItem { Icon = "💻", Title = "เบิกอุปกรณ์เพื่อการปฏิบัติงาน", ValueText = "4 รายการ", ProgressRatio = 0.7, ThemeColor = Color.FromArgb("#FF6D00"), BadgeBgColor = Color.FromArgb("#FFF3E0"), BadgeTextColor = Color.FromArgb("#FF6D00") }
            };

            BuildCategoryListUI(WorkCategoryList, workItems, "งานและการปฏิบัติงาน");
            BuildCategoryListUI(ResourceCategoryList, resourceItems, "ทรัพยากรและสวัสดิการในการทำงาน");
        }

        private void BuildCategoryListUI(VerticalStackLayout container, List<CategorySummaryItem> items, string groupTitle)
        {
            container.Children.Clear();
            foreach (var item in items)
            {
                var cardBorder = new Border
                {
                    BackgroundColor = Colors.White,
                    StrokeShape = new RoundRectangle { CornerRadius = 16 },
                    StrokeThickness = 0,
                    Padding = new Thickness(12, 10)
                };

                var tapGesture = new TapGestureRecognizer();
                tapGesture.Tapped += (s, e) => OnCategoryItemTapped(item, groupTitle);
                cardBorder.GestureRecognizers.Add(tapGesture);

                var grid = new Grid
                {
                    ColumnDefinitions = { new ColumnDefinition { Width = GridLength.Auto }, new ColumnDefinition { Width = GridLength.Star }, new ColumnDefinition { Width = GridLength.Auto }, new ColumnDefinition { Width = GridLength.Auto } },
                    ColumnSpacing = 10
                };

                var iconBorder = new Border
                {
                    BackgroundColor = Color.FromArgb("#F5F5F5"),
                    StrokeShape = new RoundRectangle { CornerRadius = 12 },
                    WidthRequest = 36,
                    HeightRequest = 36,
                    StrokeThickness = 0,
                    Content = new Label { Text = item.Icon, FontSize = 16, HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center }
                };

                var titleStack = new VerticalStackLayout { Spacing = 4, VerticalOptions = LayoutOptions.Center };
                titleStack.Children.Add(new Label { Text = item.Title, FontSize = 13, FontAttributes = FontAttributes.Bold, TextColor = Color.FromArgb("#212121") });

                var progressBg = new Grid { HeightRequest = 4, BackgroundColor = Color.FromArgb("#E0E0E0") };
                var progressBar = new BoxView { Color = item.ThemeColor, HeightRequest = 4, HorizontalOptions = LayoutOptions.Start, WidthRequest = 140 * item.ProgressRatio };
                var progressContainer = new Grid();
                progressContainer.Children.Add(progressBg);
                progressContainer.Children.Add(progressBar);
                titleStack.Children.Add(progressContainer);

                var badgeBorder = new Border
                {
                    BackgroundColor = item.BadgeBgColor,
                    StrokeShape = new RoundRectangle { CornerRadius = 12 },
                    Padding = new Thickness(10, 4),
                    StrokeThickness = 0,
                    VerticalOptions = LayoutOptions.Center,
                    Content = new Label { Text = item.ValueText, TextColor = item.BadgeTextColor, FontSize = 11, FontAttributes = FontAttributes.Bold }
                };

                var chevronLabel = new Label { Text = "›", TextColor = Color.FromArgb("#2979FF"), FontSize = 20, VerticalOptions = LayoutOptions.Center };

                Grid.SetColumn(iconBorder, 0);
                Grid.SetColumn(titleStack, 1);
                Grid.SetColumn(badgeBorder, 2);
                Grid.SetColumn(chevronLabel, 3);

                grid.Children.Add(iconBorder);
                grid.Children.Add(titleStack);
                grid.Children.Add(badgeBorder);
                grid.Children.Add(chevronLabel);

                cardBorder.Content = grid;
                container.Children.Add(cardBorder);
            }
        }

        private void RenderWelfareData()
        {
            var welfareItems = new List<WelfareItemModel>
            {
                new WelfareItemModel { Icon = "🚌", Title = "พาหนะ,ขนส่ง", AmountText = "5,750 บาท", CountText = "3 รายการ", ProgressRatio = 0 },
                new WelfareItemModel { Icon = "🛣️", Title = "ค่าทางด่วน", AmountText = "200 บาท", CountText = "1 รายการ", ProgressRatio = 0 },
                new WelfareItemModel { Icon = "⛽", Title = "ค่าน้ำมัน", AmountText = "1,200 บาท", CountText = "2 รายการ", ProgressRatio = 0.5 },
                new WelfareItemModel { Icon = "🅿️", Title = "ค่าที่จอดรถ", AmountText = "250 บาท", CountText = "1 รายการ", ProgressRatio = 0 },
                new WelfareItemModel { Icon = "🚗", Title = "ค่าเช่ารถ", AmountText = "7,500 บาท", CountText = "3 รายการ", ProgressRatio = 0 }
            };

            BuildWelfareCardsUI(WelfareItemList, welfareItems);
        }

        private void BuildWelfareCardsUI(VerticalStackLayout container, List<WelfareItemModel> items)
        {
            container.Children.Clear();
            foreach (var item in items)
            {
                var cardBorder = new Border
                {
                    BackgroundColor = Colors.White,
                    StrokeShape = new RoundRectangle { CornerRadius = 16 },
                    StrokeThickness = 0,
                    Padding = new Thickness(12, 10)
                };

                var tapGesture = new TapGestureRecognizer();
                tapGesture.Tapped += (s, e) => OnWelfareItemTapped(item);
                cardBorder.GestureRecognizers.Add(tapGesture);

                var grid = new Grid
                {
                    ColumnDefinitions = { new ColumnDefinition { Width = GridLength.Auto }, new ColumnDefinition { Width = GridLength.Star }, new ColumnDefinition { Width = GridLength.Auto }, new ColumnDefinition { Width = GridLength.Auto } },
                    ColumnSpacing = 12
                };

                var iconBorder = new Border
                {
                    BackgroundColor = Color.FromArgb("#F5F6F8"),
                    StrokeShape = new RoundRectangle { CornerRadius = 12 },
                    WidthRequest = 40,
                    HeightRequest = 40,
                    StrokeThickness = 0,
                    Content = new Label { Text = item.Icon, FontSize = 18, HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center }
                };

                var titleStack = new VerticalStackLayout { Spacing = 4, VerticalOptions = LayoutOptions.Center };
                titleStack.Children.Add(new Label { Text = item.Title, FontSize = 13, FontAttributes = FontAttributes.Bold, TextColor = Color.FromArgb("#212121") });

                if (item.ProgressRatio > 0)
                {
                    var progressBg = new Grid { HeightRequest = 4, BackgroundColor = Color.FromArgb("#E3F2FD") };
                    var progressBar = new BoxView { Color = Color.FromArgb("#1976D2"), HeightRequest = 4, HorizontalOptions = LayoutOptions.Start, WidthRequest = 120 * item.ProgressRatio };
                    var progressContainer = new Grid();
                    progressContainer.Children.Add(progressBg);
                    progressContainer.Children.Add(progressBar);
                    titleStack.Children.Add(progressContainer);
                }

                var rightStack = new VerticalStackLayout { Spacing = 2, HorizontalOptions = LayoutOptions.End, VerticalOptions = LayoutOptions.Center };
                var amountBadge = new Border
                {
                    BackgroundColor = Color.FromArgb("#E3F2FD"),
                    StrokeShape = new RoundRectangle { CornerRadius = 10 },
                    Padding = new Thickness(10, 3),
                    StrokeThickness = 0,
                    HorizontalOptions = LayoutOptions.End,
                    Content = new Label { Text = item.AmountText, TextColor = Color.FromArgb("#2979FF"), FontSize = 11, FontAttributes = FontAttributes.Bold }
                };
                var countLabel = new Label { Text = item.CountText, TextColor = Color.FromArgb("#9E9E9E"), FontSize = 11, HorizontalOptions = LayoutOptions.End };

                rightStack.Children.Add(amountBadge);
                rightStack.Children.Add(countLabel);

                var chevronLabel = new Label { Text = "›", TextColor = Color.FromArgb("#2979FF"), FontSize = 20, VerticalOptions = LayoutOptions.Center };

                Grid.SetColumn(iconBorder, 0);
                Grid.SetColumn(titleStack, 1);
                Grid.SetColumn(rightStack, 2);
                Grid.SetColumn(chevronLabel, 3);

                grid.Children.Add(iconBorder);
                grid.Children.Add(titleStack);
                grid.Children.Add(rightStack);
                grid.Children.Add(chevronLabel);

                cardBorder.Content = grid;
                container.Children.Add(cardBorder);
            }
        }

        private async void OnCreateWithdrawalTapped(object sender, EventArgs e) => await DisplayAlert("เบิก", "ไปที่หน้าสร้างรายการเบิก", "ตกลง");
        private async void OnWithdrawalListTapped(object sender, EventArgs e) => await Shell.Current.GoToAsync("..");
    }

    // --- Data Models ---
    public class WelfareGroupCategoryItem
    {
        public string Icon { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public Color BgColor { get; set; } = Colors.Transparent;
        public Color IconBgColor { get; set; } = Colors.Blue;
    }

    public class WelfareItemModel
    {
        public string Icon { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string AmountText { get; set; } = string.Empty;
        public string CountText { get; set; } = string.Empty;
        public double ProgressRatio { get; set; } = 0;
    }

    public class CategorySummaryItem
    {
        public string Icon { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string ValueText { get; set; } = string.Empty;
        public double ProgressRatio { get; set; } = 0;
        public Color ThemeColor { get; set; } = Colors.Blue;
        public Color BadgeBgColor { get; set; } = Colors.LightBlue;
        public Color BadgeTextColor { get; set; } = Colors.Blue;
    }

    public class WelfareTransactionModel
    {
        public string DateText { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string RouteText { get; set; } = string.Empty;
        public string AmountText { get; set; } = string.Empty;
    }
}