using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Awaoa.Core.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Awaoa.Core.Services
{
    public sealed class AzureBlobService : IBlobStorageService
    {
        private readonly BlobContainerPermissions DefaultBlobContainerPublicAccessPermissions;
        private readonly CloudStorageAccount StorageAccount;
        private readonly CloudBlobClient BlobClient;
        private readonly string DefaultBlobContainerName;

        public AzureBlobService(IOptionsMonitor<AzureBlobStorageOptions> optionMonitor)
        {
            var options = optionMonitor.CurrentValue;
            DefaultBlobContainerName = options.ContainerName;
            StorageAccount = CloudStorageAccount.Parse(options.ConnectionString);
            BlobClient = StorageAccount.CreateCloudBlobClient();
            DefaultBlobContainerPublicAccessPermissions = new BlobContainerPermissions
            {
                PublicAccess = BlobContainerPublicAccessType.Blob
            };
        }

        public async Task<string> UploadAsync(string source, string target, string contentType)
        {
            var fs = new FileStream(source, FileMode.Open, FileAccess.Read);
            return await UploadAsync(fs, target, contentType);
        }

        /// <summary>
        /// upload file to specified path on Azure and return remote access url of that file.
        /// </summary>
        /// <param name="source">byte array of that file</param>
        /// <param name="target"></param>
        /// <returns></returns>
        public async Task<string> UploadAsync(byte[] source, string target, string contentType)
        {
            var ms = new MemoryStream(source);
            return await UploadAsync(ms, target, contentType);
        }

        /// <summary>
        /// Upload the file to the Azure blob storage directly
        /// </summary>
        /// <param name="source">The source file stream</param>
        /// <param name="target">The garget file path</param>
        /// <returns></returns>
        public async Task<string> UploadAsync(Stream source, string target, string contentType)
        {
            string fileUri;
            try
            {
                var container = await GetBlobContainerAsync(DefaultBlobContainerPublicAccessPermissions);
                CloudBlockBlob fileBlob = container.GetBlockBlobReference(target);
                fileBlob.Properties.ContentType = contentType;

                await fileBlob.UploadFromStreamAsync(source);
                fileUri = fileBlob.Uri.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception($"Unable to upload file {target} to blob storage. Error:: ${ex.ToString()}");
            }
            return fileUri;
        }

        public async Task<ICollection<string>> ListBlobFilesAsync(string target)
        {
            var fileList = new List<string>();

            var container = await GetBlobContainerAsync(DefaultBlobContainerPublicAccessPermissions);
            var blobs = await container.ListBlobsSegmentedAsync(target, null);

            foreach (var blob in blobs.Results)
            {
                var fileName = blob.GetType().GetProperty("Name").GetValue(blob).ToString();
                fileList.Add(fileName);
            }

            return fileList;
        }

        /// <param name="container"></param>
        /// <param name="fileName"></param>
        /// <param name="duration"></param>
        /// <param name="folders"></param>
        /// <exception cref="ArgumentOutOfRangeException">throw when the duration is equal or less than 0</exception>
        /// <returns></returns>
        public async Task<string> GetFileUriWithSharedAccessSignatureAsync(string fileName, int duration = 600, params string[] folders)
        {
            var pathSeparator = "/";
            var fullFileName = string.Join(pathSeparator, folders);

            if (!string.IsNullOrEmpty(fullFileName) && !fullFileName.EndsWith("/"))
            {
                fullFileName += pathSeparator;
            }

            fullFileName += fileName;

            var blobContainer = await GetBlobContainerAsync(DefaultBlobContainerPublicAccessPermissions);

            CloudBlockBlob fileBlob = blobContainer.GetBlockBlobReference(fullFileName);

            if (duration <= 0)
            {
                var paramName = nameof(duration);
                throw new ArgumentOutOfRangeException(paramName, duration, $"The {paramName} must be bigger than 0.");
            }

            var SAS = fileBlob.GetSharedAccessSignature(new SharedAccessBlobPolicy
            {
                SharedAccessStartTime = DateTime.Now,
                SharedAccessExpiryTime = DateTime.Now.AddMinutes(duration),
                Permissions = SharedAccessBlobPermissions.Read,
            });

            return $"{fileBlob.Uri}?{SAS}";
        }

        private async Task<CloudBlobContainer> GetBlobContainerAsync(BlobContainerPermissions blobContainerPermissions)
        {
            CloudBlobContainer blobContainer = BlobClient.GetContainerReference(DefaultBlobContainerName);
            await blobContainer.CreateIfNotExistsAsync();
            await blobContainer.SetPermissionsAsync(blobContainerPermissions);
            return blobContainer;
        }
    }
}