using NETCORE.Domain.Products.Domain;
using NETCORE.Domain.Users.Domain;
using NETCORE.Domain.Users.Interfaces;
using NETCORE.Shared;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NETCORE.Domain.Common.Interfaces;
using Dapper;

namespace NETCORE.Infraestructure.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly ICustomConnection _connection;
        public UserRepository(ICustomConnection connection)
        {
            _connection = connection;
        }

        //public async Task<User> Login(string username, string password)
        //{
        //    User item = null;
        //    Profile profile = null;

        //    using (var scope = await _connection.BeginConnection())
        //    {
        //        try
        //        {
        //            using (var multiGrid = await scope.QueryMultipleAsync("USP_USER_LOGIN", new { username, password }, commandType: CommandType.StoredProcedure))
        //            {
        //                item = await multiGrid.ReadFirstOrDefaultAsync<User>();
        //                profile = await multiGrid.ReadFirstOrDefaultAsync<Profile>();
        //            }

        //            item.Profile = profile;
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new CustomException("Error al listar productos", ex);
        //        }
        //    }

        //    return item;
        //}

        //public async Task<User> Login(string username, string password)
        //{
        //    User item = null;
        //    Profile profile = null;

        //    using (var scope = await _connection.BeginConnection())
        //    {
        //        try
        //        {
        //            var results = await scope.QueryAsync<User, Profile, User>("USP_USER_LOGIN_JOIN", (u, p) =>
        //            {
        //                u.Profile = p;
        //                return u;
        //            },
        //            new { username, password }, commandType: CommandType.StoredProcedure, splitOn: "id");

        //            if (results.Count() == 1)
        //                item = results.ElementAt(0);

        //            //item.Profile = profile;
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new CustomException("Error al listar productos", ex);
        //        }
        //    }

        //    return item;
        //}

        public async Task<User> Login(string username, string password)
        {
            User item = null;

            Dictionary<int, User> dicResults = new Dictionary<int, User>();
            using (var scope = await _connection.BeginConnection())
            {
                try
                {
                    var results = await scope.QueryAsync<User, Rol, User>("USP_USER_LOGIN_JOIN", (u, r) =>
                    {
                        User _user;
                        if (!dicResults.TryGetValue(u.Id, out _user))
                        {
                            _user = u;
                            _user.Roles = new List<Rol>();
                            dicResults.Add(u.Id, _user);
                        }
                        _user.Roles.Add(r);
                        return u;
                    },
                    new { username, password }, commandType: CommandType.StoredProcedure, splitOn: "id");

                }
                catch (Exception ex)
                {
                    throw new CustomException("Error al listar productos", ex);
                }
            }

            if (dicResults.Count == 1)
            {
                item = dicResults[dicResults.Keys.First<int>()];
            }
            return item;
        }

        //public async Task<Tuple<User, Rol>> Login(string username, string password)
        //{
        //    User item = null;

        //    Dictionary<int, User> dicUsers = new Dictionary<int, User>();
        //    Dictionary<int, User> dicRoles = new Dictionary<int, User>();
        //    using (var scope = await _connection.BeginConnection())
        //    {
        //        try
        //        {
        //            var results = await scope.QueryAsync<User, Rol, User>("USP_USER_LOGIN_JOIN", (u, r) =>
        //            {
        //                User _user;
        //                if (!dicUsers.TryGetValue(u.Id, out _user))
        //                {
        //                    _user = u;
        //                    _user.Roles = new List<Rol>();
        //                    dicUsers.Add(u.Id, _user);
        //                }
        //                _user.Roles.Add(r);
        //                return u;
        //            },
        //            new { username, password }, commandType: CommandType.StoredProcedure, splitOn: "id");

        //        }
        //        catch (Exception ex)
        //        {
        //            throw new CustomException("Error al listar productos", ex);
        //        }
        //    }

        //    if (dicResults.Count == 1)
        //    {
        //        item = dicResults[dicResults.Keys.First<int>()];
        //    }
        //    return item;
        //}
    }
}
