using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;

namespace RCMApi.Services
{
    public class OzowRequest
    {
        private string siteCode;
        private string countryCode;
        private string currencyCode;
        public string amount;
        public string transactionReference;
        private string bankReference;
        private bool isTest;
        private string secret;
        private string hashCheck;

        public async Task OzowReq(int _amount)
        {
            HttpClient client = new HttpClient();
            var concat = new String($"{siteCode}{countryCode}{currencyCode}{amount}{transactionReference}{bankReference}{isTest}");
            concat = concat.ToLower();

            var json = JsonConvert.SerializeObject(concat);
            var content = new StringContent(json, Encoding.UTF8, "applcation/json");

            var response = await client.PostAsync("https://pay.ozow.com", content);

            concat = $"{concat}{secret}";

            var data = Encoding.UTF8.GetBytes(concat);
            using(SHA512 shaM = new SHA512Managed())
            {
                hashCheck = shaM.ComputeHash(data).ToString();
            }
        }
    }
}
