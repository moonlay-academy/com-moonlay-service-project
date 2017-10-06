using IdentityModel.Client;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Com.Moonlay.Service.Project.WebApi.Test
{
    public class UnitTest1
    {
        [Fact]
        public async Task Test1()
        {
            var discoClient = new DiscoveryClient("https://auth.moonlay.com/"); //TOCHECK: is trailing / required?
            //discoClient.Policy.RequireHttps = true;
            var disco = await discoClient.GetAsync();

            var tokenClient = new TokenClient(disco.TokenEndpoint, "unit.test", "test");
            var tokenResponse = tokenClient.RequestClientCredentialsAsync("service.project.read").Result;
            
            Assert.False(tokenResponse.IsError);

            var client = new HttpClient();
            client.SetBearerToken(tokenResponse.AccessToken);

            var response = client.GetAsync("http://127.0.0.1:5001/api/values").Result;
            Assert.True(response.IsSuccessStatusCode, string.Join("/", new List<string> { response.StatusCode.ToString(), response.ReasonPhrase }));
        }
    }
}
