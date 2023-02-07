using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using yakov.Notes.Domain.Interfaces;

namespace yakov.Notes.ViewModel
{
    public partial class LoginPageVM : ObservableObject
    {
        public LoginPageVM(IAuthService authService)
        {
            _authService = authService;
        }

        private IAuthService _authService;

        [ObservableProperty]
        private string _userEmail;

        [ObservableProperty]
        private string _userPassword;

        [RelayCommand]
        private async void SignIn()
        {
            try
            {
                var authConnection = await _authService.SignInAsync(_userEmail, _userPassword);
                Preferences.Set("FreshFirebaseToken", authConnection);
                Preferences.Set("Email", _userEmail);
            }
            catch (Exception ex) 
            {
                await App.Current.MainPage.DisplayAlert("Alert", ex.Message, "OK");
                throw;
            }
        }

        [RelayCommand]
        private async void SignUp()
        {
            
        }
    }
}
