using Teste.Domain;

namespace Teste.Service
{
    public interface ICreditImobiliarioService
    {
        public GetCreditReleaseResponse GenerateCreditRelease(GetCreditReleaseRequest model);
    }
}
