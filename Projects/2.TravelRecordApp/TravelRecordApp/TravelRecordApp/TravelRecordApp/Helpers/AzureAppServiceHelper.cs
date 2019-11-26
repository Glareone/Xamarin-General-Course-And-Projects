#define OFFLINE_SYNC_ENABLED

using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.Sync;

namespace TravelRecordApp.Helpers
{
    /// Synchronization class.
    public class AzureAppServiceHelper
    {
        /// Sync between local Sqlite and Azure cloud db
        public static async Task SyncAsync()
        {
            ReadOnlyCollection<MobileServiceTableOperationError> syncErrors = null;

            try
            {
                await App.MobileServiceClient.SyncContext.PushAsync();

                // Pull all information from cloud database to local db
                await App.postsTable.PullAsync("userPosts", "");
            }
            catch (MobileServicePushFailedException mspfe)
            {
                if (mspfe.PushResult != null)
                {
                    syncErrors = mspfe.PushResult.Errors;
                }
            }
            catch (Exception ex)
            {
            }

            
            if (syncErrors != null)
            {
                
                foreach (var mobileServiceTableOperationError in syncErrors)
                {
                    if (mobileServiceTableOperationError.OperationKind == MobileServiceTableOperationKind.Update && mobileServiceTableOperationError.Result != null)
                    {
                        // revert changes in the server's db copy.
                        await mobileServiceTableOperationError.CancelAndUpdateItemAsync(mobileServiceTableOperationError.Result);
                    }
                    else
                    {
                        // revert changes in local db (if error type differs from Update)
                        // for situation which something is not presented in a cloud and shouldn't be in sqlite
                        await mobileServiceTableOperationError.CancelAndDiscardItemAsync();
                    }
                }
            }
        }
    }
}