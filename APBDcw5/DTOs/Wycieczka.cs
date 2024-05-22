namespace APBD_CW5.DTOs
{
    public class WycieczkaDTO
    {
        public string Nazwa { get; set; }
        public string Opis { get; set; }
        public DateTime DataDo { get; set; }
        public DateTime DataOd { get; set; }
        public int MaksymalnaIloscKlientow { get; set; }

        public IEnumerable<KlientDTO> Klienci { get; set; }
        public IEnumerable<KrajDTO> Kraje { get; set; }
    }
}