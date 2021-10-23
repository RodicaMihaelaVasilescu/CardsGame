using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace WebAPI.Timer
{
	public class CustomTimer : System.Timers.Timer
	{
		public CustomTimer(double interval)
			: base(interval)
		{
		}

		public HubCallerContext callerContext { get; set; }
		public static ConcurrentDictionary<string, CustomTimer> Timers = new ConcurrentDictionary<string, CustomTimer>();
		//public IHubCallerClients<IClient> hubCallerClients { get; set; }
	}
}
