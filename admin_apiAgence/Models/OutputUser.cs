namespace admin_apiAgence.Models
{
    public class OutputUser
    {
        public String Http_Status_Code { get; set; }
        public string message { get; set; }
        public List<user> list_utilisateurs { get; set; }
    }
}
