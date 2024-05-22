using Microsoft.AspNetCore.Mvc;

namespace APBD_CW5.ServiceRepository
{
    public interface IKlientService
    {
        IActionResult UsunKlienta(int idKlienta);
    }
}