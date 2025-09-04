using System.Text.Json.Serialization;

class RespostaSefaz
{
    [JsonPropertyName("components")]
    public List<Servidor>? Componentes { get; set; }
}