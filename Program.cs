using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Program {
    static List<Produto> produtos = new List<Produto>();
    static List<Venda> vendas = new List<Venda>();
    static void Main(string[] args) 
    {
        produtos = CarregarProdutos();

        while(true)
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
    

    