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

using System.Text;

using System.Threading.Tasks;

using DSharpPlus;

using DSharpPlus.CommandsNext;

using DSharpPlus.EventArgs;

using BolsoBot.Commands;

using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

namespace BolsoBot;

public class Bot
{
    public DiscordClient Client { get; private set; }

    public CommandsNextExtension Commands { get; private set;}

    public async Task RunAsync()
    {
        var json = "";
        using (var fs = File.OpenRead("config.json"))
        using (var sr = new StreamReader(fs, new UTF8Encoding(false)))
            json = await sr.ReadToEndAsync().ConfigureAwait(false);

        var configJson = JsonConvert.DeserializeObject<ConfigJson>(json);

        var config = new DiscordConfiguration
        {
            Token = configJson.Token,
            TokenType = TokenType.Bot,

            AutoReconnect = true,
            MinimumLogLevel = LogLevel.Debug
        };

        Client = new DiscordClient(config);
        

        var commandsConfig = new CommandsNextConfiguration
        {
            StringPrefixes = new string[] { configJson.CommandPrefix },
            EnableDms = false,
            EnableMentionPrefix = true
        };

        Commands = Client.UseCommandsNext(commandsConfig);

        // registering commands
        Commands.RegisterCommands<Moderation>();

        await Client.ConnectAsync();

        await Task.Delay(-1);
    }

    private Task OnClientReady(DiscordClient sender, ReadyEventArgs e)
    {
        sender.Logger.LogInformation("Client is ready to process events.");

        return Task.CompletedTask;
    }
}
