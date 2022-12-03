using DSharpPlus.Entities;
using DSharpPlus;
using DSharpPlus.SlashCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Net;
using Newtonsoft.Json;

namespace bot.Commands
{
    public class SlashCommands : ApplicationCommandModule
    {
        [SlashCommand("freebadge", "Give OSdoge a free Developer Badge")]
        public async Task FreeBadge(InteractionContext ctx)
        {
            await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().WithContent("thx"));
        }

        [SlashCommand("test", "A slash command made to test the DSharpPlus Slash Commands extension!")]
        public async Task TestCommand(InteractionContext ctx)
        {
            await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().WithContent("Working"));
        }

        [SlashCommand("convo", @"Have a ""Conversation"" with me")]
        public async Task Convo(InteractionContext ctx)
        {
            await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().WithContent("Let us start."));

            var builder = new DiscordMessageBuilder()
                .WithContent("Hi!")
                .AddComponents(new DiscordComponent[]
            {
                        new DiscordButtonComponent(ButtonStyle.Primary, "button1", "hi"),
                        new DiscordButtonComponent(ButtonStyle.Primary, "button2", "hello")
            })
            .SendAsync(ctx.Channel);

            var client = Bot.Client;

            bool isOn = true;


            //Nth time using command = n number of facts given????
            client.ComponentInteractionCreated += async (s, e) =>
            {
                Console.WriteLine("Response!");

                if (e.Id != "end")
                {
                    isOn = false;
                    var responseBuilder = await new DiscordMessageBuilder()
                    .WithContent(await RandomTextGen())
                    .AddComponents(new DiscordComponent[]
                    {
                        new DiscordButtonComponent(ButtonStyle.Primary, "button3", "ok"),
                        new DiscordButtonComponent(ButtonStyle.Primary, "button4", "nice"),
                        new DiscordButtonComponent(ButtonStyle.Primary, "button5", "what"),
                        new DiscordButtonComponent(ButtonStyle.Secondary, "end", "k bye")
                    })
                    .SendAsync(ctx.Channel);
                }
                else
                {
                    var goodbyeBuilder = new DiscordMessageBuilder().WithContent($"That was the best conversation ever, <@{ctx.User.Id}>.").SendAsync(ctx.Channel);
                }
                isOn = true;
            };

            

            //await ctx.Guild.BanMemberAsync(user.Id, (int)deleteDays);
        }

        public async Task<string> RandomTextGen()
        {
            string url = @"https://api.api-ninjas.com/v1/facts";

            WebRequest request;
            request = WebRequest.Create(url);
            request.Method = "GET";

            var json = string.Empty;
            using (var fs = File.OpenRead(@"secrets.json"))
            using (var sr = new StreamReader(fs, new UTF8Encoding(false)))
                json = await sr.ReadToEndAsync().ConfigureAwait(false);

            var token = JsonConvert.DeserializeObject<Secrets>(json).ApiToken;

            request.Headers["X-Api-Key"] = token;

            using var webResponse = request.GetResponse();
            using var webStream = webResponse.GetResponseStream();

            using var reader = new StreamReader(webStream);
            var data = reader.ReadToEnd();
            Console.WriteLine(data);

            data = data.Substring(11, data.Length - 14);
            Console.WriteLine(data);

            return(data);
        }

    }
}
