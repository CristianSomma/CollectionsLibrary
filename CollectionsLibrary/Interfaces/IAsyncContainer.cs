using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionsLibrary.Interfaces
{
    public interface IAsyncContainer<T>
    {
        void Build(IEnumerable<T> items);
        Task Clear();
        Task<int> GetLength();
        Task<bool> IsEmpty();
    }
}
