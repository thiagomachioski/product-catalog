using System.IO;
using Azure.Storage.Blobs;

namespace Products.Catalog.Repository.BlobStorage
{
    public class BlobStorageProvider : IBlobStorageProvider
    {
        private const string ConnectionString = "UseDevelopmentStorage=true";
        private const string Container = "product-catalog";
        
        public string Upload(Stream stream, string fileName)
        {
            var blobClient = GetContainerClient().GetBlobClient(fileName);
            blobClient.Upload(stream);
            return blobClient.Uri.AbsoluteUri;
        }
        
        private BlobContainerClient GetContainerClient()
        {
            var blobServiceClient = new BlobServiceClient(ConnectionString);
            var containerClient = blobServiceClient.GetBlobContainerClient(Container); 
            containerClient.CreateIfNotExists();

            return containerClient;
        }
    }
}