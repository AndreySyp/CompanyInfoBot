using Microsoft.Extensions.Configuration;
using CompanyInfoBot.Models;

namespace CompanyInfoBot.Infrastructure;

/// <summary>
/// Класс необходимы для получения первоночальных настроек.
/// </summary>
public class Startup
{
    /// <summary>
    /// Конструктор, который при запуске читает файл с настройками и ключами API и сохранят их
    /// </summary>
    public Startup()
    {
        var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false);

        IConfiguration config = builder.Build();

        Settings = config.Get<Settings>();
    }

    /// <summary>
    /// Настройки и ключи API.
    /// </summary>
    public Settings Settings { get; private set; }
}
