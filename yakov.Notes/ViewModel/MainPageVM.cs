using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using yakov.Notes.Domain.Entities;
using yakov.Notes.Navigation;

namespace yakov.Notes.ViewModel
{
    public partial class MainPageVM : BaseVM
    {
        public MainPageVM(INavigationService navigationService)
        {            
            _navigationService = navigationService;
        }

        private INavigationService _navigationService;

        [ObservableProperty]
        private int _currentIndex;

        [ObservableProperty]
        private ObservableCollection<Note> _displayNoteItems = new();

        [RelayCommand]
        private async void CreateNote()
        {
            await _navigationService.NavigateToNotePage(null);
        }

        [RelayCommand]
        private void OpenNote()
        {
            
        }

        private void OpenNoteEditor()
        {

        }
    }
}
