using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Principal;
using System.Net;
using System.Globalization;

class Program
{
    //           CAMADA DE DADOS (Declaração de Structs) 
    public struct Produto
    {
        public int Id;
        public string Nome;
        public string Categoria;
        public int Quantidade;
        public decimal Preco;

        public Produto(int id, string nome, string categoria, int quantidade, decimal preco) 
        {
            Id = id;
            Nome = nome;
            Categoria = categoria;
            Quantidade = quantidade;
            Preco = preco;
        }
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
    public static void SalvarProdutos()
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

    public static void SalvarVendas()
    {
        Directory.CreateDirectory("Data");
        using (StreamWriter sw = new StreamWriter("Data/vendas.txt"))
        {
            foreach (var v in vendas)
            {
                sw.WriteLine($"{v.IdVenda};{v.NomeProduto};{v.Categoria};{v.QuantidadeVendida};{v.ValorTotal};{v.DataVenda}");
            }
        }
    }

    //               CARREGAR PRODUTOS 
    public static List<Produto> CarregarProdutos()
    {
        List<Produto> produtos = new List<Produto>();

        if (!File.Exists("Data/produtos.txt"))
            return produtos;

        foreach (var linha in File.ReadAllLines("Data/produtos.txt"))
        {
            var partes = linha.Split(';');
            Produto p = new Produto(
                int.Parse(partes[0]),
                partes[1],
                partes[2],
                int.Parse(partes[3]),
                decimal.Parse(partes[4])
            );
            produtos.Add(p);
        }
        return produtos;
    }
    
    //              CARREGAR VENDAS
    public static List<Venda> CarregarVendas()
    {
        List<Venda> vendas = new List<Venda>();

        if (!File.Exists("Data/vendas.txt"))
            return vendas;

        foreach (var linha in File.ReadAllLines("Data/vendas.txt"))
        {
            var partes = linha.Split(';');
            Venda v = new Venda();
            v.IdVenda = int.Parse(partes[0]);
            v.NomeProduto = partes[1];
            v.Categoria = partes[2];
            v.QuantidadeVendida = int.Parse(partes[3]);
            v.ValorTotal = double.Parse(partes[4]);
            v.DataVenda = DateTime.Parse(partes[5]);
            vendas.Add(v);
        }
        return vendas;
    }


//              CAMADA DE LÓGICA
//              MANIPULAAÇÃO DE PRODUTOS
    public static void CadastrarProduto() 
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("╔════════════════════════════════════════════════════════════════════════╗");
        Console.WriteLine("║                        LOJA CELULARXPRESS                              ║");
        Console.WriteLine("║                        Cadastro de Produto                             ║");
        Console.WriteLine("╠════════════════════════════════════════════════════════════════════════╣");
        Console.ResetColor();

        Console.Write("\n  Digite o Id do produto: ");
        int id;        
        while (!int.TryParse(Console.ReadLine(), out id) || id < 0) //Valida se o valor é um int
    {
        Console.WriteLine("Id inválido. Por favor, é necessário digitar um número inteiro positivo.");
        Console.Write("Digite novamente o Id: ");
    }

        Console.Write("Digite o nome do produto: ");
        string nome = Console.ReadLine();

        while (string.IsNullOrWhiteSpace(nome)) //Valida se o valor é nulo
        {
            Console.WriteLine("Nome inválido!");
            Console.Write("Digite novamente o nome do produto: ");
            nome = Console.ReadLine();
        }

        Console.Write("Digite a categoria do produto: ");
        string categoria = Console.ReadLine();

        while (string.IsNullOrWhiteSpace(categoria)) 
        {
            Console.WriteLine("Categoria inválida!.");
            Console.Write("Digite novamente a categoria do produto: ");
            categoria = Console.ReadLine();
        }

