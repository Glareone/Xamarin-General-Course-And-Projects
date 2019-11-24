using Plugin.Geolocator;
using TravelRecordApp.Model;
using TravelRecordApp.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelRecordApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewTravelPage : ContentPage
    {
        private NewTravelViewModel _newTravelViewModel;

        public NewTravelPage()
        {
            InitializeComponent();

            _newTravelViewModel = new NewTravelViewModel();
            BindingContext = _newTravelViewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var locator = CrossGeolocator.Current;
            var position = await locator.GetPositionAsync();

            var venues = await Venue.GetVenues(position.Latitude, position.Longitude);

            venueListView.ItemsSource = venues;
        }
    }
}