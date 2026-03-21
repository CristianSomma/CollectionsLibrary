using CollectionsLibrary.Interfaces;

namespace CollectionsLibrary.Collections
{
    public class SharedQueue<T>
        : IAsyncContainer<T>, IAsyncQueue<T>
    {
        // coda usata dal wrapper
        private Queue<T> _queue;

        // mutex per accesso alle azioni della coda e semaforo
        // che tiene conto di quanti sono gli elementi nella coda disponibili
        private readonly SemaphoreSlim _mutex, _itemsAvailable;
        private readonly CancellationToken _token;

        public SharedQueue(CancellationToken token = default)
        {
            _queue = new Queue<T>();
            _mutex = new SemaphoreSlim(1, 1);
            _itemsAvailable = new SemaphoreSlim(0);
            _token = token;
        }

        #pragma warning disable CS8618
        public SharedQueue(IEnumerable<T> items, CancellationToken token = default)
        {
            Build(items);
            _mutex = new SemaphoreSlim(1, 1);
            _itemsAvailable = new SemaphoreSlim(items.Count());
            _token = token;
        }
        #pragma warning restore CS8618

        public void Build(IEnumerable<T> items)
        {
            _queue = new Queue<T>(items);
        }

        public async Task Clear()
        {
            await _mutex.WaitAsync(_token);

            try
            {
                _queue.Clear();

                // finché il contatore del semaforo è maggiore di zero,
                // allora viene aspettato con attesa di zero millisecondi
                // per decrementare di 1 il contatore fino a zero
                while (_itemsAvailable.CurrentCount > 0)
                    _itemsAvailable.Wait(0);
            }
            finally
            {
                _mutex.Release();
            }
        }

        public async Task<int> GetLength()
        {
            await _mutex.WaitAsync(_token);

            try
            {
                return _queue.GetLength();
            }
            finally
            {
                _mutex.Release();
            }
        }

        public async Task<bool> IsEmpty()
        {
            await _mutex.WaitAsync(_token);

            try
            {
                return _queue.IsEmpty();
            }
            finally
            {
                _mutex.Release();
            }
        }

        public async Task<T> Peek()
        {
            await _mutex.WaitAsync(_token);

            try
            {
                return _queue.Peek();
            }
            finally
            {
                _mutex.Release();
            }
        }

        public async Task<T> Dequeue()
        {
            await _itemsAvailable.WaitAsync(_token);

            await _mutex.WaitAsync(_token);

            try
            {
                return _queue.Dequeue();
            }
            finally
            {
                _mutex.Release();
            }
        }

        public async Task Enqueue(T item)
        {
            await _mutex.WaitAsync(_token);

            try
            {
                _queue.Enqueue(item);
                _itemsAvailable.Release();
            }
            finally
            {
                _mutex.Release();
            }
        }

    }
}
