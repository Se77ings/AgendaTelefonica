using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

class AgendaTelefonica
{
    private List<Contato> contatos = new List<Contato>();

    public void AdicionarContato(Contato contato)
    {
        contatos.Add(contato);
    }

    public void ListarContatos()
    {
        Console.WriteLine("Lista de contatos:");
        foreach (var contato in contatos)
        {
            Console.WriteLine(contato);
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        AgendaTelefonica agenda = new AgendaTelefonica();

        while (true)
        {
            Console.WriteLine("\nEscolha uma opção:");
            Console.WriteLine("1 - Adicionar Contato");
            Console.WriteLine("2 - Listar Contatos");
            Console.WriteLine("3 - Editar contato");
            Console.WriteLine("4 - Pesquisar contato");
            Console.WriteLine("5 - Deletar contato");
            Console.WriteLine("6 - Sair");




            string escolha = Console.ReadLine();

            switch (escolha)
            {
                case "1":
                    Console.Clear();
                    Console.Write("Digite o nome do contato: ");
                    string nome = Console.ReadLine();

                    Console.Write("Digite o número de telefone: ");
                    string numero = Console.ReadLine();

                    Console.Write("Digite o endereço: ");
                    string endereco = Console.ReadLine();

                    Console.Write("Digite o email: ");
                    string email = Console.ReadLine();

                    Console.Write("Digite as anotações: ");
                    string anotacoes = Console.ReadLine();

                    Console.Write("Digite o grupo: ");
                    string grupo = Console.ReadLine();

                    Console.Write("Digite sua data de nascimento: ");
                    string aniversario = Console.ReadLine();
                    Contato contato = new Contato(nome, numero, email, grupo, anotacoes, endereco, aniversario);
                    agenda.AdicionarContato(contato);

                    // Carregar contatos existentes do arquivo JSON
                    List<Contato> contatosSalvos = CarregarContatosDeJSON("contatos.json");

                    // Adicionar o novo contato à lista de contatos salvos
                    contatosSalvos.Add(contato);

                    // Salvar a lista de contatos atualizada
                    SalvarContatosEmJSON(contatosSalvos, "contatos.json");

                    Console.WriteLine("\nContato adicionado com sucesso!\n\nPresione enter para voltar ao menu.");
                    Console.ReadLine();
                    Console.Clear();
                    break;
                case "2":
                    agenda.ListarContatos();
                    Console.WriteLine("\n\nPressione enter para voltar ao menu.");
                    Console.ReadLine();
                    Console.Clear();
                    break;

                case "3":



                case "6":
                    Console.WriteLine("Saindo...");
                    return;

                default:
                    Console.WriteLine("Opção inválida. Pressione enter para voltar ao menu.");
                    Console.ReadLine();
                    Console.Clear();
                    break;
            }
        }
    }

    static List<Contato> CarregarContatosDeJSON(string nomeArquivo)
    {
        if (File.Exists(nomeArquivo))
        {
            string jsonString = File.ReadAllText(nomeArquivo);
            return JsonSerializer.Deserialize<List<Contato>>(jsonString);
        }
        else
        {
            return new List<Contato>();
        }
    }

    static void SalvarContatosEmJSON(List<Contato> contatos, string nomeArquivo)
    {
        string jsonString = JsonSerializer.Serialize(contatos, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(nomeArquivo, jsonString);
    }
}


