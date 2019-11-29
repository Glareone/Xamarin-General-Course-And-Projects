using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;

namespace DeliveriesApp.Droid
{
    [Activity(Label = "DeliveriesApp.Droid", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private EditText namEditText;

        private Button helloButton;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            namEditText = FindViewById<EditText>(Resource.Id.nameEditText);
            helloButton = FindViewById<Button>(Resource.Id.helloButton);

            helloButton.Click += HelloButton_Click;
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        private void HelloButton_Click(object sender, System.EventArgs e)
        {
            Toast.MakeText(this, $"Hello, {namEditText.Text}", ToastLength.Long).Show();
        }
    }
}