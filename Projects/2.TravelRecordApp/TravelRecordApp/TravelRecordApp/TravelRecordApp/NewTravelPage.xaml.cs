using System;
using System.Linq;
using Plugin.Geolocator;
using SQLite;
using TravelRecordApp.Logic;
using TravelRecordApp.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelRecordApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewTravelPage : ContentPage
    {
        public NewTravelPage()
        {
            InitializeComponent();
        }

        private async void SaveExperience_OnClicked(object sender, EventArgs e)
        {
            try
            {
                var selectedVenue = venueListView.SelectedItem as Venue;
                var firstCategory = selectedVenue?.categories.FirstOrDefault();

                var post = new Post
                {
                    Experience = ExperienceEntry.Text,
                    CategoryId = firstCategory?.id,
                    CategoryName = firstCategory?.name,
                    Address = selectedVenue?.location.address,
                    Distance = selectedVenue?.location?.distance ?? default,
                    Latitude = selectedVenue?.location?.lat ?? default,
                    Longitude = selectedVenue?.location?.lng ?? default,
                    VenueName = selectedVenue?.name,
                    UserId = App.User.Id
                };

                await App.MobileServiceClient.GetTable<Post>().InsertAsync(post);
                await DisplayAlert("Success", "Post was saved", "Ok");
            }
            catch (Exception)
            {
                await DisplayAlert("Failure", "Post was not saved properly", "Ok");
            }
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var locator = CrossGeolocator.Current;
            var position = await locator.GetPositionAsync();

            var venues = await VenueLogic.GetVenues(position.Latitude, position.Longitude);

            venueListView.ItemsSource = venues;
        }
    }
}