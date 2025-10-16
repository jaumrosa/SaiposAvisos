using System;

public static class MonitorHelper
{
    public static string DescreverStatus(int status)
    {
        return status switch
        {
            1 => "Normal",
            2 => "Lento",
            3 => "Muito Lento",
            4 => "Timeout",
            5 => "Erro",
        _   => "Desconhecido"
        };
    }

    public static string ExtrairUF(string idWorker)
    {
        if (string.IsNullOrWhiteSpace(idWorker)) return "Desconhecido";
        var partes = idWorker.Split('_');
        return partes.Length > 0 ? partes[^1].ToUpper() : idWorker.ToUpper();
    }
}
