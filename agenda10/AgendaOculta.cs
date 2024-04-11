using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

public class AgendaOculta : AgendaTelefonica
{
    private string arquivoAgendaOculta;

    public AgendaOculta(string arquivo)
    {
        arquivoAgendaOculta = arquivo;
        CarregarContatosDeJSON(arquivoAgendaOculta);
    }

    public override void SalvarContatosEmJSON(List<Contato> contatos, string filePath)
    {
        string jsonString = JsonSerializer.Serialize(contatos, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(arquivoAgendaOculta, jsonString);
    }

    public override List<Contato> CarregarContatosDeJSON(string filePath)
    {
        if (File.Exists(filePath))
        {
            string jsonString = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<Contato>>(jsonString);
        }
        else
        {
            return new List<Contato>();
        }
    }

    public new void ListarContatos()
    {
        base.ListarContatos();
        string novoArquivo = Path.Combine(Path.GetDirectoryName(arquivoAgendaOculta), "novo_agenda_oculta.json");
        SalvarContatosEmJSON(contatos, novoArquivo);
        Console.WriteLine($"Contatos listados em: {novoArquivo}");
    }
}