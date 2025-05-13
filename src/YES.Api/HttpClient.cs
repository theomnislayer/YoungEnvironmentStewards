using YES.Core;

namespace YES;

public interface IHttpClient : Core.IHttpClient { }

public class HttpClient : Core.HttpClient, IHttpClient
{
	public HttpClient(HttpClientFactory httpClientFactory)
	{
		var baseAddress = Environment.GetEnvironmentVariable("OpenApiEndpoint");
        Client = httpClientFactory.Get(baseAddress);
	}
}
