using Teste.Domain;

namespace Teste.Service
{
    public interface ICreditPessoaJuridicaService
    {
        public GetCreditReleaseResponse GenerateCreditRelease(GetCreditReleaseRequest model);
    }
}
