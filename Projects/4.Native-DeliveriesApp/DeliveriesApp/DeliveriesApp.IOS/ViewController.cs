using System;
using Foundation;
using UIKit;
using System.Linq;
using DeliveriesApp.Model;

namespace DeliveriesApp.iOS
{
	public partial class ViewController : UIViewController
	{
		public ViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

            signinButton.TouchUpInside += SigninButton_TouchUpInside;
		}

        private async void SigninButton_TouchUpInside(object sender, EventArgs e)
        {
            var email = emailTextField.Text;
            var password = passwordTextField.Text;
            UIAlertController alert;

            var loginResult = await Users.Login(email, password);

            if (!loginResult)
            {
                alert = UIAlertController.Create("Error", "Password and Email are incorrect",
                    UIAlertControllerStyle.Alert);
            }
            else
            {
                alert = UIAlertController.Create("Success", "Welcome",
                    UIAlertControllerStyle.Alert);
            }

            alert.AddAction(UiAlertAction.Create("Ok", UiAlertActionStyle.Default, null));
            PresentViewController(alert, true, null);

        }

        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {
            base.PrepareForSegue(segue, sender);

            if(segue.Identifier == "registerSegue")
            {
                var destinationViewController = segue.DestinationViewController as RegisterViewController;
                destinationViewController.emailAddress = emailTextField.Text;
            }
        }

        public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}

