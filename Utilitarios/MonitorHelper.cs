class MonitorHelper
{
    public static string NivelInstabilidade(string statusStr)
    {
        if (!decimal.TryParse(statusStr, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out var status))
            return "Desconhecido";

        if (status < 1.1250m) return "Normal <= 2s";
        else if (status < 1.2500m) return "Lento <= 5s";
        else if (status < 1.5000m) return "Muito Lento <= 30s";
        else if (status < 3.0000m) return "Timeout > 30s";
        else return "Erro";
    } // categoriza o nivel de instabilidade conforme faixa de status

    public static string ExtrairUF(string idWorker)
    {
        if (string.IsNullOrWhiteSpace(idWorker)) return "Desconhecido";
        var partes = idWorker.Split('_');
        return partes.Length > 0 ? partes[^1].ToUpper() : idWorker.ToUpper(); // pega a última parte (UF) e deixa como maiúsculo)
    }
}