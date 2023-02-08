using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yakov.Notes.Domain.Entities;

namespace yakov.Notes.ViewModel
{
    public partial class NoteEditorPageVM : BaseVM
    {
        private Note _existingNote;
        private void SetExistingNote(Note note)
        {
            _existingNote = note;

            NoteTitle = note.Title;
            NoteContent = note.Content;
            IsShared = note.IsShared;
            CreatedTime = note.CreatedTime.ToShortDateString();
        }

        [ObservableProperty]
        private string _noteTitle;
        [ObservableProperty]
        private string _noteContent;
        [ObservableProperty]
        private bool _isShared = false;
        [ObservableProperty]
        private string _createdTime;

        [RelayCommand]
        private async Task SaveNote()
        {
            Note note = _existingNote;
            if (note is null)
            {
                string email = Preferences.Get("Email", null);
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
            }
            else
            {
                note.Title = NoteTitle;
                note.Content = NoteContent;
                note.IsShared = IsShared;
            }

        }

        public override Task OnNavigatingTo(object parameter)
        {
            SetExistingNote((Note) parameter);
            return base.OnNavigatingTo(parameter);
        }
    }
}
