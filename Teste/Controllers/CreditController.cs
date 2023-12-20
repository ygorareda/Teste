using Microsoft.AspNetCore.Mvc;
using System.Net.NetworkInformation;
using Teste.Domain;
using Teste.Service;

namespace Teste.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CreditController :ControllerBase
    {
        private readonly ICreditService _creditService;


        public CreditController(ICreditService creditService)
        {
            _creditService = creditService;
        }

        [HttpPost]
        public IActionResult GetCredit([FromBody] GetCreditReleaseRequest request)
        {
            
            var response = _creditService.GetCreditReleaseRequest(request);

            return Ok(response);
        }

    }
}
