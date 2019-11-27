using System;
using System.Windows.Input;
using TravelRecordApp.Model;

namespace TravelRecordApp.ViewModel.Commands
{
    public class RegisterCommand : ICommand
    {
        public RegisterCommand(RegisterViewModel registerViewModel)
        {
            RegisterViewModel = registerViewModel;
        }

        public RegisterViewModel RegisterViewModel { get; set; }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            var user = (Users) parameter;

            if (user == null || string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password) ||
                user.Password != user.ConfirmPassword)
                return false;

            return true;
        }

        public void Execute(object parameter)
        {
            var user = (Users) parameter;
            RegisterViewModel.Register(user);
        }
    }
}