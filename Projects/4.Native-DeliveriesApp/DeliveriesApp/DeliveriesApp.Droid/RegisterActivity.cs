using System;

using Android.App;
using Android.OS;
using Android.Widget;

namespace DeliveriesApp.Droid
{
    [Activity(Label = "RegisterActivity")]
    public class RegisterActivity : Activity
    {
        private EditText _emailEditText;
        private EditText _passwordEditText;
        private EditText _confirmPasswordEditText;

        private Button _registerButton;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Register);

            _emailEditText = FindViewById<EditText>(Resource.Id.RegisterEmailEditText);
            _passwordEditText = FindViewById<EditText>(Resource.Id.RegisterPasswordEditText);
            _confirmPasswordEditText = FindViewById<EditText>(Resource.Id.ConfirmPasswordEditText);
            _registerButton = FindViewById<Button>(Resource.Id.RegisterUserButton);

            _registerButton.Click += RegisterButton_Click;

            // get information which was provided from MainActivity using intent
            var email = Intent.GetStringExtra("email");
            // and set this text
            _emailEditText.Text = email;
        }

        private async void RegisterButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_passwordEditText.Text) || string.IsNullOrEmpty(_emailEditText.Text) ||
                _passwordEditText.Text != _confirmPasswordEditText.Text)
            {
                Toast.MakeText(this, "Form is filled incorrect", ToastLength.Long).Show();
                return;
            }

            var user = new Users
            {
                Email = _emailEditText.Text,
                Password = _passwordEditText.Text
            };

            await MainActivity.MobileServiceClient.GetTable<Users>().InsertAsync(user);
            Toast.MakeText(this, "Success", ToastLength.Long).Show();
        }
    }
}