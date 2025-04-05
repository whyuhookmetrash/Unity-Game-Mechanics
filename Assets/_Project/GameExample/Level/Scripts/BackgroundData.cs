using UnityEngine;

namespace ShootEmUp
{
    [CreateAssetMenu(fileName = "BackgroundData", menuName = "Level/BackgroundData")]
    public sealed class BackgroundData : ScriptableObject
    {
        public float startPositionY;
        public float endPositionY;
        public float movingSpeedY;
    }
}