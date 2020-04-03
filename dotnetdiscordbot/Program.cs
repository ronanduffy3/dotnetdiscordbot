using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Reflection;

namespace pringles1_0
{
    class Program
    {
        public static void Main(string[] args) =>
             new Program().Start().GetAwaiter().GetResult();

        private CommandService commands;
        private DiscordSocketClient client;
        private CommandHandler handler;

        public async Task Start()
        {

            // As we are not using 4.6 we must include the Discord.Net WebSocketProvider.
            client = new DiscordSocketClient(new DiscordSocketConfig {
                WebSocketProvider = Discord.Net.Providers.WS4Net.WS4NetProvider.Instance,
                LogLevel = LogSeverity.Verbose
            });
            // Writes detailed log to the console           
            client.Log += (l)
                => Task.Run(()
                => Console.WriteLine($"[{l.Severity}] {l.Source}: {l.Exception?.ToString() ?? l.Message}"));

            // Variable for token 
            var token = "MjY0MjAwODI2NDI2MzU5ODA4.C3NZWg.hLb2Z8U66zcef7fgC43yAra6Ydo";

            // Use token to login, and then use asyncconnect to connect to discord
            await client.LoginAsync(TokenType.Bot, token);
            await client.ConnectAsync();

            // Create a new dependency map
            var map = new DependencyMap();
            map.Add(client);

            // Recall command handler and add the dependency map to it
            handler = new CommandHandler();
            await handler.Install(map);

            // Hooking into events

            client.UserJoined += async (e) =>
            {
                try
                {
                    ulong channelid = 266188297339011082;
                    e.Discord.GetChannel(channelid);
                    Console.WriteLine("Channel Found");
                }
                catch
                {

                    Console.WriteLine("Channel not found, or error with code");

                }
            };

            // Block the closing of the program until it is closed
            await Task.Delay(-1);
        }
              
        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
    }
}
