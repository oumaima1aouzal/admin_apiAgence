namespace admin_apiAgence.Models
{
    public class Agence
    {
        public int Id { get; set; }
        public String nom { get; set; }
        public float gps1 { get; set; }
        public float gps2 { get; set; }
        public String region { get; set; }
        public int codeagence { get; set; }
        public String adresseagence { get; set; }
        public String typesite { get; set; }
        public String gsmsite { get; set; }
        public String telsite { get; set; }
        public String horaireouvmatin { get; set; }
        public String horairefermmatin { get; set; }
        public String horaireouvsoir { get; set; }
        public String horairefermsoir { get; set; }
    }
}
