using System.Text.RegularExpressions;

namespace DesafioFundamentos.Models
{
    public class Veiculo
    {
        public string Placa { get; set; } = string.Empty;
        public Decimal Horas { get; set; } = new ();
        public bool Ativo { get; set; } = false;

        public bool ValidaPlaca()
        {
            // Definir um padrão de expressão regular para placas brasileiras tradicionais e do Mercosul
            string pattern = @"^[A-Z]{3}-\d{4}$|^[A-Z]{3}\d{1}[A-Z]\d{2}$|^[A-Z]{3}\d{2}[A-Z]\d{1}$";
            // Use Regex.IsMatch para verificar se a entrada corresponde ao padrão
            return Regex.IsMatch(Placa, pattern);
        }
    }
}