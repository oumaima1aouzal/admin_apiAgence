namespace admin_apiAgence.Models
{
    public class OutputAgence
    {
        public String Http_Status_Code { get; set; }
        public string message { get; set; }
        public List<Agence> list { get; set; }
    }
}
