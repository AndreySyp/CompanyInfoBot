using CompanyInfoBot.Models;
using System.Net;

namespace CompanyInfoBot;

/// <summary>
/// Класс с вспомогательными методами.
/// </summary>
public class ProgramHelpers
{
    /// <summary>
    /// Медот для получения данных компаний.
    /// </summary>
    /// <param name="inn">Номер ИНН.</param>
    /// <param name="key">Ключ для подключения к API.</param>
    /// <returns>Десериализованные данные компаний.</returns>
    public static CompanyData GetCompanyData(string inn, string key)
    {
        string url = $"https://api-fns.ru/api/search?q={inn}&filter=active&key={key}";
        string response;

        HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
        HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

        using (StreamReader streamReader = new(httpWebResponse.GetResponseStream()))
        {
            response = streamReader.ReadToEnd();
        }

        return Newtonsoft.Json.JsonConvert.DeserializeObject<CompanyData>(response);
    }
}
