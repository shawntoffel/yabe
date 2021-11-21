using System.Threading;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Yabe.Application.Configuration;
using Yabe.Application.Extensions;
using Yabe.Domain;

namespace Yabe.Application.Handlers.BlobHandlers.ListBlobs
{
    public class ListBlobsHandler : IRequestHandler<ListBlobsRequest, ListBlobsReply>
    {
        private readonly ILogger<ListBlobsHandler> _logger;
        private readonly BlobServiceClient _blobServiceClient;
        private readonly YabeOptions _options;

        public ListBlobsHandler(ILogger<ListBlobsHandler> logger, BlobServiceClient blobServiceClient, IOptions<YabeOptions> options)
        {
            _logger = logger;
            _blobServiceClient = blobServiceClient;
            _options = options.Value;
        }

        public async Task<ListBlobsReply> Handle(ListBlobsRequest request, CancellationToken cancellationToken)
        {
            _logger.LogTrace("Listing blobs in directory: '{Directory}'", request.Path);

            var blobDirectory = new BlobDirectory()
            {
                Path = request.Path
            };

            var blobHierarchyPages = _blobServiceClient
                .GetBlobContainerClient(_options.BlobContainerName)
                .GetBlobsByHierarchyAsync(prefix: request.Path, delimiter: "/", traits: BlobTraits.Metadata);

            await foreach (var hierarchyItem in blobHierarchyPages)
            {
                if (hierarchyItem.IsPrefix)
                {
                    blobDirectory.SubDirectories.Add(hierarchyItem.Prefix);
                    continue;
                }

                var blob = hierarchyItem.ToBlob(_options);

                blobDirectory.Blobs.Add(blob);
            }

            return new ListBlobsReply()
            {
                BlobDirectories = blobDirectory
            };
        }
    }
}
