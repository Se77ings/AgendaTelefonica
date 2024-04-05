class Contato
{
    public int ID { get; set; }
    public string Nome { get; set; }
    public string NumeroTelefone { get; set; }
    public string Grupo { get; set; }
    public string Anotacoes { get; set; }
    public string Endereco { get; set; }
    public string Aniversario { get; set; }
    public string Email { get; set; }
    public Contato( string nome, string numeroTelefone, string email, string grupo, string anotacoes, string endereco, string aniversario )


    {


       
        Nome = nome;
        NumeroTelefone = numeroTelefone;
        Email = email;
        Grupo = grupo;
        Anotacoes = anotacoes;
        Endereco = endereco;
        Aniversario = aniversario;


    }

    public override string ToString()
    {
        return $"{Nome}: {NumeroTelefone}: {Grupo}: {Anotacoes}: {Endereco}: {Aniversario}: {Email}";
    }
}
