using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yakov.Notes.Domain.Entities
{
    public class Note
    {
        public int Id { get; set; }
        public Guid Guid { get; set; } = new Guid();
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime LastTimeModified { get; set; }
    }
}
