using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

public static class DiscordNotifier
{
    public static async Task EnviarMensagem(string webhookUrl, string mensagem)
    {
        using HttpClient client = new HttpClient();

        var payload = new
        {
            content = mensagem
        };

        var json = JsonSerializer.Serialize(payload);
        var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

        await client.PostAsync(webhookUrl, httpContent);
    }
}
