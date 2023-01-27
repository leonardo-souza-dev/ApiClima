namespace ApiClima.Models
{
    public class FraseRetorno
    {
        private const string FRASE_RETORNO = "Em {0} faz {1}º graus {2}.";
        private readonly string _cidade;
        private readonly decimal _valorGraus;
        private readonly string _sistema;

        public FraseRetorno(string cidade, decimal valorGraus, string sistema)
        {
            if (string.IsNullOrEmpty(cidade) || string.IsNullOrEmpty(sistema))
            {
                throw new ArgumentNullException();
            }
            _cidade = cidade;
            _valorGraus= valorGraus;
            _sistema = sistema;
        }

        public string ObterFraseFinal()
        {
            var fraseFinal = string.Format(FRASE_RETORNO, _cidade, _valorGraus, _sistema);
            
            return fraseFinal;
        }
    }
}