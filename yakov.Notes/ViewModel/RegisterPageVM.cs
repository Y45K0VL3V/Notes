using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using yakov.Notes.Domain.Interfaces;

namespace yakov.Notes.ViewModel
{
    public partial class RegisterPageVM : ObservableObject
    {
        public RegisterPageVM(IAuthService authService)
        {
            _authService = authService;
        }

        private IAuthService _authService;

        [ObservableProperty]
        private string _userEmail;

        [ObservableProperty]
        private string _userPassword;

        [RelayCommand]
        private async void SignUp()
        {
            try
            {
                var token = await _authService.SignUpAsync(_userEmail, _userPassword);
                if (token is not null)
                    await App.Current.MainPage.DisplayAlert("Alert", "User Registered successfully", "OK");
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Alert", ex.Message, "OK");
                throw;
            }
        }
    }
}
