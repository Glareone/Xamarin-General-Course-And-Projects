using System.Net.Http;
using Microsoft.WindowsAzure.MobileServices;
using Xamarin.Forms;

namespace TravelRecordApp
{
    public partial class App : Application
    {
        public static string DatabaseLocation = string.Empty;

        public static MobileServiceClient MobileServiceClient =
            new MobileServiceClient("https://travelrecordapp-glareone.azurewebsites.net", new HttpClientHandler());

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }

        public App(string databaseLocation)
        {
            DatabaseLocation = databaseLocation;
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}