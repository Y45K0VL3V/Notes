using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using yakov.Notes.Application.LocalDB;
using yakov.Notes.Domain.Entities;
using yakov.Notes.Domain.Interfaces;
using yakov.Notes.Navigation;

namespace yakov.Notes.ViewModel
{

    public partial class MainPageVM : BaseVM
    {
        public MainPageVM(INavigationService navigationService, INotesLoaderService notesLoader)
        {
            _navigationService = navigationService;
            _notesLoader = notesLoader;

            RefreshNotes();
        }

        private INavigationService _navigationService;
        private INotesLoaderService _notesLoader;

        [ObservableProperty]
        private int _currentIndex;

        private ObservableCollection<Note> _availableNotes = new();

        [ObservableProperty]
        private ObservableCollection<Note> _displayNoteItems = new();
        
        private async void RefreshNotes()
        {
            await _notesLoader.SyncWithRemote();

            _availableNotes.Clear();
            (await _notesLoader.GetLocalNotes()).ForEach(n => _availableNotes.Add(n));
        }

        [RelayCommand]
        private void ShowPrivateNotes() => ShowNotesGroup(NoteGroupType.Private);

        [RelayCommand]
        private void ShowSharedNotes() => ShowNotesGroup(NoteGroupType.Shared);

        private CancellationTokenSource _loadNotesCancellationToken = new();

        //private void ShowNotesGroup(NoteGroupType noteGroup)
        //{
        //    _loadNotesCancellationToken.Cancel();
        //    DisplayNoteItems.Clear();

        //    _loadNotesCancellationToken = new();

        //    switch (noteGroup)
        //    {
        //        case NoteGroupType.Private:
        //            LoadNotesToDisplay(_loadNotesCancellationToken.Token, _availableNotes.Where(n => !n.IsShared));
        //            break;

        //        case NoteGroupType.Shared:
        //            LoadNotesToDisplay(_loadNotesCancellationToken.Token, _availableNotes.Where(n => n.IsShared));
        //            break;
        //    }
        //}
        private void ShowNotesGroup(NoteGroupType noteGroup)
        {
            _loadNotesCancellationToken.Cancel();
            DisplayNoteItems.Clear();

            _loadNotesCancellationToken = new();

            IEnumerable<Note> notes;
            switch (noteGroup)
            {
                case NoteGroupType.Private:
                    notes = _availableNotes.Where(n => !n.IsShared);
                    break;
                case NoteGroupType.Shared:
                    notes = _availableNotes.Where(n => n.IsShared);
                    break;
                default:
                    notes = Enumerable.Empty<Note>();
                    break;
            }

            LoadNotesToDisplay(_loadNotesCancellationToken.Token, notes);
        }

        private async void LoadNotesToDisplay(CancellationToken token, IEnumerable<Note> notes)
        {
            await Task.Run(() =>
            {
                foreach (var currNote in notes)
                {
                    if (token.IsCancellationRequested)
                        return;

                    DisplayNoteItems.Add(currNote);
                }
            });
        }



        [RelayCommand]
        private async void CreateNote()
        {
            await _navigationService.NavigateToNotePage(null);
        }

        [RelayCommand]
        private void OpenNote()
        {
            
        }
    }
}
