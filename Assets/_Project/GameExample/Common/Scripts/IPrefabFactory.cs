using System.Collections.Generic;
using Zenject;
using UnityEngine;

namespace ShootEmUp
{
    public interface IMonoHandler { } //Marker

    public interface IPrefabFactory
    {
        T CreatePrefab<T>(Object prefab, Transform parent) where T : IMonoHandler;
    }
}