        int quantidade;
        Console.Write("Digite a quantidade: ");
        while (!int.TryParse(Console.ReadLine(), out quantidade) || quantidade < 0) 
        {
            Console.WriteLine("Quantidade inválida. Por favor, é necessário digitar um número inteiro positivo!");
            Console.Write("Digite novamente a quantidade: ");
        }

        decimal preco;
        Console.Write("Digite o valor (ex: 19,99): ");
        while (!decimal.TryParse(Console.ReadLine(), NumberStyles.Any, CultureInfo.CurrentCulture, out preco) || preco < 0)
        {
            Console.WriteLine("Valor inválido. Por favor, é necessário digitar um número positivo (use a vírgula como separador).");
            Console.Write("Digite o valor: ");
        }

        Produto novoProduto = new Produto(id, nome, categoria, quantidade, preco);
        produtos.Add(novoProduto);

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Produto adicionado com sucesso!");
        Console.ResetColor();
        SalvarProdutos();
    }

    public static void ConsultarProdutos()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("╔════════════════════════════════════════════════════════════════════════╗");
        Console.WriteLine("║                        LOJA CELULARXPRESS                              ║");
        Console.WriteLine("║                        Consulta de Produtos                            ║");
        Console.WriteLine("╠════════════════════════════════════════════════════════════════════════╣");
        Console.ResetColor();

        if (produtos.Count == 0)
        {
            Console.WriteLine("\n    Nenhum produto cadastrado.");
            return;
        }

