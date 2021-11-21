using System;
using System.Collections.Generic;
using System.IO;

namespace Yabe.Domain
{
    public class Blob
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public long Size { get; set; }
        public DateTimeOffset? LastModified { get; set; }
        public DateTimeOffset? LastAccessed { get; set; }
        public IDictionary<string, string> Metadata { get; set; }

        private string GetMetadataValue(string key)
        {
            if (Metadata == null)
                return string.Empty;

            return Metadata.TryGetValue(key, out var value) ? value : string.Empty;
        }

        public string BaseName()
        {
            return Path.GetFileName(Name);
        }
    }
}
