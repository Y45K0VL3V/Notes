using Firebase.Auth;
using Newtonsoft.Json;
using yakov.Notes.Domain.Interfaces;

namespace yakov.Notes.Services
{
    public class FirebaseAuthService : IAuthService
    {
        public FirebaseAuthService()
        {
            _authProvider = new(new FirebaseConfig(_firebaseApiKey));
        }

        private readonly string _firebaseApiKey = "AIzaSyAqgK2JmZW-N6Y2pHASUKAJGWlcOsSnqgo";
        private readonly FirebaseAuthProvider _authProvider;

        public async Task<string> SignInAsync(string email, string password)
        {
            var auth = await _authProvider.SignInWithEmailAndPasswordAsync(email, password);
            var content = await auth.GetFreshAuthAsync();
            var serializedContent = JsonConvert.SerializeObject(content);

            return serializedContent;
        }

        public async Task<string> SignUpAsync(string email, string password)
        {
            var auth = await _authProvider.CreateUserWithEmailAndPasswordAsync(email, password);
            return auth.FirebaseToken;
        }
    }
}
