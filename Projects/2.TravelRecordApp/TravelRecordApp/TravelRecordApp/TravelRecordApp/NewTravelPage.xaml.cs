using System;
using SQLite;
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
                    DisplayAlert("Failed", "Experience failed to be inserted", "Ok");
                }
            }
        }
    }
}