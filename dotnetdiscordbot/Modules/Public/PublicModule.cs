using System.Threading.Tasks;
using Discord.Commands;
using Discord.Net;
using Discord.WebSocket;
using System.Linq;
using System.Net;
using Discord;
using System.Runtime.InteropServices;
using System;
using System.Diagnostics;

namespace pringles1_0.Modules.Public
{
    public class PublicModule : ModuleBase<SocketCommandContext>
    {
        private DiscordSocketClient client;
        private ISocketMessageChannel channel; 

        [Command("invite")]
        [Summary("Returns the OAuth2 Invite URL of the bot")]
        public async Task Invite()
        {
            var application = await Context.Client.GetApplicationInfoAsync();
            await ReplyAsync($"A user with `MANAGE_SERVER` can invite me to your server here: <https://discordapp.com/oauth2/authorize?client_id={application.Id}&scope=bot>");
        }

        [Command("say")]
        [Alias("echo")]
        [Summary("Echos the provided input")]
        public async Task Say([Remainder] string input)
        {
            await ReplyAsync(input);
        }

        [Command("purge")]
        [Alias("purgemessage")]
        [Summary("Purges the provided amount of numbers")]
        public async Task Purge([Remainder] int numberToPurge)
        {
            //var messages = channel.GetMessagesAsync(numberToPurge);           
            //channel.DeleteMessagesAsync(messages);
        }

        /* [Command("join")]
        public async Task JoinChannel(IVoiceChannel channel = null)
        {
            // Get the audio channel
            channel = channel ?? (msg.Author as IGuildUser)?.VoiceChannel;
            if (channel == null) { await msg.Channel.SendMessageAsync("User must be in a voice channel, or a voice channel must be passed as an argument."); return; }

            // For the next step with transmitting audio, you would want to pass this Audio Client in to a service.
            var audioClient = await channel.ConnectAsync();
         }
        */

        [Command("setperms")]
        [Alias("perms")]
        [Summary("Echos the provided input")]
        public async Task SetPerms()
        {
            try
            {
                await ReplyAsync("", true);
            }
            catch
            {
                Console.WriteLine("[1] Couldn't send TTS message.");
            }
        }
        

        

    }
}
