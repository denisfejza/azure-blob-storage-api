using AzureBlobStorage.API.Models;

namespace AzureBlobStorage.API.Services
{
    /// <summary>
    /// IBlobStorageService
    /// </summary>
    public interface IBlobStorageService
    {
        /// <summary>
        /// GetStorageAccountName
        /// </summary>
        /// <returns>string</returns>
        string GetStorageAccountName();

        /// <summary>
        /// CreateContainerAsync
        /// </summary>
        /// <param name="containerName">containerName</param>
        /// <returns></returns>
        /// <exception cref="ApplicationException"></exception>
        Task CreateContainerAsync(string containerName);

        /// <summary>
        /// GetContainersAsync
        /// </summary>
        /// <returns>IAsyncEnumerable[StorageContainerModel]</returns>
        IAsyncEnumerable<StorageContainerModel> GetContainersAsync();

        /// <summary>
        /// DeleteContainerAsync
        /// </summary>
        /// <param name="containerName">containerName</param>
        /// <returns></returns>
        /// <exception cref="ApplicationException"></exception>
        Task DeleteContainerAsync(string containerName);

        /// <summary>
        /// ListBlobsInContainerAsync
        /// </summary>
        /// <param name="containerName">containerName</param>
        /// <returns>IAsyncEnumerable[BlobInfoModel]</returns>
        IAsyncEnumerable<BlobInfoModel> ListBlobsInContainerAsync(string containerName);

        /// <summary>
        /// UploadBlobAsync
        /// </summary>
        /// <param name="containerName">containerName</param>
        /// <param name="blobName">blobName</param>
        /// <param name="contentType">contentType</param>
        /// <param name="content">content</param>
        Task UploadBlobAsync(string containerName, string blobName, string contentType, Stream content);

        /// <summary>
        /// GetBlobContentsAsync
        /// </summary>
        /// <param name="containerName">containerName</param>
        /// <param name="blobName">blobName</param>
        /// <returns>BlobModel</returns>
        Task<BlobModel> GetBlobContentsAsync(string containerName, string blobName);

        /// <summary>
        /// DeleteBlobAsync
        /// </summary>
        /// <param name="containerName">containerName</param>
        /// <param name="blobName">blobName</param>
        Task DeleteBlobAsync(string containerName, string blobName);
    }
}
