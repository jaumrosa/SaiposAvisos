using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using System.Text.Json;

class Program
{
    static async Task Main()
    {
        using HttpClient client = new HttpClient();

        while (true)
        {
            try
            {
                var resposta = await client.GetAsync("https://monitor.tecnospeed.com.br/monitores?stateStatus=true&doc=nfce&worker_id=sefaz_nfce_envio_al");
                resposta.EnsureSuccessStatusCode();

                var json = await resposta.Content.ReadAsStringAsync();
                //confirma se retorno da API está funcionando corretamente 
                Console.WriteLine($"{DateTime.Now}: Status da API {resposta.StatusCode}");

                var estados = JsonSerializer.Deserialize<List<MonitorEstado>>(json);

                if (estados != null)
                {
                    var instaveis = LinqFilter.FiltrarInstaveis(estados);

                    if (instaveis.Count > 0)
                    {
                        System.Console.WriteLine($"{DateTime.Now}: ⚠️ Estados com instabilidade:");

                        foreach (var e in instaveis)
                        {
                            string uf = MonitorHelper.ExtrairUF(e.UF!);
                            string nivel = MonitorHelper.NivelInstabilidade(e.Status!);

                            //registra no console
                            System.Console.WriteLine($"{uf} | {nivel} | Status: {e.Status}");
                        }

                        //envia aviso de instabilidade no discord
                        string mensagem = "⚠️ __**Instabilidade na SEFAZ detectada:**__ ⚠️\n\n" +
                            string.Join("\n", instaveis.Select(e => 
                                $"**{MonitorHelper.ExtrairUF(e.UF!)}** | **{MonitorHelper.NivelInstabilidade(e.Status!)}** "));

                        await DiscordNotifier.EnviarMensagem("WEBHOOKDISCORD", mensagem); //ajustar webhook do canal do discord
                    }
                    else
                    {
                        System.Console.WriteLine($"{DateTime.Now}: ✅ Todos os estados estão estáveis.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
            }

            Thread.Sleep(60_000); // repete após 1 minuto (alterar pro tempo de verificação posteriormente)
        }
    }
}
