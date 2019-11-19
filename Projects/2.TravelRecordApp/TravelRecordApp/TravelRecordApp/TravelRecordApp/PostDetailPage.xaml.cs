using System;
using SQLite;
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
        }

        private void UpdateSelectedPost_OnClicked(object sender, EventArgs e)
        {
            _selectedPost.Experience = ExperienceEntry.Text;

            using (var connection = new SQLiteConnection(App.DatabaseLocation))
            {
                var updatedRowsAmount = connection.Update(_selectedPost);

                if (updatedRowsAmount > 0)
                    DisplayAlert("Success", "Experience successfully updated", "Ok");
                else
                    DisplayAlert("Failure", "Experience failed to be updated", "Ok");
            }

            Navigation.PopAsync();
        }

        private void DeleteSelectedPost_OnClicked(object sender, EventArgs e)
        {
            using (var connection = new SQLiteConnection(App.DatabaseLocation))
            {
                var deletedRowsAmount = connection.Delete(_selectedPost);

                if (deletedRowsAmount > 0)
                    DisplayAlert("Success", "Experience successfully deleted", "Ok");
                else
                    DisplayAlert("Failure", "Experience failed to be deleted", "Ok");
            }

            Navigation.PopAsync();
        }
    }
}