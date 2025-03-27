using System;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public sealed class LevelBackground :
        IGameFixedTickable,
        IInitializable
    {
        private readonly float startPositionY;
        private readonly float endPositionY;
        private readonly float movingSpeedY;
        private readonly Transform background;

        private float positionX;
        private float positionZ;

        public LevelBackground(BackgroundConfig config)
        {
            this.startPositionY = config.startPositionY;
            this.endPositionY = config.endPositionY;
            this.movingSpeedY = config.movingSpeedY;
            this.background = config.background;
        }

        void IInitializable.Initialize()
        {
            var position = this.background.position;
            this.positionX = position.x;
            this.positionZ = position.z;
        }

        void IGameFixedTickable.FixedTick(float deltaTime)
        {
            if (this.background.position.y <= this.endPositionY)
            {
                this.background.position = new Vector3(
                    this.positionX,
                    this.startPositionY,
                    this.positionZ
                );
            }

            this.background.position -= new Vector3(
                this.positionX,
                this.movingSpeedY * Time.fixedDeltaTime,
                this.positionZ
            );
        }

        [Serializable]
        public sealed class BackgroundConfig
        {
            public float startPositionY;

            public float endPositionY;

            public float movingSpeedY;

            public Transform background;
        }
    }
}