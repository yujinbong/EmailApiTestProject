using NUnit.Framework;
using RestSharp;
using RestSharp.Authenticators;
using System.Threading.Tasks;

namespace EmailApiTestProject
{
    public class MailgunTests
    {
        private const string ApiBaseUrl = "https://api.mailgun.net/v3/sandbox8429c838ba4a430cb31c1b02daf3b7dd.mailgun.org";
        private const string ApiKey = " ";

        private RestClient client;

        [SetUp]
        public void Setup()
        {
            var options = new RestClientOptions(ApiBaseUrl)
            {
                Authenticator = new HttpBasicAuthenticator("api", ApiKey)
            };

            client = new RestClient(options);
        }

        [Test]
        public async Task SendEmail_Success()
        {
            var request = new RestRequest("/messages", Method.Post);

            request.AddParameter("from", "Yujin <mailgun@sandbox8429c838ba4a430cb31c1b02daf3b7dd.mailgun.org>");
            request.AddParameter("to", "aso23456@gmail.com");
            request.AddParameter("subject", "Mailgun API Å×½ºÆ®");
            request.AddParameter("text", "Yujin's first Mailgun automated email!");

            var response = await client.ExecuteAsync(request);
            Console.WriteLine("Response content: " + response.Content);
            Console.WriteLine($"Status code: {(int)response.StatusCode}");
            Console.WriteLine($"Response content: {response.Content}");
            Assert.That((int)response.StatusCode, Is.EqualTo(200), "Status code should be 200 OK");
            Assert.That(response.Content, Does.Contain("Queued"), "Response should indicate email is queued");
        }
    }
}
