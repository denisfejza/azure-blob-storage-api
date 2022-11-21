using AzureBlobStorage.API.Models;
using AzureBlobStorage.API.Services;
using Moq;
namespace AzureBlobStorage.Test
{
    public class BlobStorageServiceTests
    {
        private readonly Mock<IBlobStorageService> _mock;

        public BlobStorageServiceTests()
        {
            _mock = new Mock<IBlobStorageService>();
        }

        public static async IAsyncEnumerable<StorageContainerModel> GetTestValues()
        {
            yield return new StorageContainerModel { Name = "test" };
        }

        [Fact]
        public void CreateContainer()
        {
            _mock.Setup(azureBlobStorage => azureBlobStorage.CreateContainerAsync(It.IsAny<string>()))
               .Returns(async () => await Task.FromResult("test"));

            _mock.Setup(azureBlobStorage => azureBlobStorage.GetContainersAsync())
               .Returns(GetTestValues);
        }

    }

}