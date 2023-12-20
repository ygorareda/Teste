using Teste.Domain;

namespace Teste.Service
{
    public interface ICreditDiretoService
    {
        public GetCreditReleaseResponse GenerateCreditRelease(GetCreditReleaseRequest model);
    }
}
