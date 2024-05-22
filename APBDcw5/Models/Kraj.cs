using System.Collections.Generic;

namespace APBD_CW5.Models
{
    public partial class Kraj
    {
        public Kraj()
        {
            Wycieczki = new HashSet<Wycieczka>();
        }

        // Właściwości encji Kraj
        public int IdKraj { get; set; }
        public string Nazwa { get; set; } = null!;

        // Nawigacja do encji Wycieczka
        public virtual ICollection<Wycieczka> Wycieczki { get; set; }
    }
}