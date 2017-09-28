using IdentityModel.Client;
using System;
using System.Net.Http;
using Xunit;

namespace Com.Moonlay.Service.Project.WebApi.Test
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        { 
            var disco = DiscoveryClient.GetAsync("http://127.0.0.1:5000").Result;
            var tokenClient = new TokenClient(disco.TokenEndpoint, "unit.test", "test");
            var tokenResponse = tokenClient.RequestClientCredentialsAsync("service.project.read").Result;

            //if (tokenResponse.IsError)
            Assert.False(tokenResponse.IsError);
            //{
            //    Console.WriteLine(tokenResponse.Error);
            //    return;
            //}

            var client = new HttpClient();
            client.SetBearerToken(tokenResponse.AccessToken);

            var response = client.GetAsync("http://localhost:5001/api/values").Result;
            Assert.True(response.IsSuccessStatusCode);
            //if (!response.IsSuccessStatusCode)
            //{
            //    Ass
            //    Console.WriteLine(response.StatusCode);
            //}
            //else
            //{
            //    var content = await response.Content.ReadAsStringAsync();
            //    Console.WriteLine(JArray.Parse(content));
            //}
        }
    }
}
