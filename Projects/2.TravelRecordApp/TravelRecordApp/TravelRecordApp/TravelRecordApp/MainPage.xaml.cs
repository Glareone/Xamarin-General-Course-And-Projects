using System;
using System.ComponentModel;
using TravelRecordApp.Model;
using TravelRecordApp.ViewModel;
using Xamarin.Forms;

namespace TravelRecordApp
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        private MainViewModel mainViewModel;

        public MainPage()
        {
            InitializeComponent();

            mainViewModel = new MainViewModel();
            BindingContext = mainViewModel;

            IconImage.Source = ImageSource.FromResource("TravelRecordApp.Assets.Images.plane.png", typeof(MainPage));
        }
    }
}