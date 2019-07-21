using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Google.Cloud.Dialogflow.V2;
using Google.Protobuf;


[assembly: OwinStartup(typeof(WebHookApp.Startup))]

namespace WebHookApp
{
    public class Startup
    {
        private static readonly JsonParser jsonParser =
   new JsonParser(JsonParser.Settings.Default.WithIgnoreUnknownFields(true));

       
        public void Configuration(IAppBuilder app)
        {
            app.Use((context, next) =>
            {
                TextWriter output = context.Get<TextWriter>("host.TraceOutput");
                return next().ContinueWith(result =>
                {
                    output.WriteLine("Scheme {0} : Method {1} : Path {2} : MS {3}",
                    context.Request.Scheme, context.Request.Method, context.Request.Path, getTime());
                });
            });

            app.Run(async (context) =>
            {
                WebhookRequest request;

                using (var reader = new StreamReader(context.Request.Body))
                {
                    request = jsonParser.Parse<WebhookRequest>(reader);
                }

                var response = new WebhookResponse
                {
                    FulfillmentText = "Hello from " + request.QueryResult.Intent.DisplayName
                };

                await context.Response.WriteAsync(response.ToString());
            });

            //app.Run(async context =>
            //{
            //    await context.Response.WriteAsync(getTime() + "test");
            //});
        }

        string getTime()
        {
            return DateTime.Now.Millisecond.ToString();
        }
    }

}
