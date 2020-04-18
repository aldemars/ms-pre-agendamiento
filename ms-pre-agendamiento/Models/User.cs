namespace Ms_pre_agendamiento.Models
{
    using System.Text.Json.Serialization;

    public class User
    {
        public string Id { get; set; }

        public string Name { get; set; }

        [JsonIgnore]
        public string Password { get; set; }
    }
}