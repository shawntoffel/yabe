using Azure.Storage.Blobs.Models;
using Yabe.Application.Configuration;
using Yabe.Domain;

namespace Yabe.Application.Extensions
{
    public static class MappingExtensions
    {
        public static Blob ToBlob(this BlobHierarchyItem blobHierarchyItem, YabeOptions options)
        {
            var blob = blobHierarchyItem.Blob;
            var properties = blob?.Properties;

            return new Blob()
            {
                Name = blob?.Name,
                Url = $"{options.BlobBaseUrl}/{blob?.Name}",
                Size = properties?.ContentLength ?? 0,
                LastModified = properties?.LastModified.InZone(options.TimeZoneId),
                LastAccessed = properties?.LastAccessedOn.InZone(options.TimeZoneId),
                Metadata = blob?.Metadata
            };
        }
    }
}
