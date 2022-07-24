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
using DSharpPlus.CommandsNext;

using DSharpPlus.CommandsNext.Attributes;

using DSharpPlus;

using DSharpPlus.Entities;

namespace BolsoBot.Commands.Moderation;

public class Ban : BaseCommandModule
{
    [
        Command("ban"), 
        Description("Bans a Member From The Server"), 
        RequirePermissions(Permissions.BanMembers)
    ]
    public async Task BanUser(CommandContext ctx,[Description("The User To Ban From The Server")] DiscordMember member,[Description("Delete the messages from the user in the past days (default is 0)")] ushort deleteMessageDays = 0,[Description("The Reason Why The Member Will Be Banned")] string? reason = null)
    {
        await ctx.TriggerTypingAsync().ConfigureAwait(false);

        if (member is null)
        {
            await ctx.RespondAsync("Please specify a member to ban");
            return;
        }
        if ((await ctx.Guild.GetBanAsync(member)) != null)
        {
            await ctx.RespondAsync($"{member.Mention} is already banned");
            return;
        }

        try
        {
            await ctx.Guild.BanMemberAsync(member.Id, deleteMessageDays,reason);
        }
        catch (System.Exception ex)
        {
             await ctx.RespondAsync($"Failed to ban {member.Mention}");
             return;
        }

        await ctx.RespondAsync($"{member.Mention} was banned");
    }
}
