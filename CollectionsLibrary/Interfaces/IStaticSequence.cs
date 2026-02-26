using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionsLibrary.Interfaces
{
    public interface IStaticSequence<T>
    {
        T GetAt(int index);
        void SetAt(int index, T newItem);
    }
}
