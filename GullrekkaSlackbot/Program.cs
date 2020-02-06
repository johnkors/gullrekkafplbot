using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Slackbot.Net.Abstractions.Hosting;
using Slackbot.Net.Configuration;
using Slackbot.Net.Extensions.Publishers.Logger;
using Slackbot.Net.Extensions.Publishers.Slack;

namespace GullrekkaSlackbot
{
    class Program
    {
        static async Task Main(string[] args)
        {
                var host = Host.CreateDefaultBuilder()
                .ConfigureServices((c,s) =>
                {
                    s.AddSlackbotWorker(c.Configuration)
                        .AddSlackPublisher(o => o.BotToken = c.Configuration.GetValue<string>(nameof(SlackOptions.Slackbot_SlackApiKey_BotUser)))
                        .AddSlackPublisherBuilder()
                        .AddLoggerPublisherBuilder()
                        .AddFplBot(c.Configuration.GetSection("fpl"));
                })
                .ConfigureLogging(c => c.AddConsole().SetMinimumLevel(LogLevel.Debug))
                .Build();

            using (host)
            {
                await host.RunAsync();
            }        
        }
    }
}
