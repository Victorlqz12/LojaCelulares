using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Program
{
    //           CAMADA DE DADOS (Declaração de Structs) 
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

    //              DECLARAÇÃO DE LISTAS
    static List<Produto> produtos = new List<Produto>();
    static List<Venda> vendas = new List<Venda>();

    //          CAMADA DE PERSISTÊNCIA (MANIPULAÇÃO DE ARQUIVOS)
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
    public static List<Produto> CarregarProdutos()
    {
        List<Produto> produtos = new List<Produto>();

        if (File.Exists("Data/produtos.txt"))
            return produtos;

        foreach (var linha in File.ReadAllLines("Data/produtos.txt"))
        {
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
    //              SALVAR VENDAS
    public static void SalvarVendas()
    {
        using (StreamWriter sw = new StreamWriter("Data/vendas.txt"))
        {
            foreach (var p in produtos)
            {
                sw.WriteLine($"{p.IdVenda};{p.NomeProduto};{p.Categoria};{p.QuantidadeVendida};{p.ValorTotal};{p.DataVenda}");
            }
        }
    }
    //              CARREGAR VENDAS
    public static List<Venda> CarregarVendas()
    {
        List<Venda> vendas = new List<Venda>();

        if (File.Exists("Data/vendas.txt"))
            return vendas;

        foreach (var linha in File.ReadAllLines("Data/produtos.txt"))
        {
            var partes = linha.Split(';');
            Venda p;
            p.IdVenda = int.Parse(partes[0]);
            p.NomeProduto = partes[1];
            p.Categoria = partes[2];
            p.QuantidadeVendida = int.Parse(partes[3]);
            p.ValorTotal = double.Parse(partes[4]);
            p.DataVenda = DateTime.Parse(partes[5]);
            produtos.Add(p);
        }
        return produtos;
    }


//              CAMADA DE LÓGICA
//              MANIPULAAÇÃO DE PRODUTOS
    public static void CadastrarProduto() { }

    public static List<Produto> ConsultarProdutos() { }

    public static void ExcluirProdutos() { }


    public void realizarVenda() { }

    //CAMADA DE INTERFACE
    static void Main(string[] args)
    {
        produtos = CarregarProdutos();
        vendas = CarregarVendas();

        while (true)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("=== LOJA CELULARXPRESS === ");
            Console.ResetColor();
            Console.WriteLine("1 - Cadastrar Produto");
            Console.WriteLine("2 - Realizar Venda");
            Console.WriteLine("3 - Consultar Produto");
            Console.WriteLine("4 - Excluir Produto");
            Console.WriteLine("5 - Relatorio de Vendas");
            Console.WriteLine("0 - Sair");

            Console.WriteLine("Escolha: ");
            string opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1": /* CadastrarProduto(produtos) */ break;
                case "2": /* RealizarVenda(produtos, vendas) */ break;
                case "3": /* ConsultarProduto(produtos) */ break;
                case "4": /* ExcluirProduto(produtos) */ break;
                case "5": /* GerarRelatorios(vendas) */ break;
                case "0": return;
                default: Console.WriteLine("Opção inválida"); break;
            }
            Console.WriteLine("Pressione uma tecla para continuar...");
            Console.ReadKey();



        }



    }
}
    

    