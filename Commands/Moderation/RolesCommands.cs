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

using DSharpPlus.CommandsNext;

using System.Text.RegularExpressions;

using DSharpPlus;

using DSharpPlus.Entities;

using DSharpPlus.CommandsNext.Attributes;

namespace BolsoBot.Commands.Moderation;

[Group("Roles")]
[Description("Manage roles")]
[RequirePermissions(Permissions.ManageRoles)]
public class RolesCommands : BaseCommandModule
{
    [
        Command("addrole"),
        Description("Add a Role Someone"),
        RequirePermissions(Permissions.ManageRoles)
    ]
    public async Task AddRole(CommandContext ctx, DiscordMember member, DiscordRole role)
    {
        await ctx.TriggerTypingAsync().ConfigureAwait(false);
        try
        {
            var newRole = ctx.Guild.GetRole(role.Id);
            await member.GrantRoleAsync(newRole);
            await member.ModifyAsync(m => {
                m.AuditLogReason = $"Edited by {ctx.User.Username} {ctx.User.Id}";
            });
            await ctx.RespondAsync("Role added");
        }
        catch (Exception ex)
        {
            await ctx.RespondAsync("Could not assign role");
        }
    }

    [
        Command("removerole"),
        Description("Remove a Role From Someone")
    ]
    public async Task RemoveRole(CommandContext ctx, DiscordMember member, DiscordRole role)
    {
        await ctx.TriggerTypingAsync().ConfigureAwait(false);

        try
        {
            var newRole = ctx.Guild.GetRole(role.Id);
            await member.RevokeRoleAsync(newRole);
            await member.ModifyAsync(m => {
                m.AuditLogReason = $"Edited by {ctx.User.Username} {ctx.User.Id}";
            });
            await ctx.RespondAsync("Role removed");
        }
        catch (System.Exception ex)
        {
            await ctx.RespondAsync("Could not remove");
        }
    }
}
