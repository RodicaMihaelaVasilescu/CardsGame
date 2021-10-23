using System.Web.Http;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Cors;
using Microsoft.Practices.Unity;
using Owin;
using WebAPI.Repository;

namespace WebAPI
{
  public class Startup
	{
		private void RegisterServices(HttpConfiguration config)
		{
			var container = new UnityContainer();

			container.RegisterType<ICardRepository, CardRepository>();
			container.RegisterType<ICardService, CardService>();

			config.DependencyResolver = new UnityDependencyResolver(container);
		}

		// This code configures Web API. The Startup class is specified as a type
		// parameter in the WebApp.Start method.
		public void Configuration(IAppBuilder appBuilder)
		{
			HttpConfiguration config = new HttpConfiguration();

			RegisterServices(config);

			config.Routes.MapHttpRoute(
				name: "DefaultApi",
				routeTemplate: "api/{controller}/{id}",
				defaults: new { id = RouteParameter.Optional }
			);

			config.MapHttpAttributeRoutes();

			config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;

			config.EnsureInitialized();

			appBuilder.UseWebApi(config);

			appBuilder.Map(
				"/signalr",
				map =>
				{
					map.UseCors(CorsOptions.AllowAll);

					var hubConfig = new HubConfiguration()
					{

					};

					map.RunSignalR(hubConfig);
				});
		}
	}
}
