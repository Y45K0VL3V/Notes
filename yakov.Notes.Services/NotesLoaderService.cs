using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yakov.Notes.Domain.Comparers;
using yakov.Notes.Domain.Entities;
using yakov.Notes.Domain.Interfaces;

namespace yakov.Notes.Services
{
    public class NotesLoaderService : INotesLoaderService
    {
        public NotesLoaderService(INotesRepositoryControl localDB, INotesRemoteRepositoryControl remoteDB)
        {
            _localDB = localDB;
            _remoteDB = remoteDB;
        }

        private INotesRepositoryControl _localDB;
        private INotesRemoteRepositoryControl _remoteDB;

        ////TODO: Check if try works faster than NetworkAccess
        public async Task SyncWithRemote()
        {
            try
            {
                var localNotes = await _localDB.GetNotes(string.Empty);

                var remoteNotes = await _remoteDB.GetAllNotes();

                var notesToAdd = localNotes.Except(remoteNotes, new NoteComparer());
          
                if (notesToAdd is not null)
                    foreach (var note in notesToAdd)
                    {
                        await _remoteDB.AddNote(note);
                    }

                notesToAdd = remoteNotes.Except(localNotes, new NoteComparer());

                if (notesToAdd is not null)
                    foreach (var note in notesToAdd)
                    {
                        await _localDB.AddNote(note);
                    }
            }
            catch { }
        }

        public async Task<List<Note>> GetLocalNotes(string authEmail)
        {
            var localNotes = await _localDB.GetNotes(string.Empty);
            return localNotes.Where(n => (n.CreatorEmail == authEmail) || n.IsShared).ToList();
        }

        public async Task DeleteNote(Guid noteGuid)
        {
            await _localDB.DeleteNote(noteGuid);
            try
            {
                await _remoteDB.DeleteNote(noteGuid);
            }
            catch { }
        }

        public async Task AddNote(Note note)
        {
            await _localDB.AddNote(note);
            try
            {
                await _remoteDB.AddNote(note);
            }
            catch { }
        }

        public async Task UpdateNote(Note note)
        {
            await _localDB.UpdateNote(note);
            try
            {
                await _remoteDB.UpdateNote(note);
            }
            catch { }
        }
    }
}
