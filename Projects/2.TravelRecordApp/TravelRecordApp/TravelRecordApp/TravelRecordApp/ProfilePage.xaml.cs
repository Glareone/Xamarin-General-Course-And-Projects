using System.Linq;
using SQLite;
using TravelRecordApp.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelRecordApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            using (var connection = new SQLiteConnection(App.DatabaseLocation))
            {
                var postTable = connection.Table<Post>().ToList();

                var postsCategories = (from p in postTable
                        orderby p.CategoryId
                        select p.CategoryName).Distinct().ToList();

                var categoriesWithCount = postsCategories.Select(category => new
                {
                    categoryName = category,
                    categoryCount = postTable.Count(post => post.CategoryName == category)
                }).ToDictionary(k => k.categoryName, v => v.categoryCount);

                categoriesListView.ItemsSource = categoriesWithCount;

                postCountLabel.Text = postTable.Count.ToString();
            }
        }
    }
}