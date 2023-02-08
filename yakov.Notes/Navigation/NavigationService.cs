using Microsoft.Maui.Controls.Internals;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using yakov.Notes.Domain.Entities;
using yakov.Notes.ViewModel;
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

        private INavigation Navigation
        {
            get => Application.Current?.MainPage?.Navigation;
        }

        public Task NavigateToLoginPage() => NavigateToPage<LoginPage>();

        public Task NavigateToRegisterPage() => NavigateToPage<RegisterPage>();
        public async Task NavigateToMainPage()
        {
            await NavigateToPage<MainPage>();

            var pagesToRemove = Navigation.NavigationStack.Where(p => p is not MainPage).ToList();
            for (int i = 0; i < pagesToRemove.Count(); i++)
                Navigation.RemovePage(pagesToRemove[i]);
        }

        public async Task NavigateToNotePage(Note note = null)
        {
            if (note is null)
            {
                await NavigateToPage<NoteEditorPage>();
            }
            else
                await NavigateToPage<NoteEditorPage>(note);
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

        private async Task NavigateToPage<T>(object parameter = null) where T : Page
        {
            var destPage = ResolvePage<T>();

            if (destPage is not null)
            {
                var toViewModel = GetPageViewModelBase(destPage);

                if (toViewModel is not null)
                    await toViewModel.OnNavigatingTo(parameter);

                await Navigation.PushAsync(destPage, true);
            }
            else
                throw new InvalidOperationException($"Unable to resolve type {typeof(T).FullName}");
        }

        private BaseVM GetPageViewModelBase(Page p) => p?.BindingContext as BaseVM;

        private T ResolvePage<T>() where T : Page
            => _services.GetService<T>();

    }
}
