using yakov.Notes.Domain.Entities;

namespace yakov.Notes.Domain.Interfaces
{
    public interface INotesRepositoryControl
    {
        public Task AddNote();
        public Task DeleteNote();
        public Task UpdateNote();

        public Task<List<Note>> SearchNotes();

    }
}
