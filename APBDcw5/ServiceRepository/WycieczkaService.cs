using APBD_CW5.Context;
using APBD_CW5.DTOs;
using APBD_CW5.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace APBD_CW5.ServiceRepository
{
    public class WycieczkaService : IWycieczkaService
    {
        private readonly BazaDanychContext _dbContext;

        public WycieczkaService(BazaDanychContext context)
        {
            _dbContext = context;
        }

        // Metoda do dodawania klienta do wycieczki
        public IActionResult DodajKlientaDoWycieczki(int idWycieczki, KlientWycieczkaDTO klientWycieczkaDto)
        {
            var istniejącyKlient = _dbContext.Klienci.FirstOrDefault(c => c.Pesel == klientWycieczkaDto.Pesel);

            if (istniejącyKlient == null)
            {
                var nowyKlientId = _dbContext.Klienci.Any() ? _dbContext.Klienci.Max(c => c.IdKlient) + 1 : 1;
                istniejącyKlient = new Klient
                {
                    IdKlient = nowyKlientId,
                    Imie = klientWycieczkaDto.Imie,
                    Nazwisko = klientWycieczkaDto.Nazwisko,
                    Email = klientWycieczkaDto.Email,
                    Telefon = klientWycieczkaDto.Telefon,
                    Pesel = klientWycieczkaDto.Pesel
                };
                _dbContext.Klienci.Add(istniejącyKlient);
                _dbContext.SaveChanges();
            }

            var istniejącaWycieczkaKlienta = _dbContext.WycieczkiKlientów
                .Include(ct => ct.KlientNawigacja)
                .FirstOrDefault(ct => ct.IdWycieczka == idWycieczki && ct.KlientNawigacja.Pesel == klientWycieczkaDto.Pesel);

            if (istniejącaWycieczkaKlienta != null)
            {
                return new BadRequestObjectResult("Klient już jest zarejestrowany na wybraną wycieczkę");
            }

            var wycieczkaIstnieje = _dbContext.Wycieczki.Any(t => t.IdWycieczka == idWycieczki);

            if (!wycieczkaIstnieje)
            {
                return new BadRequestObjectResult("Wycieczka nie istnieje");
            }

            var nowaWycieczkaKlienta = new KlientWycieczka
            {
                IdKlient = istniejącyKlient.IdKlient,
                IdWycieczka = idWycieczki,
                DataRejestracji = DateTime.Now,
                DataPlatnosci = klientWycieczkaDto.DataPlatnosci
            };

            _dbContext.WycieczkiKlientów.Add(nowaWycieczkaKlienta);
            _dbContext.SaveChanges();

            return new OkObjectResult("Dodano klienta do wycieczki");
        }

        // Metoda do pobierania wszystkich wycieczek
        public List<WycieczkaDTO> PobierzWycieczki()
        {
            return _dbContext.Wycieczki
                .Include(t => t.Kraje)
                .Include(t => t.WycieczkiKlientów)
                .OrderByDescending(t => t.DataOd)
                .Select(t => new WycieczkaDTO
                {
                    Nazwa = t.Nazwa,
                    Opis = t.Opis,
                    DataOd = t.DataOd,
                    DataDo = t.DataDo,
                    MaksymalnaIloscKlientow = t.MaksymalnaIloscKlientow,
                    Kraje = t.Kraje.Select(c => new KrajDTO
                    {
                        Nazwa = c.Nazwa
                    }).ToList(),
                    Klienci = t.WycieczkiKlientów.Select(ct => new KlientDTO
                    {
                        Imie = ct.KlientNawigacja.Imie,
                        Nazwisko = ct.KlientNawigacja.Nazwisko
                    }).ToList()
                })
                .ToList();
        }
    }
}
