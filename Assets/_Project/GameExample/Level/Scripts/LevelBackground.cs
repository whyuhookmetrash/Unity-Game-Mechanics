using System;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public sealed class LevelBackground :
        IGameFixedTickable,
        IInitializable
    {
        private float positionX;
        private float positionZ;

        private readonly WorldTransformProvider worldTransformProvider;
        private readonly BackgroundData backgroundData;

        public LevelBackground(
            WorldTransformProvider worldTransformProvider,
            BackgroundData backgroundData
            )
        {
            this.worldTransformProvider = worldTransformProvider;
            this.backgroundData = backgroundData;
        }

        void IInitializable.Initialize()
        {
            var position = this.worldTransformProvider.background.position;
            this.positionX = position.x;
            this.positionZ = position.z;
        }

        void IGameFixedTickable.FixedTick(float deltaTime)
        {
            if (this.worldTransformProvider.background.position.y <= this.backgroundData.endPositionY)
            {
                this.worldTransformProvider.background.position = new Vector3(
                    this.positionX,
                    this.backgroundData.startPositionY,
                    this.positionZ
                );
            }

            this.worldTransformProvider.background.position -= new Vector3(
                this.positionX,
                this.backgroundData.movingSpeedY * deltaTime,
                this.positionZ
            );
        }
    }
}