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
using DSharpPlus;

using DSharpPlus.CommandsNext;

using DSharpPlus.CommandsNext.Attributes;

using DSharpPlus.Entities;

namespace BolsoBot.Commands.Moderation;

public class UnBan : BaseCommandModule
{
    [
        Command("unban"),
        Description("Bans a Member From The Server"),
        RequirePermissions(Permissions.BanMembers)
    ]
    public async Task UnBanUser(CommandContext ctx, [Description("The Member To Unban")] DiscordMember member, [Description("The Reason To Unban")] string? reason = null)
    {
        await ctx.TriggerTypingAsync().ConfigureAwait(false);

        if ((await ctx.Guild.GetBanAsync(member.Id)) == null)
        {
            await ctx.RespondAsync($"{member.Mention} is not banned.");
            return;
        }
        try
        {
            await ctx.Guild.UnbanMemberAsync(member, reason);
        }
        catch (System.Exception ex)
        {
            await ctx.RespondAsync($"Failed to unban {member.Mention}.");
            return;
        }

        await ctx.RespondAsync($"{member.Mention} was unbanned.");
    }
}
