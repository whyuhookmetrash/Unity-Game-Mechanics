using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyPositionSystem
    {
        private readonly EnemyPositionProvider enemyPositionProvider;

        public EnemyPositionSystem(EnemyPositionProvider enemyPositionProvider)
        {
            this.enemyPositionProvider = enemyPositionProvider;
        }

        public Transform RandomSpawnPosition()
        {
            return this.RandomTransform(this.enemyPositionProvider.spawnPositions);
        }

        public Transform RandomAttackPosition()
        {
            return this.RandomTransform(this.enemyPositionProvider.attackPositions);
        }

        private Transform RandomTransform(Transform[] transforms)
        {
            int index = UnityEngine.Random.Range(0, transforms.Length);
            return transforms[index];
        }
    }
}