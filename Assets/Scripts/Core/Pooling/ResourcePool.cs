using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Core.Pooling
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class ResourcePool<TResource>
    {
        private readonly List<TResource> _resources = new();
        private readonly GameObject _parentGameObject;
        private readonly GameObject _prefab;
        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        private int InstanceCount { get; }
        private bool AutoScale { get; }

        public ResourcePool(int instancesCount, GameObject parentGameObject, GameObject prefab, bool autoScale = true)
        {
            _parentGameObject = parentGameObject;
            _prefab = prefab;
            InstanceCount = instancesCount;
            AutoScale = autoScale;

            for (int i = 0; i < instancesCount; i++)
            {
                TResource resource = CreateResource();
                _resources.Add(resource);
            }
        }

        private TResource CreateResource()
        {
            return Object.Instantiate(_prefab, _parentGameObject.transform).GetComponent<TResource>();
        }

        public TResource GetResource()
        {
            if (_resources.Count > 0)
            {
                TResource resource = _resources.First();
                _resources.Remove(resource);
                return resource;
            }
            else
            {
                if (AutoScale)
                {
                    return CreateResource();
                }
            }

            Debug.LogError($"Resource of type: ${typeof(TResource)} not available in pool");
            return default;
        }

        public void PoolBack(TResource resource)
        {
            _resources.Add(resource);
        }
    }
}