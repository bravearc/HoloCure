using System.Collections.Generic;
using System;

namespace Util.Pool 
{
    public class ObjectPool<T> : IObjectPool<T>, IDisposable where T : class
    {
        private readonly Stack<T> _pool;
        readonly Func<T> _createFunc;
        readonly Action<T> _actionOnGet;
        readonly Action<T> _actionOnRelease;
        readonly Action<T> _actionOnDestroy;
        readonly int _max_Size;

        public int CountAll { get; private set; }
        public int CountInactive => _pool.Count;

        public ObjectPool(Func<T> createFunc,
            Action<T> onTakeAction = null,
            Action<T> onReturned = null,
            Action<T> onDestroy = null,
            int defaultCapacity = 0,
            int max_Size = 10_000)
        {
            if (createFunc == null)
            {
                throw new ArgumentNullException("createFunc");
            }
            if (max_Size <= 0)
            {
                throw new ArgumentException("min_Size");
            }
            _pool = new Stack<T>(defaultCapacity);
            _createFunc = createFunc;
            _actionOnGet = onTakeAction;
            _actionOnRelease = onReturned;
            _actionOnDestroy = onDestroy;
            _max_Size = max_Size;
        }

        public T Get()
        {
            T instance;
            if (CountInactive > 0)
            {
                instance = _pool.Pop();
            }
            else
            {
                instance = _createFunc();
                CountAll += 1;
            }
            _actionOnGet?.Invoke(instance);
            return instance;
        }

        public PooledObject<T> Get(out T value)
        {
            return new PooledObject<T>(value = Get(), this);
        }

        public void Release(T element)
        {
            _actionOnRelease?.Invoke(element);
            if(CountInactive < _max_Size)
            {
                _pool.Push(element);
            }
            else
            {
                _actionOnDestroy?.Invoke(element);  
            }
        }
        public void Clear()
        {
             if (_actionOnDestroy != null)
            {
                foreach (T item in _pool)
                {
                    _actionOnDestroy(item);
                }
            }
        }
        public void Dispose()
        {
            Clear();
        }

    }
}