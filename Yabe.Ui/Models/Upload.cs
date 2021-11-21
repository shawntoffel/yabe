using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Components.Forms;
using Yabe.Application.Configuration;

namespace Yabe.Ui.Models
{
    public class Upload
    {
        public string Path { get; set; }

        public List<IBrowserFile> Files { get; set; }

        public Upload()
        {
            Files = new List<IBrowserFile>();
        }

        public bool IsValidSize(YabeOptions options)
        {
            return Files.All(f => f.Size <= options.MaxUploadBytes);
        }
    }
}
