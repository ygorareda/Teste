using System.Runtime.CompilerServices;
using Teste.Domain;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Teste.Service
{
    public class CreditService : ICreditService
    {
        private readonly ICreditConsignadoService _creditConsignadoService;
        private readonly ICreditDiretoService _creditDiretoService;
        private readonly ICreditImobiliarioService _creditImobiliarioService;
        private readonly ICreditPessoaFisicaService _creditPessoaFisicaService;
        private readonly ICreditPessoaJuridicaService _creditPessoaJuridicaService;
        public CreditService(ICreditConsignadoService creditConsignadoService,
                 ICreditDiretoService creditDiretoService,
                    ICreditImobiliarioService creditImobiliarioService,
                    ICreditPessoaFisicaService creditPessoaFisicaService,
                    ICreditPessoaJuridicaService creditPessoaJuridicaService
        )
        {
            _creditConsignadoService = creditConsignadoService;
            _creditDiretoService = creditDiretoService;
            _creditImobiliarioService = creditImobiliarioService;
            _creditPessoaFisicaService = creditPessoaFisicaService;
            _creditPessoaJuridicaService = creditPessoaJuridicaService;
    }

        public GetCreditReleaseResponse GetCreditReleaseRequest(GetCreditReleaseRequest request)
        {
            request.Valor = CheckCreditValue(request.Valor);


            var validInstallment = ValidateInstallmentQty(request.QtyParcelas);
            if (!validInstallment)
                return new GetCreditReleaseResponse
                {
                    Status = "Recusado",
                    ValorTotal = 0,
                    ValorJuros = 0
                };

            var validVencimentoInicial = ValidateVencimento(request.VencimentoInicial);
            if (!validVencimentoInicial)
                return new GetCreditReleaseResponse
                {
                    Status = "Recusado",
                    ValorTotal = 0,
                    ValorJuros = 0
                };


            var response = GenerateCreditRelease(request);

            return response;
        }


        private bool ValidateInstallmentQty(int installmentQyt)
        {
            if (installmentQyt < 5 || installmentQyt > 72)
            {
                return false;
            }
            return true;
        }

        private decimal CheckCreditValue(decimal creditValue)
        {
            if (creditValue > 1000000)
                return 1000000;

            return creditValue;
        }

        private bool ValidateVencimento(DateTime vencimento)
        {
            var now = DateTime.Now;
            if (now.AddDays(15) > vencimento || now.AddDays(40) < vencimento)
                return false;

            return true;
        }

        private GetCreditReleaseResponse GenerateCreditRelease(GetCreditReleaseRequest model)
        {
            return RemoverAcentos(model.Tipo).ToLower() switch
            {
                "direto" => _creditDiretoService.GenerateCreditRelease(model),
                "consignado" => _creditConsignadoService.GenerateCreditRelease(model),
                "pessoa juridica" => _creditPessoaJuridicaService.GenerateCreditRelease(model),
                "pessoa fisica" => _creditPessoaFisicaService.GenerateCreditRelease(model),
                "imobiliario" => _creditImobiliarioService.GenerateCreditRelease(model),
                _ => new GetCreditReleaseResponse
                {
                    Status = "Recusado",
                    ValorTotal = 0,
                    ValorJuros = 0
                }
            };

        }

        public string RemoverAcentos(string texto)
        {
            string Acentos = "ÄÅÁÂÀÃäáâàãÉÊËÈéêëèÍÎÏÌíîïìÖÓÔÒÕöóôòõÜÚÛüúûùÇç";
            string semAcentos = "AAAAAAaaaaaEEEEeeeeIIIIiiiiOOOOOoooooUUUuuuuCc";

            for (int i = 0; i < Acentos.Length; i++)
            {
                texto = texto.Replace(Acentos[i].ToString(), semAcentos[i].ToString());
            }
            return texto;
        }


    }
}
