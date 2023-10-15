namespace DesafioFundamentos.Models
{
    public class Estacionamento
    {
        private decimal precoInicial = 0;
        private decimal precoPorHora = 0;
        private List<Veiculo> veiculos = new();

        public Estacionamento(decimal precoInicial, decimal precoPorHora)
        {
            this.precoInicial = precoInicial;
            this.precoPorHora = precoPorHora;
        }

        public void AdicionarVeiculo()
        {
            Veiculo veiculo = new();
            veiculo = LerPlaca(veiculo);

            var veiculoSelecionado = veiculos.Where(x => x.Placa.Equals(veiculo.Placa)).FirstOrDefault();

            if (veiculoSelecionado != null && veiculoSelecionado.Ativo)
            {
                Menu.ExibeMenu();
                Console.WriteLine("Já existe um veículo com essa placa! Pressione Enter.");
                Console.ReadLine();
            }
            else if (veiculoSelecionado != null)
            {
                 var listaRestante = veiculos.Where(x => !x.Placa.Equals(veiculo.Placa)).ToList();
                 listaRestante.AddRange(veiculos.Where(x => x.Placa.Equals(veiculo.Placa)).Select(x => { x.Ativo = false; return x; }).ToList());
                 veiculos.CopyTo(0, listaRestante.ToArray(), 0, listaRestante.Count());            
            }
            else
            {
                veiculo.Ativo = true;
                veiculos.Add(veiculo);
            }
        }

        public void RemoverVeiculo()
        {
            Veiculo veiculo = new();
            decimal valorTotal = 0;
            decimal horasPatio = 0;
            string inputHoras = string.Empty;

            veiculo = LerPlaca(veiculo);

            // Verifica se o veículo existe
            if (veiculos.Where(x => x.Placa.ToUpper() == veiculo.Placa.ToUpper() && x.Ativo).Any())
            {
                veiculo = veiculos.Where(x => x.Placa.ToUpper() == veiculo.Placa.ToUpper()).FirstOrDefault();

                Console.WriteLine("Digite a quantidade de horas que o veículo permaneceu estacionado (ex: 1, 1.30, 2.30):");
                inputHoras = Console.ReadLine() ?? "0";

                while (!decimal.TryParse(inputHoras.Replace(".", ","), out horasPatio) && horasPatio >= 0)
                {
                    Console.WriteLine("Hora inválida. Tente novamente.");
                    inputHoras = Console.ReadLine() ?? string.Empty;
                }

                veiculo.Horas = horasPatio;

                int horas = (int)Math.Truncate(horasPatio);
                decimal minutos = (horasPatio - horas) * 100;

                if (minutos < 60)
                {
                    //Atualiza o registro na lista - Ativo = False
                    var listaRestante = veiculos.Where(x => !x.Placa.Equals(veiculo.Placa)).ToList();
                    listaRestante.AddRange(veiculos.Where(x => x.Placa.Equals(veiculo.Placa)).Select(x => { x.Ativo = false; return x; }).ToList());
                    veiculos.CopyTo(0, listaRestante.ToArray(), 0, listaRestante.Count());

                    valorTotal = (veiculo.Horas * precoPorHora) + precoInicial;

                    Console.WriteLine($"O veículo {veiculo.Placa} foi removido e o preço total foi de: R$ {valorTotal}");
                }
                else
                {
                    Console.WriteLine("Hora inválida. Tente novamente.");
                    inputHoras = Console.ReadLine() ?? string.Empty;
                }

            }
            else
            {
                Console.WriteLine("Desculpe, esse veículo não está estacionado aqui. Confira se digitou a placa corretamente");
            }
        }

        public void ListarVeiculos()
        {
            // Verifica se há veículos no estacionamento
            if (veiculos.Where(x => x.Ativo).Any())
            {
                Console.WriteLine("Os veículos estacionados são:");
                foreach (var veiculoCadastrado in veiculos.Where(x => x.Ativo))
                    Console.WriteLine(veiculoCadastrado.Placa);
            }
            else
            {
                Console.WriteLine("Não há veículos estacionados.");
            }
        }

        private Veiculo LerPlaca(Veiculo veiculo)
        {
            Console.WriteLine("Digite a placa do veículo: ");
            veiculo.Placa = Console.ReadLine() ?? string.Empty;

            while (!veiculo.ValidaPlaca())
            {
                Menu.ExibeMenu();
                Console.WriteLine("Digite uma placa válida: ");
                veiculo.Placa = Console.ReadLine() ?? string.Empty;
            }

            return veiculo;
        }
    }
}
