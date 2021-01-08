using System.IO;

namespace Products.Catalog.Repository.BlobStorage
{
    public interface IBlobStorageProvider
    {
        string Upload(Stream stream, string fileName);
    }
}