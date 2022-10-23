using System.Text;
using Newtonsoft.Json.Linq;

namespace Serverless
{
  public class Lambda
  {
    public static async Task Handler(string name, Func<JObject, JObject> callback)
    {
      if (name != Environment.GetEnvironmentVariable("_HANDLER"))
      {
        return;
      }

      var api = Environment.GetEnvironmentVariable("AWS_LAMBDA_RUNTIME_API");
      var client = new HttpClient();
      while (true)
      {
        var request = new HttpRequestMessage(HttpMethod.Get, $"http://{api}/2018-06-01/runtime/invocation/next");
        var response = await client.SendAsync(request);

        var requestID = response.Headers.GetValues("Lambda-Runtime-Aws-Request-Id").First();
        var input = JObject.Parse(await response.Content.ReadAsStringAsync());

        JObject body;
        string url;
        try
        {
          body = callback(input);
          url = $"http://{api}/2018-06-01/runtime/invocation/{requestID}/response";
        }
        catch (System.Exception)
        {
          body = new JObject
          {
            ["statusCode"] = 500,
            ["body"] = new JObject
            {
              ["msg"] = "Internal Lambda Error",
            }.ToString(),
          };
          url = $"http://{api}/2018-06-01/runtime/invocation/{requestID}/error";
        }

        request = new HttpRequestMessage(HttpMethod.Post, url);
        request.Content = new StringContent(body.ToString(), Encoding.UTF8, "application/json");
        await client.SendAsync(request);
      }
    }
  }
}