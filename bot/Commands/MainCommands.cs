using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.CommandsNext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bot.Commands
{
    public class MainCommands : BaseCommandModule
    {
        [Command("freebadge")]
        public async Task FreeBadge(CommandContext ctx)
        {
            await ctx.Channel.SendMessageAsync("Free Developer Badge :D");
        }

        [Command("convo")]
        public async Task Conversation(CommandContext ctx)
        {
            await ctx.Channel.SendMessageAsync("Under Development");
        }
    }
}
