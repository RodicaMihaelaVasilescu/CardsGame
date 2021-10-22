using WebAPI.Helpers;

namespace WebAPI.Hub
{
  public class GetHubConnectionString
  {

    public string HubConnectionString;
    private GetHubConnectionString()
    {
      HubConnectionString = string.Format("http://localhost:{0}/", AppConfigManager.Get("Port"));
    }
    private static GetHubConnectionString instance = null;
    public static GetHubConnectionString Instance
    {
      get
      {
        if (instance == null)
        {
          instance = new GetHubConnectionString();
        }
        return instance;
      }
    }
  }
}
