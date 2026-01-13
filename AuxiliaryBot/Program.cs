using AuxiliaryBot;
using AuxiliaryBot.Configuration;
using AuxiliaryBot.Controllers;
using AuxiliaryBot.Services;
using AuxiliaryBot.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Telegram.Bot;

var host = new HostBuilder()
.ConfigureAppConfiguration((hostingContext, config) =>
{
    config.SetBasePath(Directory.GetCurrentDirectory());
    config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
})
.ConfigureServices((context, services) =>
{
    services.Configure<AppSettings>(
        context.Configuration.GetSection("AppSettings")
    );

    services.AddSingleton<IStorage, MemoryStorageService>();
    services.AddSingleton<IAction, ActionService>();

    services.AddTransient<DefaultMessageController>();
    services.AddTransient<TextMessageController>();
    services.AddTransient<InlineKeyboardController>();

    services.AddSingleton<ITelegramBotClient>(provider => {
        var appSettings = provider.GetRequiredService<IOptions<AppSettings>>().Value;
        return new TelegramBotClient(appSettings.BotToken);
    });

    services.AddHostedService<Bot>();
})
.UseConsoleLifetime()
.Build();

await host.RunAsync();