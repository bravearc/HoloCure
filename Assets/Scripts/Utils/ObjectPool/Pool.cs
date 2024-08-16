using UnityEngine;

namespace Util.Pool
{

    public class Pool<T> where T : Component
    {
        private GameObject _container;
        protected ObjectPool<T> _pool;
        public T Get() => _pool.Get();
        public void Clear() => _pool.Clear();
        public void Dispose() => _pool.Dispose();
        public void Release(T element) => _pool.Release(element);
        public void Init(GameObject container)
        {
            _container = container;
            _pool = new(Create, OnGet, OnRelease, OnDestroy);
        }

        protected virtual T Create()
        {
            return Manager.Asset.InstantiateObject($"{typeof(T)}", _container.transform)
                .GetComponent<T>();
        }

        protected virtual void OnGet(T element) 
        {
            element.gameObject.SetActive(true);
        }

        protected virtual void OnRelease(T element) 
        {
            element.gameObject.SetActive(false);
        }

        protected virtual void OnDestroy(T element) 
        {
            Manager.Asset.Destroy(element.gameObject);
        }
    }
}