using Android.OS;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Collections.Concurrent;
using System.Collections.ObjectModel;
using yakov.Notes.Application.LocalDB;
using yakov.Notes.Domain.Comparers;
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

            InitPage();
        }

        private INavigationService _navigationService;
        private INotesLoaderService _notesLoader;
        private string _authEmail;

        private async void InitPage()
        {
            Connectivity.ConnectivityChanged += OnNetworkAccessChanged;

            _authEmail = await SecureStorage.GetAsync("yakovNotesEmail");
            await RefreshNotes();
            await ShowPrivateNotes();
        }

        private void OnNetworkAccessChanged(object sender, ConnectivityChangedEventArgs e)
        {
            NetworkAccess = Connectivity.Current.NetworkAccess;
        }

        private ConcurrentDictionary<Guid, Note> _availableNotes = new();

        [ObservableProperty]
        private ObservableCollection<Note> _displayNoteItems = new();

        private int _isRefreshing = 0;
        private async Task RefreshNotes()
        {
            if (Interlocked.Exchange(ref _isRefreshing, 1) == 1)
                return;

            await _notesLoader.SyncWithRemote();
            var localNotes = await _notesLoader.GetLocalNotes(_authEmail);
            
            foreach (var note in localNotes)
                _availableNotes.TryAdd(note.Guid, note);

            foreach (var note in _availableNotes.Values.Except(localNotes, new NoteComparer()))
                _availableNotes.TryRemove(note.Guid, out _);

            Interlocked.Exchange(ref _isRefreshing, 0);
        }

        [RelayCommand]
        private async Task ShowPrivateNotes()
        {
            await ShowNotesGroup(NoteGroupType.Private);
        }

        [RelayCommand]
        private async Task ShowSharedNotes()
        {
            await ShowNotesGroup(NoteGroupType.Shared);
        }
        
        private CancellationTokenSource _loadNotesCancellationToken = new();
        private readonly object _tokenLocker = new();
        private void ResetToken()
        {
            lock (_tokenLocker)
            {
                _loadNotesCancellationToken.Cancel();
                _loadNotesCancellationToken = new();
            }
        }

        private bool _isSharedDisplay = false;
        private async Task ShowNotesGroup(NoteGroupType noteGroup)
        {
            ResetToken();

            await Task.Run( async () =>
            {
                await RefreshNotes();

                IEnumerable<Note> notes;
                switch (noteGroup)
                {
                    case NoteGroupType.Private:
                        _isSharedDisplay = false;
                        notes = _availableNotes.Values.Where(n => !n.IsShared);
                        break;
                    case NoteGroupType.Shared:
                        _isSharedDisplay = true;
                        notes = _availableNotes.Values.Where(n => n.IsShared);
                        break;
                    default:
                        notes = Enumerable.Empty<Note>();
                        break;
                }

                await LoadNotesToDisplay(_loadNotesCancellationToken.Token, notes);

            }, _loadNotesCancellationToken.Token);
        }

        private readonly object _notesLock = new object();
        private async Task LoadNotesToDisplay(CancellationToken token, IEnumerable<Note> notes)
        {
            await Task.Run(() =>
            {
                lock (_notesLock)
                {
                    DisplayNoteItems.Clear();

                    foreach (var currNote in notes)
                    {
                        DisplayNoteItems.Add(currNote);
                    }
                }
            }, token);
        }

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                SetProperty(ref _searchText, value);

                var searchedNotes = _availableNotes.Values.Where(n => !(n.IsShared ^ _isSharedDisplay) && 
                                                                (n.Title.Contains(value) || n.Content.Contains(value))).ToList();
                ResetToken();
                LoadNotesToDisplay(_loadNotesCancellationToken.Token, searchedNotes);

            }
        }


        [RelayCommand]
        private async void CreateNote()
        {
            await _navigationService.NavigateToNotePage(null);
        }

        [ObservableProperty]
        private Note _noteToOpen;

        [RelayCommand]
        private async void OpenNote()
        {
            await _navigationService.NavigateToNotePage(NoteToOpen);
        }

        [ObservableProperty]
        private NetworkAccess _networkAccess = Connectivity.Current.NetworkAccess;

        [RelayCommand]
        private async void LogOut()
        {
            await _navigationService.NavigateToLoginPage();
            SecureStorage.Remove("yakovNotesEmail");
            SecureStorage.Remove("yakovNotesFreshFirebaseToken");
        }
    }
}
