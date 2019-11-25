using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using TravelRecordApp.Annotations;
using TravelRecordApp.Model;
using TravelRecordApp.ViewModel.Commands;

namespace TravelRecordApp.ViewModel
{
    public class NewTravelViewModel: INotifyPropertyChanged
    {
        public PostCommand PostCommand { get; set; }

        private Post post;

        public Post Post
        {
            get => post;
            set
            {
                post = value;
                OnPropertyChanged(nameof(Post));
            }
        }

        private string experience;

        public string Experience
        {
            get => experience;
            set
            {
                experience = value;
                Post = new Post
                {
                    Experience = this.Experience,
                    Venue = this.venue
                };
                OnPropertyChanged(nameof(Experience));
            }
        }

        private Venue venue;

        public Venue Venue
        {
            get => venue;
            set
            {
                venue = value;
                Post = new Post
                {
                    Experience = this.Experience,
                    Venue = this.venue
                };
                OnPropertyChanged(nameof(Venue));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public NewTravelViewModel()
        {
            PostCommand = new PostCommand(this);
            Post = new Post();
            Venue = new Venue();
        }

        public async void PublishPost(Post post)
        {
            try
            {
                Post.Insert(post);
                await App.Current.MainPage.DisplayAlert("Success", "Post was saved", "Ok");
            }
            catch (Exception)
            {
                await App.Current.MainPage.DisplayAlert("Failure", "Post was not saved properly", "Ok");
            }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
