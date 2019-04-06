namespace Awaoa.Core.Configuration
{
    public sealed class AzureBlobStorageOptions
    {
        public string ConnectionString { get; set; }

        public string ContainerName { get; set; }

        public int SharedAccessExpiryTime { get; set; }

        public AzureBlobStorageOptions()
        {
            SharedAccessExpiryTime = 30/* 30 minutes*/ * 60 /*60 seconds per minutes*/;

            ContainerName = AwaoaDefaults.Name;
        }
    }
}