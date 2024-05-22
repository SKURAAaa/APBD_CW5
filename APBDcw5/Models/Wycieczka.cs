using System;
using System.Collections.Generic;

namespace APBD_CW5.Models
{
    public partial class Wycieczka
    {
        public Wycieczka()
        {
            WycieczkiKlientów = new HashSet<KlientWycieczka>();
            Kraje = new HashSet<Kraj>();
        }

        // Właściwości encji Wycieczka
        public int IdWycieczka { get; set; }
        public string Nazwa { get; set; } = null!;
        public string Opis { get; set; } = null!;
        public DateTime DataOd { get; set; }
        public DateTime DataDo { get; set; }
        public int MaksymalnaIloscKlientow { get; set; }

        // Nawigacja do encji KlientWycieczka i Kraj
        public virtual ICollection<KlientWycieczka> WycieczkiKlientów { get; set; }
        public virtual ICollection<Kraj> Kraje { get; set; }
    }
}