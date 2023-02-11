﻿using System;
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

        public async Task SyncWithRemote()
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

        public Task<List<Note>> GetLocalNotes() => _localDB.GetNotes(string.Empty);

        public async Task DeleteNote(Guid noteGuid)
        {
            await _localDB.DeleteNote(noteGuid);
            await _remoteDB.DeleteNote(noteGuid);
        }

        public async Task AddNote(Note note)
        {
            await _localDB.AddNote(note);
            await _remoteDB.AddNote(note);
        }

        public async Task UpdateNote(Note note)
        {
            await _localDB.UpdateNote(note);
            await _remoteDB.UpdateNote(note);
        }
    }
}
