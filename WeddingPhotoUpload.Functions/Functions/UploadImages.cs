using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace WeddingPhotoUpload.Functions.Functions
{
    public class UploadImages
    {
        private readonly ILogger<UploadImages> _logger;

        public UploadImages(ILogger<UploadImages> logger)
        {
            _logger = logger;
        }

        [Function("UploadImages")]
        public async Task<IActionResult> RunAsync([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req)
        {
            Stream stream = new MemoryStream();
            await req.Body.CopyToAsync(stream);
            stream.Position = 0;
            var connectionstring = "DefaultEndpointsProtocol=https;AccountName=photouploadsa;AccountKey=zVSJQqOvqfZMNKXirl0a6I1JO50QYLJnoxQlnwEqUiSAVg53+O468sLuoiYAyj23b5yRU1FkLnRb+ASthZbchw==;EndpointSuffix=core.windows.net";
            BlobServiceClient blobServiceClient = new BlobServiceClient(connectionstring);
            BlobContainerClient blobContainerClient = blobServiceClient.GetBlobContainerClient("photo-upload");
            await blobContainerClient.CreateIfNotExistsAsync();
            BlobClient blobClent = blobContainerClient.GetBlobClient(Guid.NewGuid().ToString());
            BlobHttpHeaders httpheaders = new BlobHttpHeaders()
            {
                ContentType = req.ContentType,
            };
            var res = await blobClent.UploadAsync(stream, httpheaders);

            _logger.LogInformation("C# HTTP trigger function processed a request.");
            return new OkObjectResult("Welcome to Azure Functions!");
        }
    }
}
