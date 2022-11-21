using AzureBlobStorage.API.Models;
using AzureBlobStorage.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace AzureBlobStorage.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AzureBlobStorageController : ControllerBase
    {
        private readonly IBlobStorageService _blobStorageService;

        public AzureBlobStorageController(IBlobStorageService blobStorageService)
        {
            _blobStorageService = blobStorageService;
        }

        [HttpGet]
        [Route("storage-name")]
        public IActionResult Get()
        {
            return Ok(_blobStorageService.GetStorageAccountName());
        }

        [HttpGet]
        [Route("containers")]
        public async Task<IEnumerable<StorageContainerModel>> GetContainers()
        {
            List<StorageContainerModel> containers = new List<StorageContainerModel>();

            await foreach (var container in _blobStorageService.GetContainersAsync())
            {
                containers.Add(container);
            }

            return containers;
        }

        [HttpPost]
        [Route("create-container/{containerName}")]
        public async Task<IActionResult> CreateContainer(string containerName)
        {
            await _blobStorageService.CreateContainerAsync(containerName);

            return Ok();
        }

        [HttpDelete]
        [Route("delete-container/{containerName}")]
        public async Task<IActionResult> DeleteContainer(string containerName)
        {
            await _blobStorageService.DeleteContainerAsync(containerName);
            return Ok();
        }

        [HttpGet]
        [Route("{containerName}/blobs")]
        public async Task<IEnumerable<BlobInfoModel>> ListBlobsInContainer(string containerName)
        {
            List<BlobInfoModel> blobs = new List<BlobInfoModel>();

            await foreach (var blob in _blobStorageService.ListBlobsInContainerAsync(containerName))
            {
                blobs.Add(blob);
            }

            return blobs;
        }

        [HttpPost]
        [Route("{containerName}/upload")]
        public async Task<IActionResult> UploadAsync(string containerName, IFormFile file)
        {
            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                stream.Position = 0;
                await _blobStorageService.UploadBlobAsync(containerName, file.FileName, file.ContentType, stream);
            }

            return Ok();
        }

        [HttpGet]
        [Route("{containerName}/blob/{blobName}")]
        public async Task<BlobModel> GetBlob(string containerName, string blobName)
        {
            return await _blobStorageService.GetBlobContentsAsync(containerName, blobName);
        }

        [HttpDelete]
        [Route("{containerName}/delete/{blob}")]
        public async Task<IActionResult> DeleteBlob(string containerName, string blobName)
        {
            await _blobStorageService.DeleteBlobAsync(containerName, blobName); ;
            return Ok();
        }

    }
}
