using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Yabe.Application.Authorization;
using Yabe.Application.Handlers.BlobHandlers.DeleteBlob;
using Yabe.Application.Handlers.BlobHandlers.ListBlobs;
using Yabe.Application.Handlers.BlobHandlers.UploadBlobs;
using Yabe.Domain;

namespace Yabe.Ui.Managers
{
    public class BlobManager : IBlobManager
    {
        private ILogger<IBlobManager> _logger;
        private readonly IMediator _mediator;
        private readonly IAuthorizationManager _authManager;

        public BlobManager(ILogger<IBlobManager> logger, IMediator mediator, IAuthorizationManager authManager)
        {
            _logger = logger;
            _mediator = mediator;
            _authManager = authManager;
        }

        public async Task<BlobDirectory> ListBlobs(string directory)
        {
            try
            {
                var req = new ListBlobsRequest()
                {
                    Path = directory
                };

                var resp = await _mediator.Send(req);

                return resp.BlobDirectories;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to list blobs");
                throw;
            }
        }

        public async Task Delete(string name)
        {
            try
            {
                var user = await _authManager.GetUser();
                var req = new DeleteBlobRequest()
                {
                    Name = name,
                    User = user
                };

                await _mediator.Send(req);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to delete blob");
                throw;
            }
        }

        public async Task Upload(Models.Upload upload)
        {
            try
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
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to upload blobs");
                throw;
            }
        }
    }
}
