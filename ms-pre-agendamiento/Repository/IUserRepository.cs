using System;
using ms_pre_agendamiento.Models;

namespace ms_pre_agendamiento.Repository
{
    public interface IUserRepository
    {
        User GetById(int id);
        User GetByUserNameAndPassword(string username, string password);
        User getUser(DTO.User user);

    }
}