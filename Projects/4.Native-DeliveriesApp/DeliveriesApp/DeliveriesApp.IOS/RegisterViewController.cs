using Foundation;
using System;
using DeliveriesApp.Model;
using UIKit;

namespace DeliveriesApp.iOS
{
    public partial class RegisterViewController : UIViewController
    {
        public string emailAddress;
        public RegisterViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            emailTextField.Text = emailAddress;

            registerButton.TouchUpInside += RegisterButton_TouchUpInside;
        }

        private async void RegisterButton_TouchUpInside(object sender, EventArgs e)
        {
            var result = await Users.Register(emailTextField.Text, passwordTextField.Text,
                confirmpasswordTextField.Text);

            UIAlertController alert;

            if (result)
            {
                alert = UIAlertController.Create("Success", "User inserted", UIAlertControllerStyle.Default);
            }
            else
            {
                alert = UIAlertController.Create("Failure", "Try Again", UIAlertControllerStyle.Default);
            }

            alert.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Default, null));

            PresentViewController(alert, true, null);
        }
    }
}