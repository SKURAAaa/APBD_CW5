using APBD_CW5.Context;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace APBD_CW5.ServiceRepository
{
    public class KlientService : IKlientService
    {
        private readonly BazaDanychContext _context;

        public KlientService(BazaDanychContext context)
        {
            _context = context;
        }

        // Metoda do usuwania klienta
        public IActionResult UsunKlienta(int klientId)
        {
            var istniejeWWycieczkach = _context.WycieczkiKlientów.Any(ct => ct.IdKlient == klientId);

            if (istniejeWWycieczkach)
            {
                return new BadRequestObjectResult("Klient ma istniejące wycieczki");
            }

            var klient = _context.Klienci.Find(klientId);

            if (klient == null)
            {
                return new NotFoundObjectResult("Klient nie znaleziony");
            }

            _context.Klienci.Remove(klient);
            _context.SaveChanges();

            return new OkObjectResult("Klient został usunięty");
        }
    }
}