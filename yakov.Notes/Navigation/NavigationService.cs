using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using yakov.Notes.Domain.Entities;
using yakov.Notes.Views;

namespace yakov.Notes.Navigation
{
    public class NavigationService : INavigationService
    {
        public NavigationService(IServiceProvider services)
        {
            _services = services;
        }

        private readonly IServiceProvider _services;

        private INavigation _navigation;
        private INavigation Navigation
        {
            get => Application.Current?.MainPage?.Navigation;
        }

        public Task NavigateToLoginPage() => NavigateToPage<LoginPage>();

        public Task NavigateToRegisterPage() => NavigateToPage<RegisterPage>();
        public async Task NavigateToMainPage()
        {
            await NavigateToPage<MainPage>();

            Application.Current.MainPage = ResolvePage<MainPage>();
        }

        public Task NavigateToNotePage(Note note)
        {
            throw new NotImplementedException();
        }

        public Task NavigateBack()
        {
            if (Navigation.NavigationStack.Count > 1)
                return Navigation.PopAsync();
            throw new InvalidOperationException("No pages to navigate back to!");

        }

        private Task NavigateToPage<T>() where T : Page
        {
            var page = ResolvePage<T>();
            if (page is not null)
                return Navigation.PushAsync(page, true);

            throw new InvalidOperationException($"Unable to resolve type {typeof(T).FullName}");
        }
        private T ResolvePage<T>() where T : Page
            => _services.GetService<T>();

    }
}
