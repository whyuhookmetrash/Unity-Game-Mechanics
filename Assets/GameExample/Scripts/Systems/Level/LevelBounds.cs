using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class LevelBounds
    {
        private readonly Transform leftBorder;
        private readonly Transform rightBorder;
        private readonly Transform downBorder;
        private readonly Transform topBorder;

        public LevelBounds(LevelBoundsConfig config)
        {
            this.leftBorder = config.leftBorder;
            this.rightBorder = config.rightBorder;
            this.downBorder = config.downBorder;
            this.topBorder = config.topBorder;

        }

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

    [Serializable]
    public sealed class LevelBoundsConfig
    {
        public Transform leftBorder;
        public Transform rightBorder;
        public Transform downBorder;
        public Transform topBorder;
    }
}