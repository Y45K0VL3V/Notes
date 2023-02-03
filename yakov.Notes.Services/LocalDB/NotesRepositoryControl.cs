using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yakov.Notes.Domain.Entities;
using yakov.Notes.Domain.Interfaces;

namespace yakov.Notes.Services.LocalDB
{
    public class NotesRepositoryControl : INotesRepositoryControl
    {
        private NotesContext _context;
        public NotesRepositoryControl(NotesContext context)
        {
            _context = context;
        }

        public Task AddNote()
        {
            throw new NotImplementedException();
        }

        public Task DeleteNote()
        {
            throw new NotImplementedException();
        }

        public Task<List<Note>> SearchNotes()
        {
            throw new NotImplementedException();
        }

        public Task UpdateNote()
        {
            throw new NotImplementedException();
        }
    }
}
