using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using System;
using System.Threading.Tasks;
using System.Reflection;

namespace Barana.Auto
{
    public class UploadedFile
    {
        private static IAmazonS3 _s3Client;
        private static async Task UploadObjectFromFileAsync(IAmazonS3 client,string bucketName, string objectName, string filePath)
        {
            try
            {
                var putRequest = new PutObjectRequest
                {
                    BucketName = bucketName,
                    Key = objectName,
                    FilePath = filePath,
                    ContentType = "text/plain"
                };

                putRequest.Metadata.Add("x-amz-meta-title", "someTitle");

                PutObjectResponse response = await client.PutObjectAsync(putRequest);

                foreach (PropertyInfo prop in response.GetType().GetProperties())
                {
                    Console.WriteLine($"{prop.Name}: {prop.GetValue(response, null)}");
                }
            }
            catch (AmazonS3Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
        }
        public async Task<bool> Hello(string destinationFile, string fileName,string BUCKET_NAME)
        {
            try
            {
                var awsCredentials = new Amazon.Runtime.BasicAWSCredentials("2559ef37-8107-4b05-aa47-bddbf84fa4a4", "9ff57498db7d3f55d609eda8f40ec4c9296809283792bad682a5819bcfbcd081");
                var config = new AmazonS3Config { ServiceURL = "https://s3.ir-thr-at1.arvanstorage.com" };
                _s3Client = new AmazonS3Client(awsCredentials, config);

                await UploadObjectFromFileAsync(_s3Client, BUCKET_NAME, fileName, destinationFile);
                return true;
            }
            catch (Exception e)
            {
                string kik = e.Message;
                return (false);
            }
        }
    }

}

