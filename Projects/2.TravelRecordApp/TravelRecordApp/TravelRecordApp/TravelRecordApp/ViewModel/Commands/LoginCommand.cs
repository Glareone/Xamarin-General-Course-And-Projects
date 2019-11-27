using System;
using System.Windows.Input;
using TravelRecordApp.Model;

namespace TravelRecordApp.ViewModel.Commands
{
    public class LoginCommand : ICommand
    {
        public LoginCommand(MainViewModel mainViewModel)
        {
            MainViewModel = mainViewModel;
        }

        public MainViewModel MainViewModel { get; set; }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            var user = (Users) parameter;

            if (user == null || string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password)) return false;

            return true;
        }

        public void Execute(object parameter)
        {
            MainViewModel.Login();
        }
    }
}