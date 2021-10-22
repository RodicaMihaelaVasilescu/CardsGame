using Microsoft.Owin.Hosting;
using System;
using System.Net.Http;
using WebAPI.Timer;
using WebAPI.Helpers;
using WebAPI.Hub;

namespace WebAPI
{
  public class Program
  {
    public static void Main()
    {
      ApplicationHub timer = new ApplicationHub();
      timer.StartTime();

      string baseAddress = GetHubConnectionString.Instance.HubConnectionString;
      // Start OWIN host 
      using (WebApp.Start<Startup>(url: baseAddress))
      {
        // Create HttpClient and make a request to api/values 
        HttpClient client = new HttpClient();

        var response = client.GetAsync(baseAddress + "api/cards").Result;

        Console.WriteLine(response);
        Console.WriteLine(response.Content.ReadAsStringAsync().Result);
        Console.ReadLine();
      }
    }
  }
}
