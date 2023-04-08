// See https://aka.ms/new-console-template for more information

using System.Threading.Channels;
using main;

Console.WriteLine("CMD 1.0");

var client = new Client();
var cancellationToken = new CancellationToken();
var bytes = await client.GetImageStreamAsync("");
var response = await client.AnalyseImage(bytes, cancellationToken);

Console.WriteLine(response);