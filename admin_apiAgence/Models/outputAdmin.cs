namespace admin_apiAgence.Models
{
    public class outputAdmin
    {
        public String Http_Status_Code { get; set; }
        public string message { get; set; }
        public List<admin> list_admins { get; set; }
    }
}
