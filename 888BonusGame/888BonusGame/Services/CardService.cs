using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Formatting;
using Newtonsoft.Json;
using DataAccess.Model;
using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using WebAPI.Hub;

namespace _888BonusGame.Services
{
  class CardService
  {
    private readonly HttpClient HttpClient;
    private string baseAddress;
    private readonly HubConnection conection;
    private readonly IHubProxy itemsHub;

    public event Action<List<Card>> ShuffledCards;
    public event Action<Card> CardReceived;
    public event Action<Card> CardPut;
    public event Action<Guid> CardDeleted;
    public event Action<string> EmptyCardReceived;

    public CardService(HttpClient httpClient)
    {
      HttpClient = httpClient;
      baseAddress = GetHubConnectionString.Instance.HubConnectionString;
      conection = new HubConnection(baseAddress);

      itemsHub = conection.CreateHubProxy("ItemHub");
      itemsHub.On<List<Card>>("ShuffleCards", item => ShuffledCards?.Invoke(item));
      itemsHub.On<string>("AddCardToPage", item => EmptyCardReceived?.Invoke((item)));
      itemsHub.On<Card>("AddNewCardToPage", item => CardReceived?.Invoke((item)));
      itemsHub.On<Card>("PutCardToPage", item => CardPut?.Invoke((item)));
      itemsHub.On<Guid>("DeleteCardFromPage", item => CardDeleted?.Invoke((item)));
      conection.Start().Wait();
    }

    public async Task PostItem(Card Card)
    {
      await HttpClient.PostAsync<Card>(baseAddress + "api/cards", Card, new JsonMediaTypeFormatter());
    }

    public async Task DeleteItem(Guid id)
    {
      var request = new HttpRequestMessage
      {
        Method = HttpMethod.Delete,
        RequestUri = new Uri(baseAddress + "api/cards"),
        Content = new StringContent(JsonConvert.SerializeObject(id), Encoding.UTF8, "application/json")
      };
      var response = await HttpClient.SendAsync(request);
    }

    public async Task PutItem(Card id)
    {
      var request = new HttpRequestMessage
      {
        Method = HttpMethod.Put,
        RequestUri = new Uri(baseAddress + "api/cards"),
        Content = new StringContent(JsonConvert.SerializeObject(id), Encoding.UTF8, "application/json")
      };
      var response = await HttpClient.SendAsync(request);
    }

    public async Task<List<Card>> GetAllItems()
    {
      string content = "";

      var allItem = await HttpClient.GetAsync(baseAddress + "api/cards");
      content = await allItem.Content.ReadAsStringAsync();
      var Cards = JsonConvert.DeserializeObject<List<Card>>(content);
      return Cards;
    }

    public async Task<IEnumerable<Card>> GetAllCardsByCategory(string category)
    {
      var response = await HttpClient.GetAsync(baseAddress + "api/cards/get/" + category);
      var content = await response.Content.ReadAsStringAsync();
      var Cards = JsonConvert.DeserializeObject<List<Card>>(content);

      return Cards;
    }

  }
}
