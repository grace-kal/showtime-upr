using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.UI.Xaml.Automation;
using ShowTimeUpr.Models;

namespace ShowTimeUpr.DataAccess
{
    public class UserRepo(AppDbContext context) : IUserRepo
    {
        public async Task<User> Login(string username, string password)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Username == username);

            if (user != null && user.PasswordHash == HashPass(password))
            {
                return user;
            }

            return null;
        }

        public async Task Register(string username, string password)
        {
            if (await context.Users.AnyAsync(u => u.Username.Equals(username)))
            {
                return;
            }

            var user = new User
            {
                Username = username,
                PasswordHash = HashPass(password)
            };

            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
        }


        private string HashPass(string pass)
        {
            using (var sha256 = SHA256.Create()) // Create an instance of SHA256
            {
                byte[] bytes = Encoding.UTF8.GetBytes(pass);

                byte[] hashBytes = sha256.ComputeHash(bytes);

                StringBuilder hashStringBuilder = new StringBuilder();
                foreach (var byteValue in hashBytes)
                {
                    hashStringBuilder.Append(byteValue.ToString("x2"));
                }

                return hashStringBuilder.ToString();
            }
        }

    }
}
