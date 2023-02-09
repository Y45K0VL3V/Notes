using yakov.Notes.Domain.Entities;

namespace yakov.Notes.Domain.Interfaces
{
    public interface INotesLoaderService
    {
        public Task SyncWithRemote();
        public Task<List<Note>> GetLocalNotes();
        public Task AddNote(Note note);
        public Task UpdateNote(Note note);
        public Task DeleteNote(Guid noteGuid);
    }
}
