using System.Net.Http;
using System.Threading.Tasks;
using DataAccess.Model;

namespace WebAPI.Hub
{
  public class ItemHub : Microsoft.AspNet.SignalR.Hub
	{
		public void Send(string name, string message)
		{
			// Call the broadcastMessage method to update clients.
			Clients.All.broadcastMessage(name, message);
		}

		public void SendAsync(HttpRequestMessage response)
		{
			// Call the broadcastMessage method to update clients.
			Clients.All.broadcastMessage(response);
		}

		public async Task AddNewItems(Card product)
		{
			await Clients.All.addNewMessageToPage("", "");
		}
		
		public async Task NewItemMessage(string name, string message)
		{
			await Clients.All.addNewMessageToPage(name, message);
		}
		public override Task OnConnected()
		{
			return base.OnConnected();
		}

		public override Task OnDisconnected(bool stopCalled)
		{
			return base.OnDisconnected(stopCalled);
		}

		public override Task OnReconnected()
		{
			return base.OnReconnected();
		}
    }
}
