using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yakov.Notes.Domain.Interfaces
{
    public interface IAuthService
    {
        public Task<string> SignInAsync(string email, string password);
        public Task<string> SignUpAsync(string email, string password);
    }
}