        foreach (var p in produtos)
        {
            Console.WriteLine("\n   ----------------------------------------");
            Console.WriteLine($"    Id: {p.Id}");
            Console.WriteLine($"    Nome: {p.Nome}");
            Console.WriteLine($"    Categoria: {p.Categoria}");
            Console.WriteLine($"    Quantidade: {p.Quantidade}");
            Console.WriteLine($"    Preço: {p.Preco:C}");
            Console.WriteLine("   ---------------------------------------");
        }

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("╚════════════════════════════════════════════════════════════════════════╝");
        Console.ResetColor();
    }
          
    public static void ExcluirProdutos()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("╔════════════════════════════════════════════════════════════════════════╗");
        Console.WriteLine("║                        LOJA CELULARXPRESS                              ║");
        Console.WriteLine("║                        Excluir Produto                                 ║");
        Console.WriteLine("╠════════════════════════════════════════════════════════════════════════╣");
        Console.ResetColor();

        Console.WriteLine("\n   Digite o Id do produto que deseja excluir: ");
        if (!int.TryParse(Console.ReadLine(), out int idParaExcluir))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n   ❌ Id inválido. Por favor, digite um número.");
            Console.ResetColor();
            return;
        }

        int index = produtos.FindIndex(p => p.Id == idParaExcluir);
        if (index != -1)
        {
            produtos.RemoveAt(index);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n   ✔ Produto excluído com sucesso!");
            Console.ResetColor();
            SalvarProdutos();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n   ❌ Produto não encontrado.");
            Console.ResetColor();
        }

    }


    public static void RealizarVenda()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("╔════════════════════════════════════════════════════════════════════════╗");
        Console.WriteLine("║                        LOJA CELULARXPRESS                              ║");
        Console.WriteLine("║                         Realizar Venda                                 ║");
        Console.WriteLine("╠════════════════════════════════════════════════════════════════════════╣");
        Console.ResetColor();

        Console.WriteLine("\n   Digite o Id do produto para a venda: ");
        if (!int.TryParse(Console.ReadLine(), out int idVenda))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n   ❌ Id inválido. Por favor, digite um número.");
            Console.ResetColor();
            return;
        }

        int index = produtos.FindIndex(p => p.Id == idVenda);
        if (index == -1)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n   ❌ Produto não encontrado.");
            Console.ResetColor();
            return;
        }

        Produto produtoParaVenda = produtos[index];
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"\n   Produto selecionado: {produtoParaVenda.Nome} | Estoque disponível: {produtoParaVenda.Quantidade}");
        Console.ResetColor();

        Console.WriteLine("\n   Digite a quantidade a ser vendida: ");
        if (!int.TryParse(Console.ReadLine(), out int quantidadeVendida) || quantidadeVendida <= 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n   ❌ Quantidade inválida. Por favor, digite um número inteiro positivo.");
            Console.ResetColor();
            return;
        }

        if (quantidadeVendida > produtoParaVenda.Quantidade)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n   ⚠️ Quantidade insuficiente em estoque.");
            Console.ResetColor();
            return;
        }

        produtoParaVenda.Quantidade -= quantidadeVendida;
        produtos[index] = produtoParaVenda;

        double valorTotalVenda = (double)produtoParaVenda.Preco * quantidadeVendida;
        Venda novaVenda = new Venda
        {
            IdVenda = vendas.Count > 0 ? vendas.Max(v => v.IdVenda) + 1 : 1,
            NomeProduto = produtoParaVenda.Nome,
            Categoria = produtoParaVenda.Categoria,
            QuantidadeVendida = quantidadeVendida,
            ValorTotal = valorTotalVenda,
            DataVenda = DateTime.Now
        };
        vendas.Add(novaVenda);

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\n   ✔ Venda realizada com sucesso!");
        Console.WriteLine($"   Valor total da venda: {valorTotalVenda:C}");
        Console.ResetColor();

        SalvarProdutos();
        SalvarVendas();
    }

    public static void GerarRelatoriosDeVendas()
    {
        Console.WriteLine("\n--- Relatório de Vendas ---");
        if (vendas.Count == 0)
        {
            Console.WriteLine("Nenhuma venda registrada.");
            return;
        }
        foreach (var v in vendas)
        {
            Console.WriteLine($"\nId Venda: {v.IdVenda}");
            Console.WriteLine($"Produto: {v.NomeProduto}");
            Console.WriteLine($"Categoria: {v.Categoria}");
            Console.WriteLine($"Quantidade Vendida: {v.QuantidadeVendida}");
            Console.WriteLine($"Valor Total: {v.ValorTotal}");
            Console.WriteLine($"Data da Venda: {v.DataVenda}");
            Console.WriteLine("---------------------------------");
        }
    }

        //CAMADA DE INTERFACE
        static void Main(string[] args)
    {
        produtos = CarregarProdutos();
        vendas = CarregarVendas();

        while (true)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("╔════════════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                                                                        ║");
            Console.WriteLine("║                        LOJA CELULARXPRESS                              ║");
            Console.WriteLine("║                          Menu Principal                                ║");
            Console.WriteLine("║                                                                        ║");
            Console.WriteLine("╠════════════════════════════════════════════════════════════════════════╣");
            Console.ResetColor();

            Console.WriteLine("║                                                                        ║");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("║   [1] Cadastrar Produto                                                ║");
            Console.WriteLine("║   [2] Realizar Venda                                                   ║");
            Console.WriteLine("║   [3] Consultar Produto                                                ║");
            Console.WriteLine("║   [4] Excluir Produto                                                  ║");
            Console.WriteLine("║   [5] Relatório de Vendas                                              ║");
            Console.WriteLine("║   [0] Sair                                                             ║");
            Console.ResetColor();
            Console.WriteLine("║                                                                        ║");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("╚════════════════════════════════════════════════════════════════════════╝");
            Console.ResetColor();
            Console.Write("\nEscolha uma opção: ");
            string opcao = Console.ReadLine();


            switch (opcao)
            {
                case "1": CadastrarProduto(); break;
                case "2": RealizarVenda(); break;
                case "3": ConsultarProdutos(); break;
                case "4": ExcluirProdutos(); break;
                case "5": GerarRelatoriosDeVendas(); break;
                case "0":
                    Console.WriteLine("\nSaindo da aplicação...");
                    return;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Opção inválida. Tente novamente.");
                    Console.ResetColor();
                    break;
            }
            Console.WriteLine("Pressione uma tecla para continuar...");
            Console.ReadKey();



        }



    }
}
    

    