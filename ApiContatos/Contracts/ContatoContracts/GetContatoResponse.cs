namespace ApiContatos.Contracts.ContatoContracts;

public class GetContatoResponse
{
    public int Id { get; set; }
    public string Nome { get; set; } = null!;
    public string Telefone { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Endereco { get; set; } = null!;
    public string Categoria { get; set; } = null!;
}
