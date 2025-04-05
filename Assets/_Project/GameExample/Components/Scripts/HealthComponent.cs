using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class HealthComponent
    {
        public event Action OnHpEmpty;
        
        private int health;

        public void Init(int health)
        {
            this.health = health;
        }

        public void TakeDamage(int damage)
        {
            this.health -= damage;
            if (this.health <= 0)
            {
                this.OnHpEmpty?.Invoke();
            }
        }
    }
}