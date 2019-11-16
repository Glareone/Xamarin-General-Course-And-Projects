using SQLite;
using TravelRecordApp.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelRecordApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HistoryPage : ContentPage
    {
        public HistoryPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            using (var dbConnection = new SQLiteConnection(App.DatabaseLocation))
            {
                dbConnection.CreateTable<Post>();

                postListView.ItemsSource = dbConnection.Table<Post>().ToList();
            }
        }
    }
}