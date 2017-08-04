using System;
using Xunit;
using Microsoft.AspNetCore.Hosting;
using memes_rest_api;
using Microsoft.AspNetCore.TestHost;
using System.Threading.Tasks;
using Newtonsoft.Json;
using memes_rest_api.Models;
using System.Net.Http;

namespace Memes.Rest.Api.Integration.Test
{
    public class MemesApi : IDisposable 
	{
		TestServer server;
		HttpClient httpClient;

        public MemesApi() 
        {
            server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            httpClient = server.CreateClient();
        }

		public void Dispose()
		{
			server.Dispose();
			httpClient.Dispose();
		}

        [Fact]
        public async Task AllMemes_HappyPath()
        {
            
            var response = await httpClient.GetAsync("api/memes");

            Assert.Equal(200,(int)response.StatusCode);
        }



        [Fact]
        public async Task Search_For_Batman_Return_AtLeast_One_Item()
        {
            var response = await httpClient.GetAsync("api/memes?description=batman");

            Assert.Equal(200, (int)response.StatusCode);

			var rawContent = await response.Content.ReadAsStringAsync();
			var content = JsonConvert.DeserializeObject<GetMemesResponse>(rawContent);
           
            Assert.NotEmpty(content.Data.Memes);

        }
		[Fact]
		public async Task Search_For_Invalid_Description_Return_Empty_Result()
		{
			var response = await httpClient.GetAsync("api/memes?description=12");

			var rawContent = await response.Content.ReadAsStringAsync();
			var content = JsonConvert.DeserializeObject<GetMemesResponse>(rawContent);

			Assert.Empty(content.Data.Memes);

		}
    }
} 
