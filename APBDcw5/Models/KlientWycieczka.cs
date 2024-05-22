using System;

namespace APBD_CW5.Models
{
    public partial class KlientWycieczka
    {
        // Właściwości encji KlientWycieczka
        public int IdKlient { get; set; }
        public int IdWycieczka { get; set; }
        public DateTime DataRejestracji { get; set; }
        public DateTime? DataPlatnosci { get; set; }

        // Nawigacja do encji Klient i Wycieczka
        public virtual Klient KlientNawigacja { get; set; } = null!;
        public virtual Wycieczka WycieczkaNawigacja { get; set; } = null!;
    }
}