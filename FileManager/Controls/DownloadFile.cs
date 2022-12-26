using Amazon.S3;
using Amazon.S3.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Barana.Auto
{
    public class DownloadFile
    {
        public static byte[] GetBytesFromStream(Stream fileContentStream)
        {
            using (var memoryStreamHandler = new MemoryStream())
            {
                fileContentStream.CopyTo(memoryStreamHandler);
                return memoryStreamHandler.ToArray();
            }
        }
        public async Task<byte[]> Download(string serverUri, string bucketName)
        {

            try
            {
                var awsCredentials = new Amazon.Runtime.BasicAWSCredentials("2559ef37-8107-4b05-aa47-bddbf84fa4a4", "9ff57498db7d3f55d609eda8f40ec4c9296809283792bad682a5819bcfbcd081");
                var config = new AmazonS3Config { ServiceURL = "https://s3.ir-thr-at1.arvanstorage.com" };
                IAmazonS3 _s3Client = new AmazonS3Client(awsCredentials, config);
                string responseBody = string.Empty;
                try
                {
                    GetObjectRequest request = new GetObjectRequest
                    {
                        BucketName = bucketName,
                        Key = serverUri,
                    };
                    GetObjectResponse response = await _s3Client.GetObjectAsync(request);
                    Stream responseStream = response.ResponseStream;
                    byte[] resize = GetBytesFromStream(responseStream);
                    return resize;

                }
                catch (AmazonS3Exception )
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                string lo = ex.Message;
                return null;
            }
        }
    }
}
