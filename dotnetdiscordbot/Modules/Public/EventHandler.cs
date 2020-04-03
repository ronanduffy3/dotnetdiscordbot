using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;

namespace pringles1_0.Modules.Public
{
    public class EventHandler : ModuleBase<SocketCommandContext>
    {
        private CommandService commands;
        private DiscordSocketClient client;
        private CommandHandler handler;

        public async Task eventRecieved()
        {

            var client = new DiscordSocketClient();

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
            
            
        }

        
    }
}
