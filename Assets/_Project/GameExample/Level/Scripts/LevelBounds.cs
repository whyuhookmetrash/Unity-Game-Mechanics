using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class LevelBounds
    {
        private readonly WorldTransformProvider worldTransformProvider;

        public LevelBounds(WorldTransformProvider worldTransformProvider)
        {
            this.worldTransformProvider = worldTransformProvider;
        }

        public bool InBounds(Vector3 position)
        {
            float positionX = position.x;
            float positionY = position.y;
            return positionX > this.worldTransformProvider.leftBorder.position.x
                   && positionX < this.worldTransformProvider.rightBorder.position.x
                   && positionY > this.worldTransformProvider.downBorder.position.y
                   && positionY < this.worldTransformProvider.topBorder.position.y;
        }
    }
}