using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs.Specialized;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Yabe.Application.Configuration;

namespace Yabe.Application.Handlers.BlobHandlers.UploadBlobs
{
    public class UploadBlobsHandler : IRequestHandler<UploadBlobsRequest, UploadBlobsReply>
    {
        private readonly ILogger<UploadBlobsHandler> _logger;
        private readonly BlobServiceClient _blobServiceClient;
        private readonly YabeOptions _options;

        public UploadBlobsHandler(ILogger<UploadBlobsHandler> logger, BlobServiceClient blobServiceClient, IOptions<YabeOptions> options)
        {
            _logger = logger;
            _blobServiceClient = blobServiceClient;
            _options = options.Value;
        }

        public async Task<UploadBlobsReply> Handle(UploadBlobsRequest request, CancellationToken cancellationToken)
        {
            if (request?.Uploads == null || !request.Uploads.Any())
                return new UploadBlobsReply();

            var uploads = request.Uploads
                .Where(u => u.File != null)
                .Where(u => !string.IsNullOrEmpty(u.File?.Name))
                .ToList();

            _logger.LogInformation("Starting upload of {Count} file(s). User: '{User}'", uploads.Count, request.User?.Identity?.Name);

            var containerClient = _blobServiceClient
                .GetBlobContainerClient(_options.BlobContainerName);

            foreach (var upload in uploads)
            {
                var blockBlobClient = containerClient
                    .GetBlockBlobClient(upload.Path + "/" + upload.File.Name);

                var stream = upload.File.OpenReadStream(_options.MaxUploadBytes);

                var headers = new BlobHttpHeaders()
                {
                    ContentType = upload.File.ContentType
                };

                await blockBlobClient
                    .UploadAsync(
                        stream, 
                        headers,
                        upload.GetMetadata(),
                        cancellationToken: cancellationToken
                    );
            }

            _logger.LogInformation("Finished uploading {Count} file(s). User: '{User}'", uploads.Count, request.User?.Identity?.Name);

            return new UploadBlobsReply();
        }
    }
}
