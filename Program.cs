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

using BolsoBot.Commands.Moderation;

using BolsoBot.Commands.Utility;

using Microsoft.Extensions.Logging;

using Microsoft.Extensions.Configuration;

namespace BolsoBot;

public class Program
{
    public static DiscordClient Client { get; private set; }

    public static CommandsNextExtension Commands { get; private set; }
    static async Task Main()
    {
        //read more about configuration builder here <https://docs.microsoft.com/en-us/dotnet/api/microsoft.extensions.configuration.configurationbuilder?view=dotnet-plat-ext-6.0>
        var builder = new ConfigurationBuilder();
        builder.AddJsonFile(Path.GetFullPath("config.json"), false, true);
       IConfigurationRoot root = builder.Build();

        var config = new DiscordConfiguration
        {
            Token = $"{root["token"]}",
            TokenType = TokenType.Bot,

            AutoReconnect = true,
            MinimumLogLevel = LogLevel.Debug
        };

        Client = new DiscordClient(config);

        var commandsConfig = new CommandsNextConfiguration
        {
            StringPrefixes = new string[] { $"{root["prefix"]}" },
            EnableDms = false,
            EnableMentionPrefix = true
        };

        Commands = Client.UseCommandsNext(commandsConfig);

        // registering commands
        Commands.RegisterCommands(typeof(Program).Assembly);
        // connect to server and log in
        await Client.ConnectAsync();
        // this is to prevent premature quitting
        await Task.Delay(-1);
    }

}