using MediatR;
using System.Collections.Generic;
using Yabe.Domain;

namespace Yabe.Application.Handlers.BlobHandlers.UploadBlobs
{
    public class UploadBlobsRequest : AuthorizedRequest, IRequest<UploadBlobsReply>
    {
        public List<Upload> Uploads { get; set; }

        public UploadBlobsRequest()
        {
            Uploads = new List<Upload>();
        }
    }
}
