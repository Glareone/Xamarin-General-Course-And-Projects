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

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            postListView.ItemsSource = await App.MobileServiceClient.GetTable<Post>().Where(p => p.UserId == App.User.Id).ToListAsync();
        }

        private void PostListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (postListView.SelectedItem is Post selectedPost) Navigation.PushAsync(new PostDetailPage(selectedPost));
        }
    }
}