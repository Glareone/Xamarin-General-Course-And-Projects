using System.ComponentModel;
using TravelRecordApp.ViewModel;
using Xamarin.Forms;

namespace TravelRecordApp
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        private readonly MainViewModel _mainViewModel;

        public MainPage()
        {
            InitializeComponent();

            _mainViewModel = new MainViewModel();
            BindingContext = _mainViewModel;

            IconImage.Source = ImageSource.FromResource("TravelRecordApp.Assets.Images.plane.png", typeof(MainPage));
        }
    }
}