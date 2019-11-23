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

            postListView.ItemsSource = await Post.Read();
        }

        private void PostListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (postListView.SelectedItem is Post selectedPost) Navigation.PushAsync(new PostDetailPage(selectedPost));
        }
    }
}