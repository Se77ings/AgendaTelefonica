using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;






class AgendaTelefonica
{
    private List<Contato> contatos = new List<Contato>();
    private int proximoId;

    public AgendaTelefonica()
    {
        // Carregar contatos existentes do arquivo JSON ao iniciar a agenda
        contatos = CarregarContatosDeJSON(GetCaminhoArquivo());

        // Encontrar o maior ID existente para definir o próximo ID a ser usado
        proximoId = contatos.Count > 0 ? contatos.Max(c => c.ID) + 1 : 1;
    }

    public void AdicionarContato(Contato contato)
    {
        contato.ID = proximoId++; // Atribui o ID atual e incrementa para o próximo contato
        contatos.Add(contato);

        // Salvar contatos atualizados no arquivo JSON
        SalvarContatosEmJSON(contatos, GetCaminhoArquivo());
    }

    public void ListarContatos()
    {
        Console.WriteLine("Lista de contatos:");
        foreach (var contato in contatos)
        {
            Console.WriteLine(contato);
        }
    }

    static List<Contato> CarregarContatosDeJSON(string filePath)
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

    static void SalvarContatosEmJSON(List<Contato> contatos, string filePath)
    {
        string jsonString = JsonSerializer.Serialize(contatos, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(filePath, jsonString);
    }
    static bool validaemail (string email)
    {
        // Utilize uma expressão regular para validar o formato do e-mail
        string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        return Regex.IsMatch(email, emailPattern);
    }
    static bool validanumero(string numero)
    {
        string pattern = @"^(?:(?:\(\d{2}\)\s?|\d{4,5}-?\d{4})|(?:\d{10,11}))$";

        Regex regex = new Regex(pattern);
        return regex.IsMatch(numero);
    }

    static string GetCaminhoArquivo()
    {
        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        return Path.Combine(desktopPath, "contatos.json");
    }

    static void Main(string[] args)
    {
        AgendaTelefonica agenda = new AgendaTelefonica();
       

        string senhaAgendaOculta = "123";

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
                    AdicionarContato(agenda);
                    break;
                case "2":
                    agenda.ListarContatos();
                    Console.WriteLine("\n\nPressione enter para voltar ao menu.");
                    Console.ReadLine();
                    Console.Clear();
                    break;
                case "3":
                    Console.Write("Digite o ID do contato que deseja editar: ");
                   
                    if (int.TryParse(Console.ReadLine(), out int id))
                    {
                        agenda.EditarContato(id);
                        
                    }
                    else
                    {
                        Console.WriteLine("ID inválido.");
                    }
                    break;

                    case "4":
                
                    Console.Write("Digite o termo de pesquisa: ");
                    string termoPesquisa = Console.ReadLine();
                    List<Contato> contatosEncontrados = agenda.PesquisarContato(termoPesquisa);
                    if (contatosEncontrados.Count > 0)
                    {
                        Console.WriteLine("Contatos encontrados:");
                        foreach (var contato in contatosEncontrados)
                        {
                            Console.WriteLine(contato);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Nenhum contato encontrado com o termo de pesquisa.");
                    }
                    Console.WriteLine("\n\nPressione enter para voltar ao menu.");
                    Console.ReadLine();
                    Console.Clear();
                    break;

                    break;

                    case "5":
                    if (agenda.contatos.Count == 0)
                    {
                        Console.WriteLine("Não existem contatos salvos para deletar.");
                    }
                    else
                    {
                        Console.Write("Digite o ID do contato que deseja deletar: ");
                        if (int.TryParse(Console.ReadLine(), out int idDeletar))
                        {
                            agenda.DeletarContato(idDeletar);
                        }
                        else
                        {
                            Console.WriteLine("ID inválido.");
                        }
                    }
                    break;

                case "6":
                    Console.WriteLine("Saindo...");
                    return;

                case "9":
                    Console.Write("Digite a senha para acessar a agenda oculta: ");
                    string senhaDigitada = Console.ReadLine();

                    if (senhaDigitada == senhaAgendaOculta)
                    {
                        agenda.ListarContatos(); // Exibe a lista de contatos se a senha estiver correta
                        Console.WriteLine("\n\nPressione enter para voltar ao menu.");
                        Console.ReadLine();
                        Console.Clear();
                    }
                    else
                    {
                        Console.WriteLine("Senha incorreta. Acesso negado.");
                    }

                    break;

                default:
                    Console.WriteLine("Opção inválida. Pressione enter para voltar ao menu.");
                    Console.ReadLine();
                    Console.Clear();
                    break;


            }
        }
    }

