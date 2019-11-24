using System.Collections.Generic;
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

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var posts = (List<Post>)await Post.Read();

            var categoriesWithCount = Post.PostedCategories(posts);

            categoriesListView.ItemsSource = categoriesWithCount;

            postCountLabel.Text = categoriesWithCount.Count.ToString();
        }
    }
}