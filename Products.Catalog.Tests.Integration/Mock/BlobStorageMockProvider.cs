using System.IO;
using Products.Catalog.Repository.BlobStorage;

namespace Products.Catalog.Tests.Integration.Mock
{
    public class BlobStorageMockProvider : IBlobStorageProvider
    {
        public string Upload(Stream stream, string fileName) => "URL";
    }
}