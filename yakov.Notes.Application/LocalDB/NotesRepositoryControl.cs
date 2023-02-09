using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using yakov.Notes.Domain.Entities;
using yakov.Notes.Domain.Interfaces;

namespace yakov.Notes.Application.LocalDB
{
    public class NotesRepositoryControl : INotesRepositoryControl
    {
        private NotesContext _context;
        public NotesRepositoryControl(NotesContext context)
        {
            _context = context;
        }

        public async Task<Note> AddNote(Note note)
        {
            await _context.AddAsync(note);
            await _context.SaveChangesAsync();
            return note;
        }
        public async Task<Note?> GetNoteByGuid(Guid noteGuid) => 
            await _context.Notes.FirstOrDefaultAsync(n => n.Guid == noteGuid);

        public async Task DeleteNote(Guid noteGuid)
        {
            Note? noteToDelete = _context.Notes.FirstOrDefault(n => n.Guid == noteGuid);
            if (noteToDelete is null) return;

            _context.Notes.Remove(noteToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<Note?> UpdateNote(Note note)
        {
            if (note is null) return null;

            Note? noteToUpdate = await GetNoteByGuid(note.Guid);
            if (noteToUpdate is null) return null;

            noteToUpdate = note;
            return noteToUpdate;
        }

        public async Task<List<Note>> GetNotes(string searchStr)
        {
            List<Note> notes = new();

            await Task.Run(() =>
            {
                // Two searches for order by find in title and content separatly
                notes.AddRange(_context.Notes.Where(n => n.Title.Contains(searchStr)));
                notes.AddRange(_context.Notes.Where(n => n.Content.Contains(searchStr)));
            });

            return notes.DistinctBy(n => n.Guid).ToList();
        }

    }
}
