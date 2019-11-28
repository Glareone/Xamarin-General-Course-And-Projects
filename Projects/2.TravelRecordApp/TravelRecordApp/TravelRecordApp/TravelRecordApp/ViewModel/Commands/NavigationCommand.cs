﻿using System;
using System.Windows.Input;

namespace TravelRecordApp.ViewModel.Commands
{
    public class NavigationCommand : ICommand
    {
        public NavigationCommand(HomeViewModel homeViewModel)
        {
            HomeViewModel = homeViewModel;
        }

        public HomeViewModel HomeViewModel { get; set; }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            HomeViewModel.Navigate();
        }
    }
}