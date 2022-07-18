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

using DSharpPlus.CommandsNext;

using DSharpPlus.Entities;

namespace BolsoBot.Commands;

[Group("utility")]
[Description("Miscellaneous Commands")]
public class UtilityCommands : BaseCommandModule
{
    [Command("avatar")]
    public async Task GetUserAvatar(CommandContext ctx, DiscordMember member)
    {
        await ctx.Channel.SendMessageAsync(member.AvatarUrl);
    }

    [Command("avatar")]
    public async Task GetUserAvatar(CommandContext ctx)
    {
        await ctx.Channel.SendMessageAsync(ctx.User.AvatarUrl);
    }

    [Command("Ping")]
    public async Task Ping(CommandContext ctx)
    {
        await ctx.RespondAsync($"Ping :{ctx.Client.Ping}ms");
    }
}
