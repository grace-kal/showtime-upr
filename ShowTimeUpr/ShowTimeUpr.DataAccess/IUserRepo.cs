using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowTimeUpr.DataAccess
{
    public interface IUserRepo
    {
        public Task<Models.User> Login(string username, string password);
        public Task Register(string username, string password);
    }
}
