class LinqFilter
{
    public static void FiltrarNFC(RespostaSefaz servidores)
    {
        var autorizadorNFC = servidores.Componentes?.FirstOrDefault(s => s.Nome != null && s.Nome.Contains("Autorizadores de NFC-e", StringComparison.OrdinalIgnoreCase));

        if (autorizadorNFC != null && autorizadorNFC.Filhos != null)
        {
            Console.WriteLine($"=== {autorizadorNFC.Nome} ===");

            foreach (var filhoId in autorizadorNFC.Filhos)
            {
                var servidorFilho = servidores.Componentes?
                    .FirstOrDefault(s => s.Id == filhoId);

                if (servidorFilho != null)
                {
                    Console.WriteLine($"{servidorFilho.Nome} - {servidorFilho.Status}");
                }
            }
        }
        else
        {
            Console.WriteLine("Nenhum autorizador NFC-e encontrado.");
        }
    }
}
