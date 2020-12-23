using HappyMailApi.Models;
using HappyMailApi.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HappyMailApi.Services
{
    public interface IUserService
    {
        User Create(User user);
        bool IsValidUserCredentials(string userName, string password);
        string GetUserRole(string userName);
    }

    public class UserService : IUserService
    {
        private readonly UsersRepository _userRepository;

        public UserService(UsersRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User Create(User user) => 
            _userRepository.Create(user);

        public bool IsValidUserCredentials(string userName, string password)
        {
            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password))
            {
                return false;
            }

            return _userRepository.Get(userName).Password == password;
        }

       
        public string GetUserRole(string userName)
        {
            if (!IsAnExistingUser(userName))
            {
                return string.Empty;
            }

            if (userName == "admin")
            {
                return UserRoles.Admin;
            }

            return UserRoles.BasicUser;
        }

        private bool IsAnExistingUser(string userName)
        {
            return _userRepository.Contains(userName);
        }
    }

    public static class UserRoles
    {
        public const string Admin = nameof(Admin);
        public const string BasicUser = nameof(BasicUser);
    }
}
