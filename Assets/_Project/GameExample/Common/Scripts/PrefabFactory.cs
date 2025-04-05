using System.Collections.Generic;
using Zenject;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class PrefabFactory : IPrefabFactory
    {
        private readonly IInstantiator instantiator;

        public PrefabFactory(IInstantiator instantiator)
        {
            this.instantiator = instantiator;
        }

        public T CreatePrefab<T>(Object prefab, Transform parent) where T : IMonoHandler
        {
            T prefabHandler = this.instantiator.InstantiatePrefabForComponent<T>(prefab, parent);
            return prefabHandler;
        }
    }
}