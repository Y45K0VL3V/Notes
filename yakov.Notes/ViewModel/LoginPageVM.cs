using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using yakov.Notes.Domain.Interfaces;
using yakov.Notes.Navigation;

namespace yakov.Notes.ViewModel
{
    public partial class LoginPageVM : BaseVM
    {
        public LoginPageVM(IAuthService authService, INavigationService navigationService)
        {
            _navigationService = navigationService;
            _authService = authService;
        }

        private INavigationService _navigationService;
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

                await _navigationService.NavigateToMainPage();
            }
            catch (Exception ex) 
            {
                await App.Current.MainPage.DisplayAlert("Alert", ex.Message, "OK");
            }
        }

        [RelayCommand]
        private async void SignUp()
        {
            await _navigationService.NavigateToRegisterPage();
        }
    }
}
