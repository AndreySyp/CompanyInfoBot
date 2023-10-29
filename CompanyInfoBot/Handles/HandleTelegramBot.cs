using CompanyInfoBot.Models;
using Telegram.Bot.Exceptions;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace CompanyInfoBot.Handles;

/// <summary>
/// Класс содержащий методы для ошибок и обновления бота.
/// </summary>
public class HandleTelegramBot
{
    private readonly Settings settings = new();
    /// <summary>
    /// Задать настройки.
    /// </summary>
    /// <param name="settings">Настройки.</param>
    public HandleTelegramBot(Settings settings)
    {
        this.settings = settings;
    }

    /// <summary>
    /// Метод для вывода ошибок в боте.
    /// </summary>
    /// <param name="botClient">Бот клиент.</param>
    /// <param name="exception">Ошибка.</param>
    /// <param name="cancellationToken">Токен для остановки.</param>
    /// <returns></returns>
    public static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        var ErrorMessage = exception switch
        {
            ApiRequestException apiRequestException
                => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
            _ => exception.ToString()
        };

        Console.WriteLine(ErrorMessage);
    }

    /// <summary>
    /// Метод для реагирования на слбытия
    /// </summary>
    /// <param name="botClient">Бот клиент.</param>
    /// <param name="update">Событие.</param>
    /// <param name="cancellationToken">Токен для остановки.</param>
    /// <returns></returns>
    public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        var message = update.Message;
        if (message == null) return;
        if (message.Text == null) return;

        int spaceSymbol = message.Text.IndexOf(' ');
        if (spaceSymbol == -1) spaceSymbol = 0;
        switch (message.Text[..spaceSymbol])
        {
            case "/help":
                await botClient.SendTextMessageAsync(message.Chat.Id,
                                  "/help – вывести справку о доступных командах.\n" +
                                  "/hello – мое имя и фамилия,email, и дата получения задания.\n" +
                                  "/inn – получить наименования и адреса компаний по ИНН.",
                                  cancellationToken: cancellationToken);
                break;

            case "/hello":
                await botClient.SendTextMessageAsync(message.Chat.Id,
                                  settings.Info,
                                  cancellationToken: cancellationToken);
                break;

            case "/inn":
                string[] inns = message.Text.Replace("/inn ", "").Split(' ', ',');

                foreach (var inn in inns)
                {
                    CompanyData data = ProgramHelpers.GetCompanyData(inn, settings.FNSToken);
                    var сompanies = data.Items.Where(x => inn == x.LE.INN);

                    foreach (var company in сompanies)
                    {
                        await botClient.SendTextMessageAsync(message.Chat.Id,
                                                          $"ИНН - {company.LE.INN}\n" +
                                                          $"Название компании - {company.LE.CompanyName}\n" +
                                                          $"Адресс - {company.LE.Adress}",
                                                          cancellationToken: cancellationToken);
                    }
                }
                break;

            default :
                await botClient.SendTextMessageAsync(message.Chat.Id, "Неизвестная команда", cancellationToken: cancellationToken);
                break;
        }
    }
}
