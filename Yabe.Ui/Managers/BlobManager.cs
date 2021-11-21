using System.Threading.Tasks;
using MediatR;
using Yabe.Application.Authorization;
using Yabe.Application.Handlers.BlobHandlers.DeleteBlob;
using Yabe.Application.Handlers.BlobHandlers.ListBlobs;
using Yabe.Application.Handlers.BlobHandlers.UploadBlobs;
using Yabe.Domain;

namespace Yabe.Ui.Managers
{
    public class BlobManager : IBlobManager
    {
        private readonly IMediator _mediator;
        private readonly IAuthorizationManager _authManager;

        public BlobManager(IMediator mediator, IAuthorizationManager authManager)
        {
            _mediator = mediator;
            _authManager = authManager;
        }

        public async Task<BlobDirectory> ListBlobs(string directory)
        {
            var req = new ListBlobsRequest()
            {
                Path = directory
            };

            var resp = await _mediator.Send(req);

            return resp.BlobDirectories;
        }

        public async Task Delete(string name)
        {
            var user = await _authManager.GetUser();
            var req = new DeleteBlobRequest()
            {
                Name = name,
                User = user
            };

            await _mediator.Send(req);
        }

        public async Task Upload(Models.Upload upload)
        {
            var user = await _authManager.GetUser();
            var req = new UploadBlobsRequest()
            {
                User = user
            };

            foreach (var file in upload.Files)
            {
                req.Uploads.Add(new Upload()
                {
                    File = file,
                    UploaderName = user?.Identity?.Name,
                    Path = upload.Path
                });
            }

            await _mediator.Send(req);
        }
    }
}
