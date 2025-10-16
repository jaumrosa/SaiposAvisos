using System;
using System.Text.Json.Serialization;

public class MonitorEstado
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("id_worker")]
    public string? Id_Worker { get; set; }

    [JsonPropertyName("status")]
    public int Status { get; set; }

    [JsonPropertyName("datahora")]
    public DateTime DataHora { get; set; }

    [JsonPropertyName("tempo")]
    public int Tempo { get; set; }

    [JsonPropertyName("erro")]
    public string? Erro { get; set; }
}
