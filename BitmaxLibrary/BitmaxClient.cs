using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;

namespace BitmaxLibrary
{
    public class BitmaxClient
    {
        public decimal Market(string FirstCrypto, string SecondCrypto, string AskOrBid)
        {
            decimal value = 0;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://bitmax.io/api/v1/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync($"quote?symbol={FirstCrypto}-{SecondCrypto}").Result;
            if (response.IsSuccessStatusCode && AskOrBid == "ask")
            {
                var products = response.Content.ReadAsStringAsync().Result;
                JObject jObject = JObject.Parse(products);
                value = (decimal)jObject["askPrice"];
            }
            else if (response.IsSuccessStatusCode && AskOrBid == "bid")
            {
                var products = response.Content.ReadAsStringAsync().Result;
                JObject jObject = JObject.Parse(products);
                value = (decimal)jObject["bidPrice"];
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }
            return value;
        }
    }
}