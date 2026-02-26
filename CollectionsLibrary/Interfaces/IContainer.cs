using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionsLibrary.Interfaces
{
    public interface IContainer<T>
    {
        void Build(IEnumerable<T> items);
        void Clear();
        int GetLength();
        bool IsEmpty();
    }
}
