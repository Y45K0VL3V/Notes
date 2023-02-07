using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yakov.Notes.Domain.Interfaces;

namespace yakov.Notes.Services
{
    public class FirebaseAuthService : IAuthService
    {
        public Task<bool> SignInAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> SignUpAsync()
        {
            throw new NotImplementedException();
        }
    }
}
