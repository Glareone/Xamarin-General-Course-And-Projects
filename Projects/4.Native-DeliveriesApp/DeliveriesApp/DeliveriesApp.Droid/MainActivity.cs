﻿using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using DeliveriesApp.Model;

namespace DeliveriesApp.Droid
{
    [Activity(Label = "DeliveriesApp.Droid", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private EditText _emailEditText;
        private EditText _passwordEditText;

        private Button _signInButton;
        private Button _registerButton;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            _emailEditText = FindViewById<EditText>(Resource.Id.EmailEditText);
            _passwordEditText = FindViewById<EditText>(Resource.Id.PasswordEditText);
            _signInButton = FindViewById<Button>(Resource.Id.SignInButton);
            _registerButton = FindViewById<Button>(Resource.Id.RegisterButton);

            _signInButton.Click += SignInButton_Click;
            _registerButton.Click += RegisterButton_Click;
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        private void RegisterButton_Click(object sender, System.EventArgs e)
        {
            var intent = new Intent(this, typeof(RegisterActivity));

            // if we want to provide information from one page to another. Works like a dictionary
            // for example - information from emailEditText to emailEditText on register page.
            intent.PutExtra("email", _emailEditText.Text);

            StartActivity(intent);
        }

        private async void SignInButton_Click(object sender, System.EventArgs e)
        {
            var email = _emailEditText.Text;
            var password = _passwordEditText.Text;

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                Toast.MakeText(this, "Email and password cannot be empty", ToastLength.Long).Show();
                return;
            }

            var loginResult = await Users.Login(email, password);

            if (!loginResult)
            {
                Toast.MakeText(this, "Email and password are incorrect", ToastLength.Long).Show();
                return;
            }

            Toast.MakeText(this, "Login successful", ToastLength.Long).Show();

        }
    }
}