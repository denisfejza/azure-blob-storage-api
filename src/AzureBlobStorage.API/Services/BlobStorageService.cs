using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using AzureBlobStorage.API.Infrastructure.Exceptions;
using AzureBlobStorage.API.Models;

namespace AzureBlobStorage.API.Services
{
    public class BlobStorageService : IBlobStorageService
    {
        private readonly BlobServiceClient _blobServiceClient;

        public BlobStorageService(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
        }

        public string GetStorageAccountName()
        {
            return _blobServiceClient.AccountName;
        }

        public async Task CreateContainerAsync(string containerName)
        {
            BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(containerName);

            if (await containerClient.ExistsAsync())
                throw new ContainerAlreadyExistsException(containerName);

            await containerClient.CreateAsync();
        }

        public async IAsyncEnumerable<StorageContainerModel> GetContainersAsync()
        {
            AsyncPageable<BlobContainerItem> containers = _blobServiceClient.GetBlobContainersAsync();

            await foreach (var item in containers)
            {
                yield return new StorageContainerModel() { Name = item.Name };
            }
        }


        public async Task DeleteContainerAsync(string containerName)
        {
            BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(containerName);

            if (!await containerClient.ExistsAsync())
                throw new ContainerDoesNotExistException(containerName);

            await containerClient.DeleteAsync();
        }

        public async IAsyncEnumerable<BlobInfoModel> ListBlobsInContainerAsync(string containerName)
        {
            BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(containerName);

            if (!await containerClient.ExistsAsync())
                throw new ContainerDoesNotExistException(containerName);

            AsyncPageable<BlobItem> blobs = containerClient.GetBlobsAsync();

            await foreach (var blob in blobs)
            {
                BlobInfoModel model = new BlobInfoModel()
                {
                    Name = blob.Name,
                    Tags = blob.Tags,
                    ContentEncoding = blob.Properties.ContentEncoding,
                    ContentType = blob.Properties.ContentType,
                    Size = blob.Properties.ContentLength,
                    CreatedOn = blob.Properties.CreatedOn,
                    AccessTier = blob.Properties.AccessTier?.ToString(),
                    BlobType = blob.Properties.BlobType?.ToString()
                };

                yield return model;
            }
        }

        public async Task UploadBlobAsync(string containerName, string blobName, string contentType, Stream content)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            if (!await containerClient.ExistsAsync())
                throw new ContainerDoesNotExistException(containerName);

            var blobClient = containerClient.GetBlobClient(blobName);
            var options = new BlobUploadOptions() { HttpHeaders = new BlobHttpHeaders() { ContentType = contentType } };
            var response = await blobClient.UploadAsync(content, options);
        }

        public async Task<BlobModel> GetBlobContentsAsync(string containerName, string blobName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            if (!await containerClient.ExistsAsync())
                throw new ContainerDoesNotExistException(containerName);

            var blobClient = containerClient.GetBlobClient(blobName);
            if (!await blobClient.ExistsAsync())
                throw new ApplicationException($"Unable to delete blob {blobName} in container '{containerName}' as no blob with this name exists in this container");

            return new BlobModel()
            {
                Name = blobName,
                ContentType = blobClient.GetProperties().Value.ContentType,
                Content = await blobClient.OpenReadAsync()
            };
        }


        public async Task DeleteBlobAsync(string containerName, string blobName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            if (!await containerClient.ExistsAsync())
                throw new ContainerDoesNotExistException(containerName);

            var blobClient = containerClient.GetBlobClient(blobName);
            if (!await blobClient.ExistsAsync())
                throw new ApplicationException($"Unable to delete blob {blobName} in container '{containerName}' as no blob with this name exists in this container");

            await blobClient.DeleteAsync();
        }
    }
}
