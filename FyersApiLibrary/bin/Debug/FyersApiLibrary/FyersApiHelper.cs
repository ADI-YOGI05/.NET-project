// (Class Library)
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class FyersApiHelper
{
    private readonly HttpClient httpClient;

    public FyersApiHelper(string baseUrl, string accessToken)
    {
        if (string.IsNullOrWhiteSpace(baseUrl))
        {
            throw new ArgumentNullException(nameof(baseUrl), "Base URL cannot be null or empty.");
        }

        httpClient = new HttpClient
        {
            BaseAddress = new Uri(baseUrl)
        };

        if (!string.IsNullOrWhiteSpace(accessToken))
        {
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
        }

        httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

        
    }

    public string PlaceOrder(string symbol, string side, int qty, string jsonPayload)
    {
        // request payload
        var payload = new { Symbol = symbol, Side = side, Qty = qty };
        string requestJsonPayload = JsonConvert.SerializeObject(payload);

        int maxRetries = 3;
        int retries = 0;

        while (retries < maxRetries)
        {
            try
            {
                HttpResponseMessage response = httpClient.PostAsync("api/placeorder", new StringContent(jsonPayload, Encoding.UTF8, "application/json")).Result;
                return response.Content.ReadAsStringAsync().Result;
            }
            catch (HttpRequestException)
            {
               
                retries++;
                Task.Delay(1000).Wait(); 
            }
        }

       
        return "Failed after multiple retries";
    }

}
