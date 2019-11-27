using TravelRecordApp.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelRecordApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        private readonly RegisterViewModel _registerViewModel;

        public RegisterPage()
        {
            InitializeComponent();

            _registerViewModel = new RegisterViewModel();
            ContainerStackLayout.BindingContext = _registerViewModel;
        }
    }
}