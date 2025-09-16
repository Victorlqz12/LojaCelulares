using System;
using System.IO;
using System.Collections.Generic;

//               SALVAR PRODUTOS 
public static void SalvarProdutos(List<Produto> produtos)
{
    Directory.CreateDirectory("Data"); 
    using (StreamWriter sw = new StreamWriter("Data/produtos.txt"))
    {
        foreach (var p in produtos)
        {
            sw.WriteLine($"{p.Id};{p.Nome};{p.Categoria};{p.Quantidade};{p.Preco}");
        }
    }
}

//               CARREGAR PRODUTOS 
public static List<Produto> CarregarProdutos() {
    List<Produto> produtos = new List<Produto>();

    if (!File.Exists("Data/produtos.txt"))
        return produtos;

    foreach (var linha in File.ReadAllLines("Data/produtos.txt")) {
        var partes = linha.Split(';');
        Produto p;
        p.Id = int.Parse(partes[0]);
        p.Nome = partes[1];
        p.Categoria = partes[2];
        p.Quantidade = int.Parse(partes[3]);
        p.Preco = double.Parse(partes[4]);
        produtos.Add(p);
    }
    return produtos;
}
