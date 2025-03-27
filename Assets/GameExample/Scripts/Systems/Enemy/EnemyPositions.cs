using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyPositions
    {
        private readonly Transform[] spawnPositions;

        private readonly Transform[] attackPositions;

        public EnemyPositions(EnemyPositionsConfig config)
        {
            this.spawnPositions = config.spawnPositions;
            this.attackPositions = config.attackPositions;
        }

        public Transform RandomSpawnPosition()
        {
            return this.RandomTransform(this.spawnPositions);
        }

        public Transform RandomAttackPosition()
        {
            return this.RandomTransform(this.attackPositions);
        }

        private Transform RandomTransform(Transform[] transforms)
        {
            int index = UnityEngine.Random.Range(0, transforms.Length);
            return transforms[index];
        }
    }

    [Serializable]
    public sealed class EnemyPositionsConfig
    {
        public Transform[] spawnPositions;

        public Transform[] attackPositions;
    }
}