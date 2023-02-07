using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yakov.Notes.Domain.Interfaces
{
    public interface IAuthService
    {
        public Task<bool> SignInAsync();
        public Task<bool> SignUpAsync();
    }
}
