using System;
using System.Windows.Input;

namespace TravelRecordApp.ViewModel.Commands
{
    public class RegisterNavigationCommand: ICommand
    {
        public MainViewModel MainViewModel { get; set; }

        public event EventHandler CanExecuteChanged;

        public RegisterNavigationCommand(MainViewModel mainViewModel)
        {
            MainViewModel = mainViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            MainViewModel.Navigate();
        }
    }
}
