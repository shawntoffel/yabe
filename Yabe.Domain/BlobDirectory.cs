using System.Collections.Generic;

namespace Yabe.Domain
{
    public class BlobDirectory
    {
        public string Path { get; set; }
        public List<Blob> Blobs { get; set; }
        public List<string> SubDirectories { get; set; }

        public BlobDirectory()
        {
            Blobs = new List<Blob>();
            SubDirectories = new List<string>();
        }
    }
}
