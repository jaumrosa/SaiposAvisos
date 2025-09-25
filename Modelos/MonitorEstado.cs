using System.Text.Json.Serialization;

public class MonitorEstado
{
    [JsonPropertyName("id_worker")]
    public string? UF { get; set; }

    [JsonPropertyName("status")]
    public string? Status { get; set; }
}