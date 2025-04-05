using System;
using UnityEngine;

namespace ShootEmUp
{
    [Serializable]
    public sealed class EnemyPositionProvider
    {
        public Transform[] spawnPositions;
        public Transform[] attackPositions;
    }
}