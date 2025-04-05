using System;
using UnityEngine;

namespace ShootEmUp
{
    [Serializable]
    public sealed class WorldTransformProvider
    {
        [Header("Pool Transforms")]
        public Transform enemyPoolTransform;
        public Transform enemyWorldTransform;
        public Transform bulletPoolTransform;
        public Transform bulletWorldTransform;
        public Transform worldTransform;

        [Header("Player Transforms")]
        public Transform playerSpawnTransform;

        [Header("Level Bounds Transforms")]
        public Transform leftBorder;
        public Transform rightBorder;
        public Transform downBorder;
        public Transform topBorder;
        public Transform background;
    }
}