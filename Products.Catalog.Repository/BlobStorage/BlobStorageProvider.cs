using System;
using System.IO;
using Azure.Storage.Blobs;
using Products.Catalog.Repository.Infra;

namespace Products.Catalog.Repository.BlobStorage
{
    public class BlobStorageProvider : IBlobStorageProvider
    {
        private readonly AppSettings _appSettings;

        public BlobStorageProvider(AppSettings appSettings)
        {
            _appSettings = appSettings;
        }
        public string Upload(Stream stream, string fileName)
        {
            var blobClient = GetContainerClient().GetBlobClient($"{Guid.NewGuid()}{fileName}");
            blobClient.Upload(stream);
            return blobClient.Uri.AbsoluteUri;
        }
        
        private BlobContainerClient GetContainerClient()
        {
            var blobServiceClient = new BlobServiceClient(_appSettings.BlobStorage.ConnectionString);
            var containerClient = blobServiceClient.GetBlobContainerClient(_appSettings.BlobStorage.ContainerName); 
            containerClient.CreateIfNotExists();

            return containerClient;
        }
    }
}