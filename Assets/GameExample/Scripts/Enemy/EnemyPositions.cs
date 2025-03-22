using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyPositions : GameMonoBehaviour
    {
        [SerializeField]
        private Transform[] spawnPositions;

        [SerializeField]
        private Transform[] attackPositions;

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
            int index = Random.Range(0, transforms.Length);
            return transforms[index];
        }
    }
}