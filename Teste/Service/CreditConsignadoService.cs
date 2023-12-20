using Teste.Domain;

namespace Teste.Service
{
    public class CreditConsignadoService : ICreditConsignadoService
    {
        public GetCreditReleaseResponse GenerateCreditRelease(GetCreditReleaseRequest model)
        {

            double taxaJuros = 0.01;

            var valorTotal = model.Valor * (decimal)Math.Pow((1 + taxaJuros), (double)model.QtyParcelas);
            var juros = valorTotal - model.Valor;


            return new GetCreditReleaseResponse
            {
                Status = "Aprovado",
                ValorTotal = decimal.Round(valorTotal, 2, MidpointRounding.AwayFromZero),
                ValorJuros = decimal.Round(juros, 2, MidpointRounding.AwayFromZero)
        };
        }
    }
}
