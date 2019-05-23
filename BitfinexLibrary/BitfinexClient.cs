using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
namespace BitfinexLibrary
{
    public class BitfinexClient
    {
        public decimal Market(string FirstCrypto, string SecondCrypto, string AskOrBid)
        {
            decimal value = 0;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://api.bitfinex.com/v1/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync($"ticker/{FirstCrypto}{SecondCrypto}").Result;
            if (response.IsSuccessStatusCode && AskOrBid == "ask")
            {
                var products = response.Content.ReadAsStringAsync().Result;
                JObject jObject = JObject.Parse(products);
                value = (decimal)jObject["ask"];
            }
            else if (response.IsSuccessStatusCode && AskOrBid == "bid")
            {
                var products = response.Content.ReadAsStringAsync().Result;
                JObject jObject = JObject.Parse(products);
                value = (decimal)jObject["bid"];
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }
            return value;
        }
    }
}