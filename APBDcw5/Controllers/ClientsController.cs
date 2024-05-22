using APBD_CW5.ServiceRepository;
using Microsoft.AspNetCore.Mvc;

namespace APBD_CW5.Controller
{
    [Route("api/klienci")]
    [ApiController]
    public class KlienciController : ControllerBase
    {
        private readonly IKlientService _klientService;

        public KlienciController(IKlientService klientService)
        {
            _klientService = klientService;
        }

        // Endpoint do usuwania klienta
        [HttpDelete("{idKlienta}")]
        public IActionResult UsunKlienta(int idKlienta)
        {
            var rezultat = _klientService.UsunKlienta(idKlienta);
            return rezultat ?? NotFound(new { Message = "Klient nie znaleziony" });
        }
    }
}