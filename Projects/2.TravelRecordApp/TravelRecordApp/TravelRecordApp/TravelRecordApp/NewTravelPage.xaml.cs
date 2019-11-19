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

        private void SaveExperience_OnClicked(object sender, EventArgs e)
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
                    VenueName = selectedVenue?.name
                };

                using (var connection = new SQLiteConnection(App.DatabaseLocation))
                {
                    connection.CreateTable<Post>();

                    var insertRowsAmount = connection.Insert(post);

                    if (insertRowsAmount > 0)
                        DisplayAlert("Success", "Experience successfully inserted", "Ok");
                    else
                        DisplayAlert("Failure", "Experience failed to be inserted", "Ok");
                }
            }
            catch (NullReferenceException ex)
            {
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