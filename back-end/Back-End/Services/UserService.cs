﻿using Back_End.Contexts;
using Back_End.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Back_End.Services
{
    public class UserService : IUserService
    {
        private UserContext _users;

        public UserService(UserContext users)
        {
            _users = users;
        }

        public async Task<User> Authenticate(string username, string password)
        {
            User user = await _users.Users.SingleOrDefaultAsync(u => u.Username == username && u.Password == password);
            return user;

        }

        public async Task<User> Register(string username, string password)
        {
            if (_users.Users.Single(u => u.Username == username) == null)
            {
                User user = new User { UserId = Guid.NewGuid().ToString(), Username = username, Password = password };
                //UserData userdata = new UserData { UserDataId = Guid.NewGuid().ToString(), UserId = user.UserId };
                await _users.Users.AddAsync(user);
                //await _users.UserDatas.AddAsync(userdata);
                await _users.SaveChangesAsync();
                return user;
            }
            return null;
        }
    }
}
