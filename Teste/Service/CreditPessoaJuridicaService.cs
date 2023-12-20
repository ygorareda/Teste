using System;
using Teste.Domain;

namespace Teste.Service
{
    public class CreditPessoaJuridicaService : ICreditPessoaJuridicaService
    {
        public GetCreditReleaseResponse GenerateCreditRelease(GetCreditReleaseRequest model)
        {
            var ValidValueIfIsPessoaFisica = ValidateMinValueForPessoaJuridica(model.Valor);
            if (!ValidValueIfIsPessoaFisica)
                return new GetCreditReleaseResponse
                {
                    Status = "Recusado",
                    ValorTotal = 0,
                    ValorJuros = 0
                };

            double taxaJuros = 0.05;

            var valorTotal = model.Valor * (decimal)Math.Pow((1 + taxaJuros), (double)model.QtyParcelas);
            var juros = valorTotal - model.Valor;
            

            return new GetCreditReleaseResponse
            {
                Status = "Aprovado",
                ValorTotal = decimal.Round(valorTotal, 2, MidpointRounding.AwayFromZero),
                ValorJuros = decimal.Round(juros, 2, MidpointRounding.AwayFromZero)
            };
        }

        private bool ValidateMinValueForPessoaJuridica(decimal creditValue)
        {
            if (creditValue < 15000)
                return false;

            return true;
        }
    }
}
