using yakov.Notes.Domain.Entities;

namespace yakov.Notes.Domain.Interfaces
{
    public interface INotesRepositoryControl
    {
        public Task<Note> AddNote(Note note);
        public Task<Note?> UpdateNote(Note note);
        public Task<Note?> GetNoteByGuid(Guid noteGuid);
        
        public Task DeleteNote(Guid noteGuid);

        public Task<List<Note>> SearchNotes(string searchStr);

    }
}
