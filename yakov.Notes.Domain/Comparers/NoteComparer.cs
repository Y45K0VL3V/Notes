using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yakov.Notes.Domain.Entities;

namespace yakov.Notes.Domain.Comparers
{
    public class NoteComparer : IEqualityComparer<Note>
    {
        public bool Equals(Note? x, Note? y)
        {
            return x?.Guid == y?.Guid;
        }

        public int GetHashCode([DisallowNull] Note obj)
        {
            if (ReferenceEquals(obj, null))
                return 0;

            return obj.Guid.GetHashCode();
        }
    }
}
