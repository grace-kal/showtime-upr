using Microsoft.Maui.Controls;

namespace ShowTimeUpr.App
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}
