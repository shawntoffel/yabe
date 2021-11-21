using MediatR;

namespace Yabe.Application.Handlers.BlobHandlers.ListBlobs
{
    public class ListBlobsRequest : IRequest<ListBlobsReply>
    {
        public string Path { get; set; }
    }
}
