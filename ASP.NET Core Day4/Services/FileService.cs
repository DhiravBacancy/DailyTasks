using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Linq;
using ASP.NET_Core_Day4.Models;

namespace ASP.NET_Core_Day4.Services
{
    public class FileService
    {
        private readonly string _filePath = "userData.json";

        public FileService()
        {
            if (!File.Exists(_filePath))
            {
                File.WriteAllText(_filePath, "[]"); // Initialize empty JSON file
            }
        }

        public List<UserModel> GetAllUsers()
        {
            var json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<UserModel>>(json) ?? new List<UserModel>();
        }

        public void SaveUser(UserModel user)
        {
            var users = GetAllUsers();
            users.Add(user);
            File.WriteAllText(_filePath, JsonSerializer.Serialize(users));
        }

        public bool ValidateGuid(string guid)
        {
            return GetAllUsers().Any(u => u.Guid == guid);
        }
    }
}
