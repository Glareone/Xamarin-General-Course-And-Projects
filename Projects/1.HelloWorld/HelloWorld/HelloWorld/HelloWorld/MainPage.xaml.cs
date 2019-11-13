using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace HelloWorld
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void HandleClicked(object sender, EventArgs e)
        {
            var userName = nameEntry.Text;
            var greeting = $"Hello, {userName}!";

            greetingLabel.Text = greeting;
        }
    }
}
