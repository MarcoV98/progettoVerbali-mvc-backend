namespace progettoVerbali_mvc_backend.Models
{
    public class Verbali
    {
        public int IDVerbale { get; set; }
        public DateTime DataViolazione { get; set; }
        public string IndirizzoViolazione { get; set; }
        public string Nominativo_Agente { get; set; }
        public DateTime DataTrascrizioneVerbale { get; set; }
        public decimal Importo { get; set; }
        public byte DecurtamentoPunti { get; set; }
        public int AnagraficaID { get; set; }
        public int TipoViolazioneID { get; set; }
    }
}
