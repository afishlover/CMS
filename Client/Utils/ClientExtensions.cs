using System.Net.Http.Headers;

namespace Client.Utils
{
    public static class ClientExtensions
    {
        public static void AddCMSClient(this IServiceCollection services)
        {
            services.AddTransient(_ =>
            {
                HttpClient client = new HttpClient();
                var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);
                return client;
            });
        }
    }
}