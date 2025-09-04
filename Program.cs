using System.Text.Json;

using (HttpClient client = new HttpClient())
{
    string resposta = await client.GetStringAsync("https://monitorsefaz.webmaniabr.com/v2/components.json");
    var resultado = JsonSerializer.Deserialize<RespostaSefaz>(resposta)!;

    if (resultado?.Componentes != null)
    {
        foreach (var servidor in resultado.Componentes)
        {
            System.Console.WriteLine($"{servidor.Nome} - {servidor.Status}");
        }
    }
}