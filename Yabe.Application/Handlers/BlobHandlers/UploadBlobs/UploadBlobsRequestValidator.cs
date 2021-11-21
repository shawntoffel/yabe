using FluentValidation;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Options;
using Yabe.Application.Configuration;
using Yabe.Application.Extensions;
using Yabe.Domain;

namespace Yabe.Application.Handlers.BlobHandlers.UploadBlobs
{
    public class UploadBlobsRequestValidator : AbstractValidator<UploadBlobsRequest>
    {
        public UploadBlobsRequestValidator(IOptions<YabeOptions> options)
        {
            RuleFor(x => x.User)
                .NotNull()
                .WithMessage("Unauthorized")
                .Must(x => x.IsInRole(options.Value.EditorRole))
                .WithMessage("Unauthorized");

            RuleForEach(x => x.Uploads)
                .SetValidator(new UploadValidator(options));
        }
    }

    public class UploadValidator : AbstractValidator<Upload>
    {
        public UploadValidator(IOptions<YabeOptions> options)
        {
            RuleFor(x => x.File)
                .SetValidator(new BrowserFileValidator(options));
        }
    }

    public class BrowserFileValidator : AbstractValidator<IBrowserFile>
    {
        public BrowserFileValidator(IOptions<YabeOptions> options)
        {
            RuleFor(x => x.Size)
                .LessThanOrEqualTo(options.Value.MaxUploadBytes)
                .WithMessage(x => $"File '{x.Name}' is too large. The maximum allowed file size is {options.Value.MaxUploadBytes.ToFriendlyBytesString()}.");
        }
    }
}
