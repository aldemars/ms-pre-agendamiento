using System.Text.Json.Serialization;

namespace ms_pre_agendamiento.Models
{
    public class User
    {
        public string Id { set; get; }
        public string Name { set; get; }
        [JsonIgnore]
        public string Password { set; get; }
        public string Token { set; get; }
    }
}