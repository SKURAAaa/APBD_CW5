using APBD_CW5.DTOs;
using APBD_CW5.ServiceRepository;
using Microsoft.AspNetCore.Mvc;

namespace APBD_CW5.Controller
{
    [Route("api/wycieczki")]
    [ApiController]
    public class WycieczkiController : ControllerBase
    {
        private readonly IWycieczkaService _wycieczkaService;

        public WycieczkiController(IWycieczkaService wycieczkaService)
        {
            _wycieczkaService = wycieczkaService;
        }

        // Endpoint do pobierania wszystkich wycieczek
        [HttpGet]
        public IActionResult PobierzWycieczki()
        {
            var wynik = _wycieczkaService.PobierzWycieczki();
            return Ok(wynik);
        }

        // Endpoint do dodawania klienta do wycieczki
        [HttpPost("{idWycieczki}/klienci")]
        public IActionResult DodajKlientaDoWycieczki(int idWycieczki, [FromBody] KlientWycieczkaDTO dodajKlientaRequestDto)
        {
            var rezultat = _wycieczkaService.DodajKlientaDoWycieczki(idWycieczki, dodajKlientaRequestDto);
            return rezultat ?? BadRequest(new { Message = "Nie można dodać klienta do wycieczki." });
        }
    }
}