using System.Net.Http;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using TravelRecordApp.Model;
using Xamarin.Forms;

namespace TravelRecordApp
{
    public partial class App : Application
    {
        public static string DatabaseLocation = string.Empty;

        public static MobileServiceClient MobileServiceClient =
            new MobileServiceClient("https://travelrecordapp-glareone.azurewebsites.net", new HttpClientHandler());

        public static IMobileServiceSyncTable<Post> postsTable;

        public static Users User = new Users();

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

            // To use both storages simultaneously - offline and online.
            var combinationWithOnlineAndOfflineSqliteStore = new MobileServiceSQLiteStore(databaseLocation);
            // what should be syncing
            combinationWithOnlineAndOfflineSqliteStore.DefineTable<Post>();
            // how should be syncing
            MobileServiceClient.SyncContext.InitializeAsync(combinationWithOnlineAndOfflineSqliteStore);

            postsTable = MobileServiceClient.GetSyncTable<Post>();
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