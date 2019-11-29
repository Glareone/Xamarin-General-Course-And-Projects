using System;

using Android.App;
using Android.OS;
using Android.Widget;
using DeliveriesApp.Model;

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
            var result = await Users.Register(_emailEditText.Text, _passwordEditText.Text, _confirmPasswordEditText.Text);

            if (result)
            {
                Toast.MakeText(this, "Success", ToastLength.Long).Show();
            }
            Toast.MakeText(this, "Error", ToastLength.Long).Show();
        }
    }
}