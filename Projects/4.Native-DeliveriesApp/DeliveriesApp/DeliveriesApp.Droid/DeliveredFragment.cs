﻿using Android.OS;
using Android.Support.V4.App;
using Android.Views;

namespace DeliveriesApp.Droid
{
    public class DeliveredFragment : Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            return inflater.Inflate(Resource.Layout.Delivered, container, false);
        }
    }
}