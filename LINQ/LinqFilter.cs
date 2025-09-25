using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;

public static class LinqFilter
{
    public static List<MonitorEstado> FiltrarInstaveis(List<MonitorEstado> estados)
    {
        if (estados == null) return new List<MonitorEstado>();

        return estados
            .Where(e =>
            {
                bool parseOk = decimal.TryParse(e.Status, NumberStyles.Any, CultureInfo.InvariantCulture, out var s);
                return parseOk && s >= 1.1250m; //a partir de qual status considerado instabilidade
            })
            .ToList();
    }
}

/*

Mapeamento de status por lentidão tecnospeed:

"status":"1.0000' = Normal <= 2s 
"status":"1.1250" = Lento <= 5s 
"status":"1.2500" = Muito Lento <= 30s 
"status":"1.5000" = Timeout > 30s 
"status":"3.0000" = Erro

*/