class LinqFilter
{
    public static void FiltrarNFC(RespostaSefaz servidores)
    {
        var ServidoresNFC = servidores.Componentes?.Where(servidor => servidor.Nome.Contains("Autorizadores de NFC-e"));

        foreach (var servidor in ServidoresNFC)
        {
            Console.WriteLine($"{servidor.Nome} - {servidor.Status}");
        }
    }
}