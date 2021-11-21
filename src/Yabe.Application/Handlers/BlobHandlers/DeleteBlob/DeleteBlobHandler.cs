using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Specialized;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Threading;
using System.Threading.Tasks;
using Yabe.Application.Configuration;

namespace Yabe.Application.Handlers.BlobHandlers.DeleteBlob
{
    public class DeleteBlobHandler : IRequestHandler<DeleteBlobRequest, DeleteBlobReply>
    {
        private readonly ILogger<DeleteBlobHandler> _logger;
        private readonly BlobServiceClient _blobServiceClient;
        private readonly YabeOptions _options;

        public DeleteBlobHandler(ILogger<DeleteBlobHandler> logger, BlobServiceClient blobServiceClient, IOptions<YabeOptions> options)
        {
            _logger = logger;
            _blobServiceClient = blobServiceClient;
            _options = options.Value;
        }

        public async Task<DeleteBlobReply> Handle(DeleteBlobRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Received request to delete blob '{Name}'. User: '{User}'", request.Name, request.User?.Identity?.Name);
                
            var blockBlobClient = _blobServiceClient
                .GetBlobContainerClient(_options.BlobContainerName)
                .GetBlockBlobClient(request.Name);

            var result = await blockBlobClient.DeleteIfExistsAsync(cancellationToken: cancellationToken);

            var message = result?.Value == true ? "Blob has been deleted." : "Delete skipped. Blob did not exist.";
            _logger.LogInformation(message + " Blob: '{Blob}'. User: '{User}'", request.Name, request.User?.Identity?.Name);

            return new DeleteBlobReply()
            {
                Success = result?.Value ?? false
            };
        }
    }
}
