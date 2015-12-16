using Amazon.S3;
using Amazon.S3.Model;
using System;


namespace AWSS3FileService
{
    class Program
    {
        static string bucketName = @"4592-dev-s3/DLUpload"; // " * ** bucket name ***";
        static string keyName = "Test03.txt"; // " * ** key name when object is created ***";
        static string filePath = @"C:\Narayanan\Project_PricingandCosting\MicrArchServices\Test03.txt"; //"*** absolute path to a sample file to upload ***";

        static string accessKeyID = "AKIAJD5RAV54F5ACYTJQ";
        static string secretAccessKeyID = "6H60dxmf32Jpc2en1xUeCjfNLG9EbDcNybbx+23Z";

        //old
        //static string accessKeyID = "AKIAJ5EZIBYMS2SPS3WA";
        //static string secretAccessKeyID = "5Bel3Rr6wUmOTvmCFDHR48BQyYFsSp7NN+tjS7x1";


        static IAmazonS3 client;

        public static void Main(string[] args)
        {
            //AWSGetFiles();
            AWSUploadFile();
        }

        static void AWSGetFiles()
        {
            client = Amazon.AWSClientFactory.CreateAmazonS3Client(
                                accessKeyID, secretAccessKeyID,Amazon.RegionEndpoint.USEast1);

            ListObjectsRequest request = new ListObjectsRequest();
            request.BucketName = bucketName;
            
            do
            {
                ListObjectsResponse response = client.ListObjects(request);

                // Process response.
                // ...

                // If response is truncated, set the marker to get the next 
                // set of keys.
                if (response.IsTruncated)
                {
                    request.Marker = response.NextMarker;
                }
                else
                {
                    request = null;
                }
            } while (request != null);

        }

        static void AWSUploadFile()
        {

            using (client = Amazon.AWSClientFactory.CreateAmazonS3Client(
                               accessKeyID, secretAccessKeyID, Amazon.RegionEndpoint.USEast1))
            {
                Console.WriteLine("Uploading an object");
                WritingAnObject();
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
        static void WritingAnObject()
        {
            try
            {
                PutObjectRequest putRequest1 = new PutObjectRequest
                {
                    BucketName = bucketName,
                    Key = keyName,
                    ContentBody = "sample text"
                };

                PutObjectResponse response1 = client.PutObject(putRequest1);

                // 2. Put object-set ContentType and add metadata.
                PutObjectRequest putRequest2 = new PutObjectRequest
                {
                    BucketName = bucketName,
                    Key = keyName,
                    FilePath = filePath,
                    ContentType = "text/plain"
                };
                putRequest2.Metadata.Add("x-amz-meta-title", "someTitle");

                PutObjectResponse response2 = client.PutObject(putRequest2);

            }
            catch (AmazonS3Exception amazonS3Exception)
            {
                if (amazonS3Exception.ErrorCode != null &&
                    (amazonS3Exception.ErrorCode.Equals("InvalidAccessKeyId")
                    ||
                    amazonS3Exception.ErrorCode.Equals("InvalidSecurity")))
                {
                    Console.WriteLine("Check the provided AWS Credentials.");
                    Console.WriteLine(
                        "For service sign up go to http://aws.amazon.com/s3");
                }
                else
                {
                    Console.WriteLine(
                        "Error occurred. Message:'{0}' when writing an object"
                        , amazonS3Exception.Message);
                }
            }
        }
    }
}