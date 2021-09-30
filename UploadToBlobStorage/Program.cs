using Azure.Storage.Blobs;
using System;
using System.IO;
using System.Threading.Tasks;

namespace UploadToBlobStorage
{
    class Program
    {
        static async Task Main(string[] args)
        {
            BlobServiceClient blobServiceClient = new BlobServiceClient("teste123123123");

            //Conecta ao container, já criado no portal
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient("images");

            string localPath = "./data/";
            string fileName = "blob_storage_file" + Guid.NewGuid().ToString() + ".txt";
            string localFilePath = Path.Combine(localPath, fileName);

            if (!Directory.Exists(localPath))
                Directory.CreateDirectory(localPath);

            File.WriteAllText(localFilePath, "Hello world do blob storage");

            BlobClient client = containerClient.GetBlobClient(fileName);

            client.Upload(localFilePath);

        
            var localPathToSave = "./data/download/";

            if (!Directory.Exists(localPathToSave))
                Directory.CreateDirectory(localPathToSave);

            var blobClient = containerClient.GetBlobClient("https://storage.blob.core.windows.net/images/" + fileName);

            await blobClient.DownloadToAsync(localPathToSave + fileName);          

        }
    }
}
