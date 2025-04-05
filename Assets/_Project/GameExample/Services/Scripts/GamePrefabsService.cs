using System;
using UnityEngine;

namespace ShootEmUp
{
    [CreateAssetMenu(fileName = "GamePrefabsService", menuName = "Game/GamePrefabsService")]
    public sealed class GamePrefabsService : ScriptableObject
    {
        public Enemy EnemyPrefab => enemyPrefab;
        [SerializeField] private Enemy enemyPrefab;

        public Player PlayerPrefab => playerPrefab;
        [SerializeField] private Player playerPrefab;

        public Bullet BulletPrefab => bulletPrefab;
        [SerializeField] private Bullet bulletPrefab;
    }
}