using System;
using System.Threading.Tasks;
using DataAccess.Manager;
using DataAccess.Model;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Internal;
using WebAPI.Hub;

namespace WebAPI.Timer
{
	public class ApplicationHub : Microsoft.AspNetCore.SignalR.Hub// <IClient>
	{
		// This method will be called on Start button click
		public CustomTimer timer = new CustomTimer(1000);
		private readonly IHubContext productsHub;
		private int index = 0;

		public ApplicationHub()
		{
			this.productsHub = GlobalHost.ConnectionManager.GetHubContext<ItemHub>();
		}

		public async Task StartTime()
		{
			timer = CustomTimer.Timers.GetOrAdd(""/*Context.ConnectionId*/, timer);
			timer.callerContext = Context;
			timer.Elapsed += aTimer_Elapsed;
			timer.Interval = 10000;
			timer.Enabled = true;
			productsHub.Clients.All.ShuffleCards();
		}

		// This method will pass time to all connected clients
		void aTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
		{
			var timer = (CustomTimer)sender;
			timer = (CustomTimer)sender;
			HubCallerContext hcallerContext = timer.callerContext;
      //this.productsHub.Clients.All.AddCardToPage("item" + index.ToString());
      productsHub.Clients.All.ShuffleCards(CardsManager.GetRandomCards());
      index++;
		}

		// This should stop running timer on button click event from client
		public async Task StopTime()
		{
			timer = CustomTimer.Timers.GetOrAdd(Context.ConnectionId, timer);
			timer.Elapsed -= aTimer_Elapsed;
			timer.Enabled = false;

			//await Clients.All.StopTime("Timer Stopped");
		}
	}
}