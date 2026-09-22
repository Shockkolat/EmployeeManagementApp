namespace EmployeeManagementApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(Welfare_E_List), typeof(Welfare_E_List));
            Routing.RegisterRoute(nameof(SummaryPage), typeof(SummaryPage));
        }
    }
}
