using yakov.Notes.Domain.Entities;

namespace yakov.Notes.Domain.Interfaces
{
    public interface INotesRemoteRepositoryControl
    {
        public Task AddNote(Note note);
        public Task UpdateNote(Note note);
        public Task DeleteNote(Guid noteGuid);
        public Task<List<Note>> GetAllNotes();
    }
}
