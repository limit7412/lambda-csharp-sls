using Newtonsoft.Json.Linq;

class SampleLambda : Serverless.Lambda
{
  static async Task Main(string[] args)
  {
    await Handler("hello", _ =>
    {
      return new JObject
      {
        ["statusCode"] = 200,
        ["body"] = new JObject
        {
          ["msg"] = "さよなら透明だった僕たち",
        }.ToString(),
      };
    });

    await Handler("world", input =>
    {
      return new JObject
      {
        ["statusCode"] = 200,
        ["body"] = new JObject
        {
          ["msg"] = "でしょうねミスター・サーバーレス",
          ["event"] = JObject.Parse(input.GetValue("body").ToString()),
        }.ToString(),
      };
    });
  }
}
