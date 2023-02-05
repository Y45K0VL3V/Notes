using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public async Task SyncWithRemote()
        {
            var localNotes = await _localDB.GetNotes(string.Empty);
            var remoteNotes = await _remoteDB.GetAllNotes();

            var notesToAdd = localNotes.Except(remoteNotes);
            foreach (var note in notesToAdd)
            {
                await _remoteDB.AddNote(note);
            }

            notesToAdd = remoteNotes.Except(localNotes);
            foreach (var note in notesToAdd)
            {
                await _localDB.AddNote(note);
            }
        }
    }
}
