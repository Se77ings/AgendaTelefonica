using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;

public class AgendaTelefonica
{
    public List<Contato> contatos = new List<Contato>();
    public int proximoId;

    public AgendaTelefonica()
    {
        contatos = CarregarContatosDeJSON(GetCaminhoArquivo());
        proximoId = contatos.Count > 0 ? contatos.Max(c => c.ID) + 1 : 1;
    }

    public void AdicionarContato(Contato contato)
    {
        contato.ID = proximoId;
        contatos.Add(contato);
        proximoId++;
        SalvarContatosEmJSON(contatos, GetCaminhoArquivo());
    }

    public void ListarContatos()
    {
        Console.Clear();

        if (contatos.Count == 0)
        {
            Console.WriteLine("Não há contatos salvos para serem exibidos.");
        }
        else
        {
            Console.WriteLine("Lista de contatos:");

            foreach (var contato in contatos)
            {
                Console.WriteLine(contato);
                Console.WriteLine();
            }
        }

        Console.WriteLine("\nPressione Enter para voltar ao menu...");
        Console.ReadLine();
        Console.Clear();
    }

    public void EditarContato(int id)
    {
        
        if (contatos.Count == 0)
        {
            Console.WriteLine("Não há contatos salvos para editar.");
            Console.ReadLine() ;
            Console.Clear() ;
            return;
        }


        Contato contato = contatos.FirstOrDefault(c => c.ID == id);
        if (contato == null)
        {
            // Se o contato é null, nenhum contato com o ID fornecido foi encontrado
            Console.WriteLine("Nenhum contato pertence a esse ID.");
            Console.ReadLine();
            Console.Clear();
            return;
        }
        if (contato != null)
        {

            Console.Clear();
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
                    string novaDataNascimento;
                    do
                    {
                        Console.Write("Digite a nova data de nascimento (dd/MM/yyyy): ");
                        novaDataNascimento = Console.ReadLine();
                    } while (!ValidarDataNascimento(novaDataNascimento));


                    contato.DataNascimento = novaDataNascimento;

                    break;
                default:
                    Console.WriteLine("Opção inválida, você será redirecionado ao menu.");
                    Console.ReadLine();
                    Console.Clear();

                    return;

            }
        }
    }

    public void DeletarContato(int id)
    {
        if (contatos.Count == 0)
        {
            Console.WriteLine("Não existem contatos salvos para deletar.");
            Console.Clear();
            return;
        }

        Contato contato = contatos.FirstOrDefault(c => c.ID == id);
        if (contato != null)
        {
            contatos.Remove(contato);

            
            for (int i = 0; i < contatos.Count; i++)
            {
                contatos[i].ID = i + 1;
            }

            proximoId = contatos.Count > 0 ? contatos.Max(c => c.ID) + 1 : 1;

            SalvarContatosEmJSON(contatos, GetCaminhoArquivo());

            Console.WriteLine("Contato deletado com sucesso.");
            Console.ReadLine();
            Console.Clear();
        }
        else
        {
            Console.WriteLine("ID do contato não encontrado.");
            Console.ReadLine();
            Console.Clear();
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
                    contato.DataNascimento.IndexOf(termoPesquisa, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    contatosEncontrados.Add(contato);
                }
            }

            return contatosEncontrados;
        }

    public virtual  List<Contato> CarregarContatosDeJSON(string filePath)
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

    protected virtual void SalvarContatosEmJSON(List<Contato> contatos, string filePath)
        {
            string jsonString = JsonSerializer.Serialize(contatos, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, jsonString);
        }

    public virtual string GetCaminhoArquivo()
        {
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            return Path.Combine(desktopPath, "contatos.json");
        }

        public bool ValidarDataNascimento(string dataNascimento)
        {
            DateTime data;
            if (DateTime.TryParseExact(dataNascimento, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out data))
            {

                if (data <= DateTime.Today)
                {
                    return true;
                }
                else
                {
                    Console.WriteLine("Data inválida, digite novamente.");
                }
            }
            else
            {
                Console.WriteLine("Formato de data inválido, digite novamente no formato dd/MM/yyyy.");
            }
            return false;
        }

        public bool validaemail(string email)
        {

            string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, emailPattern);
        }

        public bool validanumero(string numero)
        {
            string pattern = @"^(?:(?:\(\d{2}\)\s?|\d{4,5}-?\d{4})|(?:\d{10,11}))$";

            Regex regex = new Regex(pattern);
            return regex.IsMatch(numero);
        }
    }