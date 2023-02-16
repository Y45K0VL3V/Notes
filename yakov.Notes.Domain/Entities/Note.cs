using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

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

        ////TODO: Check later sync of edited notes
        ///public bool IsEdited { get; set; } = false;
    }
}
