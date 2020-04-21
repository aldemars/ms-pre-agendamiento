namespace ms_pre_agendamiento.DTO
{
    public class LoginResponse
    {
            public string Token { set; get; }

            public LoginResponse(string token)
            {
                Token = token;
            }
    }
}