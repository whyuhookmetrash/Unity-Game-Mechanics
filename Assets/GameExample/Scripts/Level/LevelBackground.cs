using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class LevelBackground : GameMonoBehaviour,
        IGameFixedTickable
    {
        private float startPositionY;

        private float endPositionY;

        private float movingSpeedY;

        private float positionX;

        private float positionZ;

        private Transform myTransform;

        [SerializeField]
        private BackgroundParams backgroundParams;

        private void Awake()
        {
            this.startPositionY = this.backgroundParams.startPositionY;
            this.endPositionY = this.backgroundParams.endPositionY;
            this.movingSpeedY = this.backgroundParams.movingSpeedY;
            this.myTransform = this.transform;
            var position = this.myTransform.position;
            this.positionX = position.x;
            this.positionZ = position.z;
        }

        void IGameFixedTickable.FixedTick(float deltaTime)
        {
            if (this.myTransform.position.y <= this.endPositionY)
            {
                this.myTransform.position = new Vector3(
                    this.positionX,
                    this.startPositionY,
                    this.positionZ
                );
            }

            this.myTransform.position -= new Vector3(
                this.positionX,
                this.movingSpeedY * Time.fixedDeltaTime,
                this.positionZ
            );
        }

        [Serializable]
        public sealed class BackgroundParams
        {
            public float startPositionY;

            public float endPositionY;

            public float movingSpeedY;
        }
    }
}