namespace yakov.Notes.Domain.Interfaces
{
    public interface INotesLoaderService
    {
        public Task SyncWithRemote();
    }
}
