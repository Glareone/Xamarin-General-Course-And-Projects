using Foundation;
using System;
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
            if (!string.IsNullOrEmpty(passwordTextField.Text))
            {
                if (passwordTextField.Text == confirmpasswordTextField.Text)
                {
                    var user = new User()
                    {
                        Email = emailTextField.Text,
                        Password = passwordTextField.Text
                    };

                    await AppDelegate.MobileService.GetTable<User>().InsertAsync(user);

                    var alert = UIAlertController.Create("Success", "User inserted", UIAlertControllerStyle.Alert);

                    alert.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));

                    PresentViewController(alert, true, null);

                    return;
                }
            }
        }
    }
}