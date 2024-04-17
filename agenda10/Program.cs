using System.Security.Cryptography;
using System.Text;

class Program
{

    public static readonly byte[] chave = Encoding.UTF8.GetBytes("0123456789ABCDEF0123456789ABCDEF");
    public static readonly byte[] vetorInicializacao = Encoding.UTF8.GetBytes("0123456789ABCDEF");
    static void Main(string[] args)
    {

 
    AgendaTelefonica agenda = new AgendaTelefonica();
    AgendaOculta agendaOculta = new AgendaOculta("AgendaOculta", chave, vetorInicializacao);

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
                        Console.ReadLine();
                        Console.Clear();
                    }

                    break;

                case "4":
                    Console.Clear(); 
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

                case "5":
                    if (agenda.contatos.Count == 0)
                    {
                        Console.WriteLine("Não existem contatos salvos para deletar.");
                        Console.ReadLine();
                        Console.Clear();
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
                            Console.ReadLine();
                            Console.Clear();
                        }
                    }
                    break;
                case "6":
                    Console.WriteLine("Saindo...");
                    return;
                case "9":
                    
                    Console.Clear();
                    Console.Write("Digite a senha para acessar a agenda oculta: ");
                    string senhaDigitada = Console.ReadLine();

                    if (senhaDigitada == senhaAgendaOculta)
                    {
                        
                        
                        Console.Clear();
                        Console.WriteLine("Acesso à agenda oculta concedido.");

                        IniciarAgendaOculta(agendaOculta);
                    }
                    else
                    {
                        Console.WriteLine("Senha incorreta. Acesso negado.");
                    }
                    break;
                default:
                    Console.WriteLine("Opção inválida.");
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
            if (!agenda.validanumero(numero))
            {
                Console.WriteLine("Número de telefone inválido. Por favor, tente novamente.");
            }
        } while (!agenda.validanumero(numero));

        Console.Write("Digite o endereço: ");
        string endereco = Console.ReadLine();

        string email;
        do
        {
            Console.Write("Digite o email: ");
            email = Console.ReadLine();
            if (!agenda.validaemail(email))
            {
                Console.WriteLine("O e-mail fornecido não é válido. Por favor, tente novamente.");
            }
        } while (!agenda.validaemail(email));

        Console.Write("Digite as anotações: ");
        string anotacoes = Console.ReadLine();

        Console.Write("Digite o grupo: ");
        string grupo = Console.ReadLine();

        string dataNascimento;
        do
        {
            Console.Write("Digite a data de nascimento (dd/MM/yyyy): ");
            dataNascimento = Console.ReadLine();
        } while (!agenda.ValidarDataNascimento(dataNascimento));
        Contato contato = new Contato(nome, numero, email, grupo, anotacoes, endereco, dataNascimento);
        agenda.AdicionarContato(contato);

        Console.WriteLine("\nContato adicionado com sucesso!\n\nPressione enter para voltar ao menu.");
        Console.ReadLine();
        Console.Clear();
    }

    // Método para iniciar a agenda oculta
    static void IniciarAgendaOculta(AgendaOculta agenda)
    {
        bool sair = false;
        while (!sair)
        {
            Console.WriteLine("\nMenu da Agenda Oculta:");
            Console.WriteLine("1 - Adicionar Contato");
            Console.WriteLine("2 - Listar Contatos");
            Console.WriteLine("3 - Editar contato");
            Console.WriteLine("4 - Pesquisar contato");
            Console.WriteLine("5 - Deletar contato");
            Console.WriteLine("0 - Voltar ao menu principal");

            string escolha = Console.ReadLine();

            switch (escolha)
            {
                case "1":
                    AdicionarContato(agenda);
                    break;
                case "2":

                    
                    agenda.ListarContatos();
                    

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
                        Console.ReadLine();
                        Console.Clear();
                    }
                    break;
                case "4":
                    Console.Clear();
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
                case "5":
                    if (agenda.contatos.Count == 0)
                    {
                        Console.WriteLine("Não existem contatos salvos para deletar.");
                        Console.ReadLine();
                        Console.Clear();
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
                            Console.ReadLine();
                            Console.Clear();

                        }
                    }
                    break;
                case "0":
                    Console.WriteLine("Voltando ao menu da Agenda Telefonica");
                    sair = true;
                    Console.ReadLine();
                    Console.Clear();
                    break;
                default:
                    Console.WriteLine("Opção inválida.");
                    Console.ReadLine();
                    Console.Clear();
                    break;
            }
        }


    }


}
