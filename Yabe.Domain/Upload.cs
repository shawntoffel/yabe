using System.Collections.Generic;
using Microsoft.AspNetCore.Components.Forms;

namespace Yabe.Domain
{
    public class Upload
    {
        public string Path { get; set; }
        public string UploaderName { get; set; }
        public IBrowserFile File { get; set; }

        public Dictionary<string, string> GetMetadata()
        {
            return new Dictionary<string, string>()
            {
                {"Yabe-UploaderName", UploaderName}
            };
        }
    }
}
