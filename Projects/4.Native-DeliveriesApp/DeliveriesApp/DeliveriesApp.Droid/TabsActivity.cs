using Android.App;
using Android.OS;
using Android.Support.Design.Widget;

namespace DeliveriesApp.Droid
{
    [Activity(Label = "TabsActivity")]
    public class TabsActivity : Android.Support.V4.App.FragmentActivity
    {
        private TabLayout _tabLayout;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Tabs);

            _tabLayout = FindViewById<TabLayout>(Resource.Id.mainTabLayout);
            _tabLayout.TabSelected += TabLayout_Selected;

            FragmentNavigate(new DeliveriesFragment());
        }

        private void TabLayout_Selected(object sender, TabLayout.TabSelectedEventArgs e)
        {
            switch (e.Tab.Position)
            {
                case 0:
                    FragmentNavigate(new DeliveriesFragment());
                    break;
                case 1:
                    FragmentNavigate(new DeliveredFragment());
                    break;
                case 2:
                    FragmentNavigate(new ProfileFragment());
                    break;
            }
        }

        private void FragmentNavigate(Android.Support.V4.App.Fragment fragment)
        {
            var transaction = SupportFragmentManager.BeginTransaction();
            // inject (replace) selected fragment (Deliveries, Delivered, Profile clicking on the tab) to FragmentLayout.
            transaction.Replace(Resource.Id.contentFrame, fragment);
            transaction.Commit();
        }
    }
}