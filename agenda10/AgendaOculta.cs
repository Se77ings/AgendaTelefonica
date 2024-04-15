using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

public class AgendaOculta : AgendaTelefonica
{
    public string nomeArquivoOculto;

    public AgendaOculta(string nomeArquivo) : base()
    {
        nomeArquivoOculto = nomeArquivo;
        contatos = CarregarContatosDeJSON(GetCaminhoArquivo());
        proximoId = contatos.Count > 0 ? contatos.Max(c => c.ID) + 1 : 1;
    }

    public override string GetCaminhoArquivo()
    {
        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        return Path.Combine(desktopPath, $"{nomeArquivoOculto}.json");
    }
}
