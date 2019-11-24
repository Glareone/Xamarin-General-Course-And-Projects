using TravelRecordApp.Model;
using TravelRecordApp.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelRecordApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HistoryPage : ContentPage
    {
        private HistoryViewModel _historyViewModel;

        public HistoryPage()
        {
            InitializeComponent();

            _historyViewModel = new HistoryViewModel();
            BindingContext = _historyViewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await _historyViewModel.UpdatePosts();
        }

        private void PostListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (postListView.SelectedItem is Post selectedPost) Navigation.PushAsync(new PostDetailPage(selectedPost));
        }
    }
}