using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yakov.Notes.Domain.Entities;
using yakov.Notes.Domain.Interfaces;

namespace yakov.Notes.Application.RemoteDB
{
    public class FirebaseDBControl : INotesRemoteRepositoryControl
    {
        private FirebaseClient _client = new(FirebaseDBSettings.FirebaseDBUrl, new FirebaseOptions
        {
            AuthTokenAsyncFactory = () => Task.FromResult(FirebaseDBSettings.FirebaseDBSecret)
        });

        public async Task AddNote(Note note)
        {
            //await _client.Child(nameof(Note)).PostAsync(note);
            await _client.Child(nameof(Note)).Child(note.Guid.ToString()).PutAsync(note);
        }

        public async Task DeleteNote(Guid noteGuid)
        {
            await _client.Child(nameof(Note)).Child(noteGuid.ToString()).DeleteAsync();
        }

        public async Task UpdateNote(Note note)
        {
            await _client.Child(nameof(Note)).Child(note.Guid.ToString()).PutAsync(note);
        }

        public async Task<List<Note>> GetAllNotes()
        {
            return (await _client.Child(nameof(Note)).OnceAsync<Note>()).Select(n => new Note()
            {
                Guid = n.Object.Guid,
                CreatorEmail = n.Object.CreatorEmail,
                Title = n.Object.Title,
                Content = n.Object.Content,
                CreatedTime = n.Object.CreatedTime,
                LastTimeModified = n.Object.LastTimeModified,
                IsShared = n.Object.IsShared
            }).ToList();
        }
    }
}
