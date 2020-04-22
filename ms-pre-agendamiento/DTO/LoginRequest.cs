using System.ComponentModel.DataAnnotations;

namespace ms_pre_agendamiento.Dto
{
    public class LoginRequest
    {
        [Required]
        public string Name { set; get; }
        
        [Required] public string Password { set; get; }
        
    }
}