using APBD_CW5.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace APBD_CW5.ServiceRepository
{
    public interface IWycieczkaService
    {
        IActionResult DodajKlientaDoWycieczki(int idWycieczki, KlientWycieczkaDTO dodajKlientaRequestDto);
        List<WycieczkaDTO> PobierzWycieczki();
    }
}