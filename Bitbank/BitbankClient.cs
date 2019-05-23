using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;

namespace BitbankLibrary
{
    public class BitbankClient
    {
        public decimal Market(string FirstCrypto, string SecondCrypto, string AskOrBid)
        {
            decimal value = 0;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://public.bitbank.cc/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync($"{FirstCrypto}_{SecondCrypto}/ticker").Result;
            if (response.IsSuccessStatusCode && AskOrBid == "ask")
            {
                var products = response.Content.ReadAsStringAsync().Result;
                JObject jObject = JObject.Parse(products);
                value = (decimal)jObject["data"]["sell"];
            }
            else if (response.IsSuccessStatusCode && AskOrBid == "bid")
            {
                var products = response.Content.ReadAsStringAsync().Result;
                JObject jObject = JObject.Parse(products);
                value = (decimal)jObject["data"]["buy"];
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }
            return value;
        }
    }
}