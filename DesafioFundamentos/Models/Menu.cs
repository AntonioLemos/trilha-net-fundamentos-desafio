namespace DesafioFundamentos.Models
{
    public static class Menu
    {
        public static void ExibeMenu()
        {
            Console.Clear();
            Console.WriteLine("Digite a sua opção:");
            Console.WriteLine("1 - Cadastrar veículo");
            Console.WriteLine("2 - Remover veículo");
            Console.WriteLine("3 - Listar veículos");
            Console.WriteLine("4 - Encerrar");
        }

        public static void ImprimeErroMenu()
        {
            Console.WriteLine("Digite uma opção válida!!");
            Console.WriteLine("Pressione Enter para continuar!!");
            Console.ReadLine();
        }
    }
}