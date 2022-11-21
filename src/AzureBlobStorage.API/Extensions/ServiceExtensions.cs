using Azure.Storage.Blobs;
using AzureBlobStorage.API.Services;

namespace AzureBlobStorage.API.Extensions
{
    public static class ServiceExtensions
    {
        public static void RegisterAppServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(new BlobServiceClient(configuration.GetSection("AzureBlobStorage")?.Value?.ToString()));
            services.AddScoped<IBlobStorageService, BlobStorageService>();
        }
    }
}
