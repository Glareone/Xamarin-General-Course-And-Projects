using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using TravelRecordApp.Model;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;
using GeoLocatorPosition = Plugin.Geolocator.Abstractions.Position;
using MapPosition = Xamarin.Forms.Maps.Position;

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
                        await DisplayAlert("Your location is needed", "We need to get access to your location", "Ok");

                    var result = await CrossPermissions.Current.RequestPermissionsAsync(Permission.LocationWhenInUse);
                    if (result.ContainsKey(Permission.LocationWhenInUse)) status = result[Permission.LocationWhenInUse];
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

            var posts = await GetPosts();

            DisplayOnMap(posts);
        }

        private void DisplayOnMap(IEnumerable<Post> posts)
        {
            foreach (var post in posts)
            {
                var position = new MapPosition(post.Latitude, post.Longitude);

                var pin = new Pin
                {
                    Type = PinType.SavedPin,
                    Position = position,
                    Label = post.VenueName,
                    Address = post.Address
                };

                LocationsMap.Pins.Add(pin);
            }
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

        private void MoveMap(GeoLocatorPosition position)
        {
            var mapCenter = new MapPosition(position.Latitude, position.Longitude);
            var mapSpan = new MapSpan(mapCenter, 1, 1);
            LocationsMap.MoveToRegion(mapSpan);
        }

        private static async Task<IEnumerable<Post>> GetPosts()
        {
            return await Post.Read();
        }
    }
}