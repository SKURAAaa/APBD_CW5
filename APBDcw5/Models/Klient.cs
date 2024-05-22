using System.Collections.Generic;

namespace APBD_CW5.Models
{
    public partial class Klient
    {
        public Klient()
        {
            WycieczkiKlientów = new HashSet<KlientWycieczka>();
        }

        // Właściwości encji Klient
        public int IdKlient { get; set; }
        public string Imie { get; set; } = null!;
        public string Nazwisko { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Telefon { get; set; } = null!;
        public string Pesel { get; set; } = null!;

        // Nawigacja do encji KlientWycieczka
        public virtual ICollection<KlientWycieczka> WycieczkiKlientów { get; set; }
    }
}