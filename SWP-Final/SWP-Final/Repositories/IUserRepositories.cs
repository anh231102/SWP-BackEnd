﻿using SWP_Final.Models;

namespace SWP_Final.Repositories
{
    public interface IUserRepositories
    {
        public Task<List<UserModel>> GetAllUsersAsync();

        public Task<UserModel> GetUserByIdAsync(string id);

        public Task<UserModel> GetUserByNameAsync(string name);

        public Task DeleteUserAsync(string id);    

        public Task UpdateUserAsync(UserModel user, String userID);

        public Task<UserModel> LoginAsync(string username, string password);

        public Task RegisterAsync(string username, string password, CustomerModel customer);
        
    }
}
