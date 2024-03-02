namespace progettoVerbali_mvc_backend.Models
{
    public class ViolazioneVM
    {
        public int IDVerbale { get; set; }
        public DateTime DataViolazione { get; set; }
        public string IndirizzoViolazione { get; set; }
        public string NominativoAgente { get; set; }
        public DateTime DataTrascrizioneVerbale { get; set; }
        public decimal Importo { get; set; }
        public int DecurtamentoPunti { get; set; }
        public int AnagraficaID { get; set; }
        public int TipoViolazioneID { get; set; }
        public string DescrizioneTipoViolazione { get; set; }
    }
}
