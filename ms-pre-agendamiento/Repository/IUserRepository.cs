using Ms_pre_agendamiento.Models;

namespace Ms_pre_agendamiento.Repository
{
    public interface IUserRepository
    {
        User GetById(int id);
        User GetByUserNameAndPassword(string username, string password);

        bool IsValid(DTO.User user);
    }
}