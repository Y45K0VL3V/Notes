using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yakov.Notes.Domain.Entities;
using yakov.Notes.Domain.Interfaces;
using yakov.Notes.Navigation;

namespace yakov.Notes.ViewModel
{
    public partial class NoteEditorPageVM : BaseVM
    {
        public NoteEditorPageVM(INotesLoaderService notesLoader, INavigationService navigationService)
        {
            _notesLoader = notesLoader;
            _navigationService = navigationService;
        }

        private INavigationService _navigationService;
        private INotesLoaderService _notesLoader;

        private Note _existingNote;
        private void SetExistingNote(Note note)
        {
            _existingNote = note;

            NoteTitle = note.Title;
            NoteContent = note.Content;
            IsShared = note.IsShared;
            CreatorEmail = note.CreatorEmail;
            CreatedTime = note.CreatedTime.ToShortDateString();
        }

        [ObservableProperty]
        private string _noteTitle;
        [ObservableProperty]
        private string _noteContent;
        [ObservableProperty]
        private bool _isShared = false;
        [ObservableProperty]
        private string _creatorEmail = "";
        [ObservableProperty]
        private string _createdTime = DateTime.Now.ToShortDateString();

        [RelayCommand]
        private void SwitchShareMode()
        {
            IsShared = !IsShared;
        }

        [RelayCommand]
        private async Task SaveNote()
        {
            Note note = _existingNote;
            if (note is null)
            {
                string email = await SecureStorage.GetAsync("yakovNotesEmail");

                if (email is null)
                    await App.Current.MainPage.DisplayAlert("Alert", "Email not provided", "OK");

                note = new()
                {
                    Title = NoteTitle,
                    Content = NoteContent,
                    IsShared = IsShared,
                    CreatedTime = DateTime.Now,
                    LastTimeModified = DateTime.Now,
                    CreatorEmail = email,
                };

                await _notesLoader.AddNote(note);
            }
            else
            {
                note.Title = NoteTitle;
                note.Content = NoteContent;
                note.IsShared = IsShared;
                note.LastTimeModified = DateTime.Now;
                await _notesLoader.UpdateNote(note);
            }

            await _navigationService.NavigateBack();
        }

        public override Task OnNavigatingTo(object parameter)
        {
            SetExistingNote((Note) parameter);
            return base.OnNavigatingTo(parameter);
        }
    }
}
