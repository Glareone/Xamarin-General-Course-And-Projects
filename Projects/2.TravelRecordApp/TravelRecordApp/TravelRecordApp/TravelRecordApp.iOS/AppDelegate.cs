using System;
using System.IO;
using Foundation;
using Microsoft.WindowsAzure.MobileServices;
using UIKit;
using Xamarin;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

namespace TravelRecordApp.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public class AppDelegate : FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            Forms.Init();

            // We have to call init method to access to be able to use AzureServices
            CurrentPlatform.Init();

            // Method which prepares IOS to use Xamarin Maps
            FormsMaps.Init();

            var dbName = "travel_db.sqlite";

            // IOS doesn't allow store dbfile in Personal folder.
            // That's why we need to go up from personal folder and move to another folder near it with name "Library"
            var folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "..",
                "Library");
            var fullPath = Path.Combine(folderPath, dbName);

            LoadApplication(new App(fullPath));

            return base.FinishedLaunching(app, options);
        }
    }
}