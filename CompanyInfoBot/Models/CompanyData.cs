using Newtonsoft.Json;

namespace CompanyInfoBot.Models;

/// <summary>
/// Класс необходимы для получентя данных о компании.
/// </summary>
public class CompanyData
{
    public Item[] Items { get; set; }
    public long Count { get; set; }
}

/// <summary>
/// Класс необходимы для получентя данных о компании.
/// </summary>
public class Item
{
    [JsonProperty("ЮЛ")]
    public LE LE { get; set; }
}

/// <summary>
/// Класс необходимы для получентя данных о компании.
/// </summary>
public class LE
{
    [JsonProperty("ИНН")]
    public string INN { get; set; }

    [JsonProperty("НаимСокрЮЛ", NullValueHandling = NullValueHandling.Ignore)]
    public string CompanyName { get; set; }

    [JsonProperty("НаимПолнЮЛ")]
    public string CompanyNameFull { get; set; }

    [JsonProperty("АдресПолн")]
    public string Adress { get; set; }
}
