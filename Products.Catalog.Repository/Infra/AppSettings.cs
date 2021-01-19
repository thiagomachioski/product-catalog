namespace Products.Catalog.Repository.Infra
{
    public class AppSettings
    {
        public string ConnectionString { get; set; }
        public BlobStorage BlobStorage { get; set; }
    }

}
