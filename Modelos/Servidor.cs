using System.Text.Json.Serialization;

class Servidor
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("name")]
    public string? Nome { get; set; }

    [JsonPropertyName("status")]
    public string? Status { get; set; }
    [JsonPropertyName("description")]
    public string? Descricao { get; set; }
    [JsonPropertyName("isParent")]
    public bool EPai { get; set; }
    [JsonPropertyName("children")]
    public string[]? Filhos { get; set; }


}


