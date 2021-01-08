using System;
using System.IO;

namespace Products.Catalog.UI.Extensions
{
    public static class Base64Extensions 
    {
        public static string ToBase64(this Stream stream)
        {
            using var memoryStream = new MemoryStream();
            stream.CopyTo(memoryStream);
            return memoryStream.ToBase64();
        }

        public static string ToBase64(this MemoryStream stream) =>
            Convert.ToBase64String(stream.ToArray());

        public static string ToBase64(this byte[] bytes) =>
            Convert.ToBase64String(bytes);
    }
}
