using System.ComponentModel.DataAnnotations;

namespace ms_pre_agendamiento.DTO
{
    public class User
    {
        [Required]
        public string Name { set; get; }
        
        [Required] public string Password { set; get; }
        
    }
}