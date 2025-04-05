using System.Collections.Generic;
using Zenject;
using UnityEngine;

namespace ShootEmUp
{
    public interface IDamageTaker
    {
        TeamComponent TeamComponent { get; }
        void TakeDamage(int damage);
    }
}