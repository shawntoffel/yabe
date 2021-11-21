using FluentValidation;
using Microsoft.Extensions.Options;
using Yabe.Application.Configuration;

namespace Yabe.Application.Handlers.BlobHandlers.DeleteBlob
{
    public class DeleteBlobRequestValidator : AbstractValidator<DeleteBlobRequest>
    {
        public DeleteBlobRequestValidator(IOptions<YabeOptions> options)
        {
            RuleFor(x => x.User)
                .NotNull()
                .WithMessage("Unauthorized")
                .Must(x => x.IsInRole(options.Value.EditorRole))
                .WithMessage("Unauthorized");

            RuleFor(x => x.Name)
                .NotEmpty();
        }
    }
}
