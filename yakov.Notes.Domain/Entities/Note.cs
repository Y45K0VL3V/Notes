using System.ComponentModel.DataAnnotations;

namespace yakov.Notes.Domain.Entities
{
    public class Note
    {
        [Key]
        public Guid Guid { get; set; } = new Guid();
        public string CreatorEmail { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime LastTimeModified { get; set; }
        public bool IsShared { get; set; } = false;
    }
}
