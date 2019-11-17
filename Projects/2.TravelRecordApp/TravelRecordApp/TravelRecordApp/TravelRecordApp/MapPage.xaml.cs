using System;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelRecordApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapPage : ContentPage
    {
        private bool _hasLocationPermission;

        public MapPage()
        {
            InitializeComponent();

            GetPermissions();
        }

        // Get and check permissions for Plugin.Permissions.
        private async void GetPermissions()
        {
            try
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.LocationWhenInUse);
                if (status != PermissionStatus.Granted)
                {
                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission
                        .LocationWhenInUse))
                    {
                        await DisplayAlert("Your location is needed", "We need to get access to your location", "Ok");
                    }

                    var result = await CrossPermissions.Current.RequestPermissionsAsync(Permission.LocationWhenInUse);
                    if (result.ContainsKey(Permission.LocationWhenInUse))
                    {
                        status = result[Permission.LocationWhenInUse];
                    }
                }

                if (status == PermissionStatus.Granted)
                {
                    LocationsMap.IsShowingUser = true;
                    _hasLocationPermission = true;

                    GetLocation();
                }
                else
                {
                    await DisplayAlert("Location access denied", "We need to get access to your location", "Ok");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Ok");
                throw;
            }
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (_hasLocationPermission)
            {
                var currentGeoLocator = CrossGeolocator.Current;

                currentGeoLocator.PositionChanged += CurrentGeoLocator_OnPositionChanged;
                await currentGeoLocator.StartListeningAsync(TimeSpan.Zero, 100);
            }

            GetLocation();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            CrossGeolocator.Current.StopListeningAsync();
            CrossGeolocator.Current.PositionChanged -= CurrentGeoLocator_OnPositionChanged;
        }

        private void CurrentGeoLocator_OnPositionChanged(object sender, PositionEventArgs e)
        {
            MoveMap(e.Position);
        }

        private async void GetLocation()
        {
            var currentGeoLocator = CrossGeolocator.Current;
            var position = await currentGeoLocator.GetPositionAsync();

            MoveMap(position);
        }

        private void MoveMap(Position position)
        {
            var mapCenter = new Xamarin.Forms.Maps.Position(position.Latitude, position.Longitude);
            var mapSpan = new Xamarin.Forms.Maps.MapSpan(mapCenter, 1, 1);
            LocationsMap.MoveToRegion(mapSpan);
        }
    }
}