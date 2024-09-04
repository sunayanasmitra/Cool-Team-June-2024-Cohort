namespace HealthCareApp.Client
{
	public class PublicHttp
	{
		public HttpClient Client { get; }
		public PublicHttp(HttpClient httpClient)
		{
			Client = httpClient;
		}
	}
}
