using System.Net;

namespace Microsoft.OpenApi.CodeGeneration
{
    internal static class RepositoryDownloader
    {
        public static void DownloadFile(string address, string destination)
        {
            WebClient client = new WebClient();
            client.DownloadFile(address, destination);
        }
    }
}