public class Contato
{
    public int ID { get; set; }
    public string Nome { get; set; }
    public string NumeroTelefone { get; set; }
    public string Grupo { get; set; }
    public string Anotacoes { get; set; }
    public string Endereco { get; set; }
    public string DataNascimento { get; set; }
    public string Email { get; set; }
    public Contato(string nome, string numeroTelefone, string email, string grupo, string anotacoes, string endereco, string dataNascimento)


    {



        Nome = nome;
        NumeroTelefone = numeroTelefone;
        Email = email;
        Grupo = grupo;
        Anotacoes = anotacoes;
        Endereco = endereco;
        DataNascimento = dataNascimento;


    }

    public override string ToString()
    {
        return $"ID: {ID}\nNome: {Nome}\nNúmero de Telefone: {NumeroTelefone}\nGrupo: {Grupo}\nAnotações: {Anotacoes}\nEndereço: {Endereco}\nData de Nascimento: {DataNascimento}\nEmail: {Email}";
    }

}