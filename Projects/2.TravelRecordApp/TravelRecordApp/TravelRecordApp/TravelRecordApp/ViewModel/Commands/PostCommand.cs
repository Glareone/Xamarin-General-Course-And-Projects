using System;
using System.Windows.Input;
using TravelRecordApp.Model;

namespace TravelRecordApp.ViewModel.Commands
{
    public class PostCommand: ICommand
    {
        public NewTravelViewModel NewTravelViewModel { get; set; }

        public event EventHandler CanExecuteChanged;

        public PostCommand(NewTravelViewModel newTravelViewModel)
        {
            NewTravelViewModel = newTravelViewModel;
        }

        public bool CanExecute(object parameter)
        {
            var post = (Post) parameter;

            if (post == null || string.IsNullOrEmpty(post.Experience) || post.Venue == null)
            {
                return false;
            }

            return true;
        }

        public void Execute(object parameter)
        {
            var post = (Post) parameter;

            NewTravelViewModel.PublishPost(post);
        }
    }
}
