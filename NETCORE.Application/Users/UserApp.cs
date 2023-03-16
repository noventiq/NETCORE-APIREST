using Microsoft.Extensions.Logging;
using NETCORE.Application.Common;
using NETCORE.Application.Products;
using NETCORE.Domain.Products.Interfaces;
using NETCORE.Domain.Users.Domain;
using NETCORE.Domain.Users.Interfaces;
using NETCORE.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NETCORE.Application.Users
{
    public class UserApp : BaseApp<UserApp>
    {

        private readonly IUserRepository _userRepository;

        public UserApp(IUserRepository productRepository, ILogger<BaseApp<UserApp>> logger) : base(logger)
        {
            _userRepository = productRepository;
        }

        public async Task<StatusResponse<User>> Login(string username, string password)
        {
            return await this.complexProcess(() => this._userRepository.Login(username, password));
        }
    }
}
