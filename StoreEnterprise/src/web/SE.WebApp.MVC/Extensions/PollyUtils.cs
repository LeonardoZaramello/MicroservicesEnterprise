using Polly;
using Polly.Extensions.Http;
using Polly.Retry;

namespace SE.WebApp.MVC.Extensions
{
    public class PollyUtils
    {
        public static AsyncRetryPolicy<HttpResponseMessage> RetryPolicy()
        {
            var retryPolicy = HttpPolicyExtensions.HandleTransientHttpError()
                .WaitAndRetryAsync(new[]
                {
                    TimeSpan.FromSeconds(1),
                    TimeSpan.FromSeconds(5),
                    TimeSpan.FromSeconds(10),

                }, (outcome, timespan, retryCount, context) =>
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Tentando pela {retryCount} vez");
                    Console.ForegroundColor = ConsoleColor.White;
                });

            return retryPolicy;
        }
    }
}
