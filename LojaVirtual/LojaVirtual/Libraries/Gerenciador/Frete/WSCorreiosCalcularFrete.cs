using LojaVirtual.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;
using WSCorreios;

namespace LojaVirtual.Libraries.Gerenciador.Frete
{
    public class WSCorreiosCalcularFrete
    {
        private IConfiguration _configuration;
        private CalcPrecoPrazoWSSoap _servico;

        public WSCorreiosCalcularFrete(IConfiguration configuration, CalcPrecoPrazoWSSoap servico)
        {
            _configuration = configuration;
            _servico = servico;
        }

        public async Task CalcularValorPrazoFrete(String cepDestino, String tipoFrete, Pacote pacote)
        {
            var CepOrigem = _configuration.GetValue<String>("Frete:CepOrigem");
            var MaoPropria = _configuration.GetValue<String>("Frete:MaoPropria");
            var AvisoRecebimento = _configuration.GetValue<String>("Frete:AvisoRecebimento");
            var Diametro = Math.Max(Math.Max(pacote.Comprimento, pacote.Largura), pacote.Altura);


            cResultado Resultado = await _servico.CalcPrecoPrazoAsync("", "", tipoFrete, CepOrigem, cepDestino, pacote.Peso.ToString(), 1, pacote.Comprimento, pacote.Altura, pacote.Largura, Diametro, MaoPropria, 0, AvisoRecebimento);

            if (Resultado.Servicos[0].Erro == "0")
            {
                //TODO - Implementar um resultado

            }
            else
            {
                throw new Exception("Erro: " + Resultado.Servicos[0].MsgErro);
            }
        }
    }
}
