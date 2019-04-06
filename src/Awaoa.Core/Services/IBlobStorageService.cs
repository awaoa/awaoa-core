using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Awaoa.Core.Services
{
    public interface IBlobStorageService
    {
        Task<string> UploadAsync(string source, string target, string contentType);

        Task<string> UploadAsync(byte[] source, string target, string contentType);

        Task<string> UploadAsync(Stream source, string target, string contentType);

        Task<ICollection<string>> ListBlobFilesAsync(string target);

        Task<string> GetFileUriWithSharedAccessSignatureAsync(string fileName, int duration = 600, params string[] folders);
    }
}