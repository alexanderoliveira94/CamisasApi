using CamisasApi.Models.Enuns;

namespace CamisasApi.Models
{
public class Camisas
{
    public int Id { get; set; }
    public string ?Nome { get; set; }
    public decimal Valor { get; set; }
    public string ?Tamanho { get; set; }
    public ClasseEnum Classe { get; set; } //referencia das condições dentro da PASTA ENUNS
    public string Foto { get; set; }
}

}


