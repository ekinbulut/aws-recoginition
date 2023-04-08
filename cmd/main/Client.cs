using System.Net;
using System.Text.Json.Serialization;
using Amazon;
using Amazon.Rekognition;
using Amazon.Rekognition.Model;
using Newtonsoft.Json;

namespace main;

public class Client
{
    private readonly string _awsAccessKeyId = "";
    private readonly string _awsSecretAccessKey = "";
    
    public async Task<string> AnalyseImage(MemoryStream bytes, CancellationToken cancellationToken)
    {
        var rkgClient = new AmazonRekognitionClient(_awsAccessKeyId, _awsSecretAccessKey, new AmazonRekognitionConfig()
        {
            RegionEndpoint = RegionEndpoint.USEast1
        });
        var request = new DetectFacesRequest
        {
            Attributes = new List<string>(){"ALL"},
            Image = new Image
            {
                Bytes = bytes
            }
        };
        var response = await rkgClient.DetectFacesAsync(request, cancellationToken);

        return response.HttpStatusCode != HttpStatusCode.OK ? String.Empty : SerializeResponse(response);
    }

    private string SerializeResponse<T>(T value) where T : class
    {
        return JsonConvert.SerializeObject(value);
    }
    
    public async Task<MemoryStream> GetImageStreamAsync(string imagePath)
    {
        await using var fileStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read, FileShare.Read);
        using var memoryStream = new MemoryStream();

        await fileStream.CopyToAsync(memoryStream);
        memoryStream.Seek(0, SeekOrigin.Begin);

        return memoryStream;
    }

}