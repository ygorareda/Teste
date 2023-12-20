using Teste.Domain;

namespace Teste.Service
{
    public interface ICreditPessoaFisicaService
    {
        public GetCreditReleaseResponse GenerateCreditRelease(GetCreditReleaseRequest model);
    }
}
