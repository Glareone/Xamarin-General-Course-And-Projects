using System;
using TravelRecordApp.Helpers;
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

            // synchronize local db and cloud db
            await AzureAppServiceHelper.SyncAsync();
        }

        private void PostListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (postListView.SelectedItem is Post selectedPost) Navigation.PushAsync(new PostDetailPage(selectedPost));
        }

        private async void Experience_MenuItem_OnDelete(object sender, EventArgs e)
        {
            var selectedPost = (Post)((MenuItem) sender).CommandParameter;
            await _historyViewModel.DeletePost(selectedPost);

            await _historyViewModel.UpdatePosts();
        }

        private async void PostListView_OnRefreshing(object sender, EventArgs e)
        {
            await _historyViewModel.UpdatePosts();

            // synchronize local db and cloud db
            // we should add it here because we want to update elements if anyone was changes while we were offline.
            await AzureAppServiceHelper.SyncAsync();

            // the refreshing is done.
            postListView.IsRefreshing = false; 
        }
    }
}