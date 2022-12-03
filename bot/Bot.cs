using System;
using System.ComponentModel;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using bot.Commands;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.EventArgs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using DSharpPlus.SlashCommands;

namespace bot
{
    public class Bot
    {
        public static DiscordClient Client { get; set; }
        public CommandsNextExtension commands { get; private set; }


        public async Task RunAsync()
        {
            var json = string.Empty;
            using (var fs = File.OpenRead(@"secrets.json"))
            using (var sr = new StreamReader(fs, new UTF8Encoding(false)))
                json = await sr.ReadToEndAsync().ConfigureAwait(false);

            var token = JsonConvert.DeserializeObject<Secrets>(json).Token;
            
            var config = new DiscordConfiguration
            {
                Token = token,
                //for replit: System.Environment.GetEnvironmentVariable("token")
                TokenType = TokenType.Bot,
                AutoReconnect = true
            };

            Client = new DiscordClient(config);

            Client.Ready += OnClientReady;

            //var commandsconfig = new commandsnextconfiguration
            //{
            //    stringprefixes = new string[] { "!" },
            //    enabledms = true

            //};

            //var commands = Client.UseCommandsNext(commandsConfig);

            //commands.RegisterCommands<MainCommands>();

            var slash = Client.UseSlashCommands();
            slash.RegisterCommands<SlashCommands>();

            await Client.ConnectAsync();

                await Task.Delay(-1);
        }

        private Task OnClientReady(object sender, ReadyEventArgs e)
        {
            Console.WriteLine("Loaded");
            return Task.CompletedTask;
        }
    }
}
