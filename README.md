# 🛰️ Monitor SEFAZ NFC-e
=======================

Script em **C#** para monitorar automaticamente os servidores da **SEFAZ NFC-e** (Nota Fiscal de Consumidor Eletrônica) de todos os estados brasileiros, usando os dados do **monitor da TecnoSpeed.\
O sistema pode enviar alertas para um canal do Discord via webhook.

* * * * *

## ⚙️ Como funciona:
----------------

-   Faz consultas a cada **1 minuto** aos servidores SEFAZ NFC-e de todas as UFs.

-   Verifica o campo `Status` retornado pela API da TecnoSpeed.

-   Considera uma **instabilidade relevante** apenas se durar **5 minutos ou mais**.

-   Quando o servidor volta ao normal:

    -   Se houve alerta → envia mensagem de encerramento no Discord.

    -   Se a falha foi curta → apenas registra no console (sem alerta).

* * * * *

## 🔧 Configuração
---------------

1.  Abra o arquivo `Program.cs`.

2.  Localize a linha abaixo:

    `static readonly string discordWebhook = "WEBHOOKDISCORD";`

3.  Substitua `"WEBHOOKDISCORD"` pelo **link do webhook do seu canal do Discord**.

* * * * *

## ▶️ Execução
-----------

Execute o projeto com:

`dotnet run`

Exemplo de saída no console:

`Monitor SEFAZ iniciado!

2025-10-27 19:02: ⚠️ PR em instabilidade --- Erro de comunicação
2025-10-27 19:07: 🚨 Alerta enviado no discord informando instabilidade para PR.
2025-10-27 19:12: ✅ PR voltou ao normal. - (Alerta enviado no discord informando fim da instabilidade.)`

* * * * *

## 📦 Dependências
---------------
-   .NET 8.0

* * * * *

## ⏱️ Intervalos
-------------

-   Verificação a cada **60 segundos** (`Task.Delay(TimeSpan.FromSeconds(60))`)

-   Instabilidade relevante após **5 minutos de persistência**

* * * * *

## 📌 Observações
--------------

-   Evite rodar múltiplas instâncias do monitor ao mesmo tempo.

-   Se o monitor deixar de enviar mensagens, verifique:

    -   Validade do webhook;

    -   Conectividade com o endpoint da TecnoSpeed;

    -   Logs no console para exceções.

* * * * *

> ✅ **Lembrete rápido:** ajuste o valor da variável `discordWebhook` em `Program.cs` antes de rodar o projeto.