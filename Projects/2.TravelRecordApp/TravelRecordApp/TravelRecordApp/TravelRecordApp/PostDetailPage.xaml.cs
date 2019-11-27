using System;
using TravelRecordApp.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelRecordApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PostDetailPage : ContentPage
    {
        private readonly Post _selectedPost;

        public PostDetailPage(Post selectedPost)
        {
            InitializeComponent();

            _selectedPost = selectedPost;
            ExperienceEntry.Text = selectedPost.Experience;
            VenueLabel.Text = selectedPost.VenueName;
            CategoryLabel.Text = selectedPost.CategoryName;
            AddressLabel.Text = selectedPost.Address;
            CoordinatesLabel.Text = $"lat: {selectedPost.Latitude}, lng: {selectedPost.Longitude}";
            DistanceLabel.Text = selectedPost.Distance.ToString();
        }

        private async void UpdateSelectedPost_OnClicked(object sender, EventArgs e)
        {
            _selectedPost.Experience = ExperienceEntry.Text;

            await App.MobileServiceClient.GetTable<Post>().UpdateAsync(_selectedPost);

            await DisplayAlert("Success", "Experience successfully updated", "Ok");

            await Navigation.PopAsync();
        }

        private async void DeleteSelectedPost_OnClicked(object sender, EventArgs e)
        {
            await App.MobileServiceClient.GetTable<Post>().DeleteAsync(_selectedPost);

            await DisplayAlert("Success", "Experience successfully deleted", "Ok");

            await Navigation.PopAsync();
        }
    }
}