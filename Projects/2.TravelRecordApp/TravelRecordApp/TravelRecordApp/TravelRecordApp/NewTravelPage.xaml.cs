﻿using System;
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
            var post = new Post
            {
                Experience = ExperienceEntry.Text
            };

            using (var connection = new SQLiteConnection(App.DatabaseLocation))
            {
                connection.CreateTable<Post>();

                var insertRowsAmount = connection.Insert(post);

                if (insertRowsAmount > 0)
                {
                    DisplayAlert("Success", "Experience successfully inserted", "Ok");
                }
                else
                {
                    DisplayAlert("Failure", "Experience failed to be inserted", "Ok");
                }
            }
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var locator = CrossGeolocator.Current;
            var position = await locator.GetPositionAsync();

            var venues = await VenueLogic.GetVenues(position.Latitude, position.Longitude);
        }
    }
}