using Teste.Domain;

namespace Teste.Service
{
    public interface ICreditConsignadoService
    {

        public GetCreditReleaseResponse GenerateCreditRelease(GetCreditReleaseRequest model);
    }
}