    static void AdicionarContato(AgendaTelefonica agenda)
    {
        Console.Clear();
        Console.Write("Digite o nome do contato: ");
        string nome = Console.ReadLine();

        string numero;
        do
        {
            Console.Write("Digite o número de telefone: ");
            numero = Console.ReadLine();
            if (!validanumero(numero))
            {
                Console.WriteLine("Número de telefone inválido. Por favor, tente novamente.");
            }
        } while (!validanumero(numero)); // Repete até que o número seja válido

        Console.Write("Digite o endereço: ");
        string endereco = Console.ReadLine();

       
        

        string email;
        do
        {
            Console.Write("Digite o email: ");
            email = Console.ReadLine();
            if (!validaemail(email))
            {
                Console.WriteLine("O e-mail fornecido não é válido. Por favor, tente novamente.");
            }
        } while (!validaemail(email)); // Repete até que o número seja válido



        Console.Write("Digite as anotações: ");
        string anotacoes = Console.ReadLine();

        Console.Write("Digite o grupo: ");
        string grupo = Console.ReadLine();

        Console.Write("Digite sua data de nascimento: ");
        string aniversario = Console.ReadLine();
        Contato contato = new Contato(nome, numero, email, grupo, anotacoes, endereco, aniversario);
        agenda.AdicionarContato(contato);

        Console.WriteLine("\nContato adicionado com sucesso!\n\nPressione enter para voltar ao menu.");
        Console.ReadLine();
        Console.Clear();
    }

    public void EditarContato(int id)
    {
        Contato contato = contatos.FirstOrDefault(c => c.ID == id);
        if (contato != null)
        {
            Console.WriteLine(contato);
            Console.WriteLine("\nContato encontrado. Escolha qual campo deseja editar:");
            Console.WriteLine("1 - Nome");
            Console.WriteLine("2 - Número de telefone");
            Console.WriteLine("3 - Endereço");
            Console.WriteLine("4 - Email");
            Console.WriteLine("5 - Anotações");
            Console.WriteLine("6 - Grupo");
            Console.WriteLine("7 - Data de nascimento");

            string escolha = Console.ReadLine();
            switch (escolha)
            {
                case "1":
                    Console.Write("Novo nome: ");
                    contato.Nome = Console.ReadLine();
                    break;
                case "2":

                    Console.Write("Novo número de telefone: ");
                    string novoNumero;
                    do
                    {
                        novoNumero = Console.ReadLine();
                        if (validanumero(novoNumero))
                        {
                            contato.NumeroTelefone = novoNumero;
                        }
                        else
                        {
                            Console.WriteLine("Número de telefone inválido. Por favor, digite um número válido.");
                            Console.Write("Novo número de telefone: ");
                        }
                    } while (!validanumero(novoNumero));
                    break;
                case "3":
                    Console.Write("Novo endereço: ");
                    contato.Endereco = Console.ReadLine();
                    break;
                case "4":
                    string novoEmail;
                    do
                    {
                        Console.Write("Novo email: ");
                        novoEmail = Console.ReadLine();
                        if (validaemail(novoEmail))
                        {
                            contato.Email = novoEmail;
                        }
                        else
                        {
                            Console.WriteLine("O e-mail fornecido não é válido. Por favor, tente novamente.");
                        }
                    } while (!validaemail(novoEmail));
                    break;
                case "5":
                    Console.Write("Novas anotações: ");
                    contato.Anotacoes = Console.ReadLine();
                    break;
                case "6":
                    Console.Write("Novo grupo: ");
                    contato.Grupo = Console.ReadLine();
                    break;
                case "7":
                    Console.Write("Nova data de nascimento: ");
                    contato.Aniversario = Console.ReadLine();
                    break;
                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }

            // Salvar contatos atualizados no arquivo JSON
            SalvarContatosEmJSON(contatos, GetCaminhoArquivo());

            Console.WriteLine("Contato editado com sucesso.");
        }
        else
        {
            Console.WriteLine("Contato não encontrado.");
            
        }
    }
    public void DeletarContato(int id)
    {
        if (contatos.Count == 0)
        {
            Console.WriteLine("Não existem contatos salvos para deletar.");
            return;
        }

        Contato contato = contatos.FirstOrDefault(c => c.ID == id);
        if (contato != null)
        {
            contatos.Remove(contato);

            // Reorganizar os IDs para que sejam contínuos
            for (int i = 0; i < contatos.Count; i++)
            {
                contatos[i].ID = i + 1;
            }

            // Salvar contatos atualizados no arquivo JSON
            SalvarContatosEmJSON(contatos, GetCaminhoArquivo());

            Console.WriteLine("Contato deletado com sucesso.");
        }
        else
        {
            Console.WriteLine("ID do contato não encontrado.");
        }
    }


    public List<Contato> PesquisarContato(string termoPesquisa)
    {
        List<Contato> contatosEncontrados = new List<Contato>();

        foreach (var contato in contatos)
        {
            if (contato.Nome.IndexOf(termoPesquisa, StringComparison.OrdinalIgnoreCase) >= 0 ||
                contato.NumeroTelefone.IndexOf(termoPesquisa, StringComparison.OrdinalIgnoreCase) >= 0 ||
                contato.Endereco.IndexOf(termoPesquisa, StringComparison.OrdinalIgnoreCase) >= 0 ||
                contato.Email.IndexOf(termoPesquisa, StringComparison.OrdinalIgnoreCase) >= 0 ||
                contato.Anotacoes.IndexOf(termoPesquisa, StringComparison.OrdinalIgnoreCase) >= 0 ||
                contato.Grupo.IndexOf(termoPesquisa, StringComparison.OrdinalIgnoreCase) >= 0 ||
                contato.Aniversario.IndexOf(termoPesquisa, StringComparison.OrdinalIgnoreCase) >= 0)
            {
                contatosEncontrados.Add(contato);
            }
        }

        return contatosEncontrados;
    }




}