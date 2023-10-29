using CompanyInfoBot.Handles;
using CompanyInfoBot.Infrastructure;
using Telegram.Bot;

namespace CompanyInfoBot;

internal partial class Program
{
    private static readonly Startup startup = new();

    private static void Main(string[] args)
    {
        TelegramBotClient botClient = new(startup.Settings.BotToken);
        HandleTelegramBot handleTelegramBot = new(startup.Settings); 
        botClient.StartReceiving(handleTelegramBot.HandleUpdateAsync, HandleTelegramBot.HandleErrorAsync);

        Console.WriteLine("Started");
        Console.ReadLine();
    }
}
