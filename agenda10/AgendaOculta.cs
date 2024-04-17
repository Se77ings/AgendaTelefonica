using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

public class AgendaOculta : AgendaTelefonica
{
    public string nomeArquivoOculto;
    public readonly byte[] chave;
    public readonly byte[] vetorInicializacao;

    public AgendaOculta(string nomeArquivo, byte[] chave, byte[] vetorInicializacao) : base()
    {
        nomeArquivoOculto = nomeArquivo;
        this.chave = chave;
        this.vetorInicializacao = vetorInicializacao;
        contatos = CarregarContatosDeJSON(GetCaminhoArquivo());
        proximoId = contatos.Count > 0 ? contatos.Max(c => c.ID) + 1 : 1;
    }

    public override string GetCaminhoArquivo()
    {
        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        return Path.Combine(desktopPath, $"{nomeArquivoOculto}.json");
    }

    protected override void SalvarContatosEmJSON(List<Contato> contatos, string caminho)
    {
        string json = JsonSerializer.Serialize(contatos, new JsonSerializerOptions { WriteIndented = true });
        string dadosCriptografados = Criptografar(json);
        File.WriteAllText(caminho, dadosCriptografados);
    }

    public override List<Contato> CarregarContatosDeJSON(string caminho)
    {
        if (!File.Exists(caminho))
        {
            return new List<Contato>();
        }

        string dadosCriptografados = File.ReadAllText(caminho);
        string json = Descriptografar(dadosCriptografados);
        return JsonSerializer.Deserialize<List<Contato>>(json);
    }

    private string Criptografar(string textoPlano)
    {
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = chave;
            aesAlg.IV = vetorInicializacao;

            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                {
                    swEncrypt.Write(textoPlano);
                }
                return Convert.ToBase64String(msEncrypt.ToArray());
            }
        }
    }

    public string Descriptografar(string textoCriptografado)
    {
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = chave;
            aesAlg.IV = vetorInicializacao;

            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(textoCriptografado)))
            using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
            using (StreamReader srDecrypt = new StreamReader(csDecrypt))
            {
                return srDecrypt.ReadToEnd();
            }
        }
    }
}
