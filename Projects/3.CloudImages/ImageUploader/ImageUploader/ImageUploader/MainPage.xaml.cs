using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.ComponentModel;
using System.IO;
using Microsoft.WindowsAzure.Storage;
using Xamarin.Forms;

namespace ImageUploader
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        // You can get the proper names from portal.azure.com
        private const string AzureCloudResourceStorageName = "DELETED";
        private const string ContainerName = "DELETED";

        public MainPage()
        {
            InitializeComponent();
        }

        private async void PictureSelectButton_OnClicked(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("Error", "Not supported on your device", "Ok");
                return;
            }

            // Make photo smaller
            var mediaOptions = new PickMediaOptions
            {
                PhotoSize = PhotoSize.Medium
            };

            var selectedImageFile = await CrossMedia.Current.PickPhotoAsync(mediaOptions);

            if (selectedImageFile == null)
            {
                await DisplayAlert("Warning", "Please select the image first", "Ok");
                return;
            }

            SelectedImage.Source = ImageSource.FromStream(() => selectedImageFile.GetStream());

            UploadImage(selectedImageFile.GetStream());
        }

        private async void UploadImage(Stream stream)
        {
            var account = CloudStorageAccount.Parse(
                $"DefaultEndpointsProtocol=https;AccountName={AzureCloudResourceStorageName};AccountKey=Y71CHpv9iyRjJbsu3HWYiLf61NB8FDYWwnper9klnoMlYzlM+lo7V+oaRtyNF5bml6nIxlEyJKLc2AhiPuTMkw==;EndpointSuffix=core.windows.net");
            var client = account.CreateCloudBlobClient();
            var container = client.GetContainerReference(ContainerName);
            await container.CreateIfNotExistsAsync();

            var newImageId = Guid.NewGuid().ToString();
            var blockBlob = container.GetBlockBlobReference($"{newImageId}.jpg");

            await blockBlob.UploadFromStreamAsync(stream);

            var url = blockBlob.Uri.OriginalString;
        }
    }
}
