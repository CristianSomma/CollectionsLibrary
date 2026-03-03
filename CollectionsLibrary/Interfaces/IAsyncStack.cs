using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionsLibrary.Interfaces
{
    public interface IAsyncStack<T>
    {
        Task Push(T item);
        Task<T> Peek();
        Task<T> Pop();
    }
}
