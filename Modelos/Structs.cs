
using System;

public struct Produto
{
    public int Id;
    public string Nome;
    public string Categoria;
    public int Quantidade;
    public double Preco;
}

public struct Venda
{
    public int IdVenda;
    public string NomeProduto;
    public string Categoria;
    public int QuantidadeVendida;
    public double ValorTotal;
    public DateTime DataVenda;
}
