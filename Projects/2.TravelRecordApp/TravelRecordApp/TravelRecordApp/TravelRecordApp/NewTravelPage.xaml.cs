using System;
using Plugin.Geolocator;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
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
            try
            {
                // do we have permissions to get user's location. If not - request this access from user.
                var status =
                    await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);

                if (status != PermissionStatus.Granted)
                {
                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Location))
                    {
                        await DisplayAlert("Need permission", "We will have to access to your location", "Ok");
                    }

                    var permissionRequestResults =
                        await CrossPermissions.Current.RequestPermissionsAsync(Permission.Location);

                    if (permissionRequestResults.ContainsKey(Permission.Location))
                    {
                        status = permissionRequestResults[Permission.Location];
                    }
                }

                if (status == PermissionStatus.Granted)
                {
                    var locator = CrossGeolocator.Current;
                    var position = await locator.GetPositionAsync();

                    var venues = await Venue.GetVenues(position.Latitude, position.Longitude);
                    venueListView.ItemsSource = venues;
                }
                else
                {
                    await DisplayAlert("No permission",
                        "You didn't granted permission to access to your location, we cannot proceed", "Ok");
                }
            }
            catch (Exception)
            {
                await DisplayAlert("Oops", "Something wrong", "Ok");
            }
        }
    }
}