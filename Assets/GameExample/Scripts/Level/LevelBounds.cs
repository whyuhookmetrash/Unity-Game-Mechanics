using UnityEngine;

namespace ShootEmUp
{
    public sealed class LevelBounds : GameMonoBehaviour
    {
        [SerializeField]
        private Transform leftBorder;

        [SerializeField]
        private Transform rightBorder;

        [SerializeField]
        private Transform downBorder;

        [SerializeField]
        private Transform topBorder;
        
        public bool InBounds(Vector3 position)
        {
            float positionX = position.x;
            float positionY = position.y;
            return positionX > this.leftBorder.position.x
                   && positionX < this.rightBorder.position.x
                   && positionY > this.downBorder.position.y
                   && positionY < this.topBorder.position.y;
        }
    }
}