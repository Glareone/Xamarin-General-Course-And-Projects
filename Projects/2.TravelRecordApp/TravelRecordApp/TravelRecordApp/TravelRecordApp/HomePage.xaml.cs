using TravelRecordApp.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelRecordApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : TabbedPage
    {
        private readonly HomeViewModel _viewModel;

        public HomePage()
        {
            InitializeComponent();

            _viewModel = new HomeViewModel();
            BindingContext = _viewModel;
        }
    }
}