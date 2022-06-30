using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;



namespace LoopMainProject.Intergration
{
    public class HealthCheckTest: IClassFixture<WebApplicationFactory<Program>>
    {
		private readonly HttpClient _httpClient;

		public HealthCheckTest(WebApplicationFactory<Program> factory) =>
			_httpClient = factory.CreateDefaultClient();

		[Fact]
		public async Task HealthCheckReturnExpectedResponse()
		{
			var response = await _httpClient.GetStringAsync("/Health");

			response.Should().Be("Healthy");
		}
	}
}
