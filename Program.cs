using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Threading;

class Program
{
    static readonly HttpClient client = new HttpClient();
    static readonly string discordWebhook = "WEBHOOKDISCORD"; // <--- webhook discord para envio dos avisos via chat
    static readonly Dictionary<string, DateTime?> erroDetectado = new();

    static readonly string[] UFs =
    {
        "ac", "al", "am", "ap", "ba", "ce", "df", "es", "go", "ma",
        "mg", "ms", "mt", "pa", "pb", "pe", "pi", "pr", "rj", "rn",
        "ro", "rr", "rs", "sc", "se", "sp", "to"
    };

    static async Task Main()
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("Monitor SEFAZ iniciado!\n");
        Console.ResetColor();

        while (true)
        {
            foreach (var uf in UFs)
            {
                await VerificarEstado(uf);
            }
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"{DateTime.Now}: 🔁 Verificação de servidores concluída.\n");
            Console.ResetColor();
            await Task.Delay(TimeSpan.FromSeconds(60)); // intervalo de 1 minuto antes de nova consulta
        }
    }

    static async Task VerificarEstado(string uf)
    {
        string url = $"https://monitor.tecnospeed.com.br/monitores?current=true&worker_id=sefaz_nfce_envio_{uf}";
        try
        {
            var resposta = await client.GetAsync(url);
            resposta.EnsureSuccessStatusCode();

            var json = await resposta.Content.ReadAsStringAsync();
            var dados = JsonSerializer.Deserialize<List<MonitorEstado>>(json);

            if (dados == null || dados.Count == 0)
                return;

            var estado = dados[0];
            int status = estado.Status;

            if (status == 1)
            {
                // voltou ao normal -> registra no console e avisa no discord
                if (erroDetectado.ContainsKey(uf))
                {
                    erroDetectado.Remove(uf);

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"{DateTime.Now}: ✅ {uf.ToUpper()} voltou ao normal.");
                    Console.ResetColor();

                    string mensagem = $"✅  __**Instabilidade na SEFAZ encerrada:**__  ✅\n\n" +
                                      $"**Estado:** {uf.ToUpper()}\n" +
                                      $"**Horário de Fim da instabilidade:** {DateTime.Now:HH:mm}";
                    await DiscordNotifier.EnviarMensagem(discordWebhook, mensagem);
                }
                return;
            }

            // status diferente de 1 = erro
            if (!erroDetectado.ContainsKey(uf))
            {
                erroDetectado[uf] = DateTime.Now;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"{DateTime.Now}: ⚠️  {uf.ToUpper()} em instabilidade — {MonitorHelper.DescreverStatus(status)}");
                Console.ResetColor();
            }
            else
            {
                var inicioErro = erroDetectado[uf];
                if (inicioErro.HasValue && (DateTime.Now - inicioErro.Value).TotalMinutes >= 3)
                {
                    // envia alerta no Discord
                    string mensagem = $"⚠️  __**Instabilidade na SEFAZ detectada: **__  ⚠️\n\n" +
                                      $"**Estado:** {uf.ToUpper()}\n" +
                                      $"**Horário de inicio da instabilidade:** {inicioErro.Value:HH:mm}";

                    await DiscordNotifier.EnviarMensagem(discordWebhook, mensagem);
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    Console.WriteLine($"{DateTime.Now}: 🚨 Alerta enviado no discord para {uf.ToUpper()}.");
                    Console.ResetColor();

                    // só reenvia se voltar ao normal e cair novamente
                    erroDetectado[uf] = null;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao consultar {uf.ToUpper()}: {ex.Message}");
        }
    }
}
