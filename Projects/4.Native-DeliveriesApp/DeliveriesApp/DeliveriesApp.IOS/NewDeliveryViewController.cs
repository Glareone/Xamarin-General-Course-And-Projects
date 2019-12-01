using DeliveriesApp.Model;
using Foundation;
using System;
using UIKit;

namespace DeliveriesApp.iOS
{
    public partial class NewDeliveryViewController : UIViewController
    {
        public NewDeliveryViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            saveBarButtonItem.Clicked += SaveBarButtonItem_Clicked;
        }

        private async void SaveBarButtonItem_Clicked(object sender, EventArgs e)
        {
            Delivery delivery = new Delivery()
            {
                Name = packageNameTextField.Text,
                Status = 0
            };

            await Delivery.InsertDelivery(delivery);
        }
    }
}