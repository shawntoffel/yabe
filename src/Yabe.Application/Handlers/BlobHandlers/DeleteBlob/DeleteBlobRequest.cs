using MediatR;

namespace Yabe.Application.Handlers.BlobHandlers.DeleteBlob
{
    public class DeleteBlobRequest : AuthorizedRequest, IRequest<DeleteBlobReply>
    {
        public string Name { get; set; }
    }
}
