using System.IO;
using System.Net;

namespace Project.Seed.Services
{
    public class AmazonUploader
    {
        string imageDirectory = @"C:\Users\Jason\Pictures\projectImages";

        public void SendFileToS3(string sourceFilePath, string bucketName, string subDirectoryInBucket, string fileNameInS3)
        {
            var path = string.IsNullOrEmpty(subDirectoryInBucket) ? bucketName : bucketName + subDirectoryInBucket;

            using (var client = new Amazon.S3.AmazonS3Client(Amazon.RegionEndpoint.USWest2))
            {
                using (var transferUtility = new Amazon.S3.Transfer.TransferUtility(client))
                {
                    var transferRequest = new Amazon.S3.Transfer.TransferUtilityUploadRequest
                    {
                        CannedACL = Amazon.S3.S3CannedACL.PublicRead,
                        InputStream = GetHttpStream(sourceFilePath, fileNameInS3),
                        BucketName = path,
                        Key = fileNameInS3,
                        AutoCloseStream = true
                    };
                    transferUtility.Upload(transferRequest);
                }
            }
        }

        private Stream GetHttpStream(string sourceFilePath, string fileName)
        {
            var request = WebRequest.Create(sourceFilePath);
            using (var response = request.GetResponse())
            {
                var stream = new MemoryStream();
                response.GetResponseStream().CopyTo(stream);
                return stream;
            }
        }
    }
}
