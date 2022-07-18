//MIT License
//
//Copyright (c) 2022 Daniel
//
//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:
//
//The above copyright notice and this permission notice shall be included in all
//copies or substantial portions of the Software.
//
//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE.
using System;

using System.Collections.Generic;

using System.Linq;

using System.Threading.Tasks;

using System.Text.RegularExpressions;

using DSharpPlus.CommandsNext.Attributes;

using DSharpPlus;

using DSharpPlus.CommandsNext;

using DSharpPlus.Entities;

namespace BolsoBot.Commands.Moderation;


[Group("admin")] //marks the class as a command group
[Description("Administrative Commands")] //description for help purposes
[RequirePermissions(Permissions.ManageGuild)]
public class ModerationCommands : BaseCommandModule
{
    [
        Command("setnick"),
        Description("Gives Someone a New Nickname"),
        RequirePermissions(Permissions.ManageNicknames)
    ]
    public async Task ChangeNickname(CommandContext ctx, DiscordMember member, string newNickname)
    {
        //trigger typing indicator
        await ctx.TriggerTypingAsync().ConfigureAwait(false);

        try
        {
            await member.ModifyAsync(member =>
            {
                member.Nickname = newNickname;
                member.AuditLogReason = $"Changed by {ctx.User.Username} {ctx.User.Id}";
            });

            var emoji = DiscordEmoji.FromName(ctx.Client, ":+1:");

            await ctx.RespondAsync(emoji);
        }
        catch (Exception ex)
        {
             var emoji = DiscordEmoji.FromName(ctx.Client, ":-1:");

             await ctx.RespondAsync(emoji);
        }
    }

    [
        Command("kick"),
        Description("Kick Users"),
        RequirePermissions(Permissions.KickMembers)
    ]
    public async Task KickUser(CommandContext ctx, DiscordMember member, string reason)
    {
        //await ctx.TriggerTypingAsync().ConfigureAwait(false);
        //
        //try
        //{
        //    
        //}
        //catch (System.Exception ex)
        //{
        //     // TODO
        //}
    }

    [
        Command("ban"),
        Description("Ban Users"),
        RequirePermissions(Permissions.BanMembers)
    ]
    public async Task BanUser(CommandContext ctx, DiscordMember member,[RemainingText] string reason)
    {
        //await ctx.TriggerTypingAsync().ConfigureAwait(false);
        //
        //try
        //{
        //    await member.BanAsync(1, reason);
        //    await ctx.RespondAsync($"{member.Id} was banned");
        //    await member.ModifyAsync(member => 
        //    {
        //        member.AuditLogReason = $"Banned by {ctx.User.Username} {ctx.User.Username}, reason {reason}";
        //    });
        //}
        //catch (System.Exception ex)
        //{
        //    await ctx.RespondAsync($"Could not ban {member.Username}");
        //    await ctx.RespondAsync($"{ex}");
        //}
    }
}
