using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Diagnostics;

namespace W15_SyncAsyncImageBillboard
{
    public partial class BillboardForm : Form
    {
        CloudStorageAccount storageAccount;
        CloudBlobClient blobClient;
        CloudBlobContainer blobContainer;

        const string blobContainerUrl = "https://spdvistorage.blob.core.windows.net/syncasync/";
        string[] blobNames =
        {
            "animals-animal-pet-cat-cat-sleep-orange-cat-1594715-pxhere.com.jpg",
            "mammal-pet-pets-dog-doggy-sad-dog-1594710-pxhere.com.jpg",
            "nature-beautiful-close-up-free-images-focus-macro-1448319-pxhere.com.jpg",
            "wood-kitten-cat-mammal-black-fauna-861796-pxhere.com.jpg"
        };

        public BillboardForm()
        {
            InitializeComponent();
            InitializeBlobStorage();
        }

        private void InitializeBlobStorage()
        {
            try
            {
                // TODO: Refactor to store configuration string in App.config or better in Azure Key Vault
                storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=spdvistorage;AccountKey=gvceCorkm2MJlxNWsnSpfAJvEPGen+fN6aYkrYlS47O5prrnthfjnhz+ddEjUb/z8dlmHf4pf2QYXXCmLrGLsQ==;EndpointSuffix=core.windows.net");
                blobClient = storageAccount.CreateCloudBlobClient();
                blobContainer = blobClient.GetContainerReference("syncasync");
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void LoadImagesSyncButton_Click(object sender, EventArgs e)
        {
            TextLabel.Text += $"\nDownloading image 1... ";
            Stopwatch watch = Stopwatch.StartNew();
            pictureBox1.Image = LoadImageFromUri(blobNames[0]);
            watch.Stop();
            TextLabel.Text += "Finished in " + watch.ElapsedMilliseconds + " ms";

            //await Task.Delay(1000);  // Wait 1s to give time to update the UI

            //TextLabel.Text += $"\nDownloading image 2... ";
            //watch.Reset();
            //watch.Start();
            //pictureBox2.Image = LoadImageFromUri(blobNames[1]);
            //watch.Stop();
            //TextLabel.Text += "Finished in " + watch.ElapsedMilliseconds + " ms";

            //await Task.Delay(1000);  // Wait 1s to give time to update the UI

            //TextLabel.Text += $"\nDownloading image 3... ";
            //watch.Reset();
            //watch.Start();
            //pictureBox3.Image = LoadImageFromUri(blobNames[2]);
            //watch.Stop();
            //TextLabel.Text += "Finished in " + watch.ElapsedMilliseconds + " ms";
            
            //await Task.Delay(1000);  // Wait 1s to give time to update the UI

            //TextLabel.Text += $"\nDownloading image 4... ";
            //watch.Reset();
            //watch.Start();
            //pictureBox4.Image = LoadImageFromUri(blobNames[3]);
            //watch.Stop();
            //TextLabel.Text += "Finished in " + watch.ElapsedMilliseconds + " ms";
        }

        private Image LoadImageFromUri(string imageFileName)
        {
            MemoryStream stream = new MemoryStream();
            CloudBlockBlob cloudBlockBlob = new CloudBlockBlob(new Uri(blobContainerUrl + imageFileName));
            cloudBlockBlob.DownloadToStream(stream);
            TextLabel.Text += $"({cloudBlockBlob.Properties.Length / 1024 / 1024} MB) ";
            Image image = Image.FromStream(stream);
            stream.Dispose();
            return image;
        }

    }
}
