using NETCORE.Domain.Products.Domain;
using NETCORE.Domain.Users.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NETCORE.Domain.Users.Interfaces
{
    public interface IUserRepository
    {
        Task<User> Login(string username, string password);
    }
}
