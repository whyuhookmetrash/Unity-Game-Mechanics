using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class RigidBody2DController : GameMonoBehaviour,
        IGamePauseListener,
        IGameResumeListener,
        IGameFinishListener,
        IGameStartListener
    {
        private Rigidbody2D rigidBody;

        private void Awake()
        {
            this.rigidBody = this.gameObject.GetComponent<Rigidbody2D>();
            if (GameCycle.Instance.MainState != GameState.PLAY)
            {
                this.rigidBody.simulated = false;
            } 
        }

        void IGameFinishListener.OnGameFinish()
        {
            this.rigidBody.simulated = false;
        }

        void IGamePauseListener.OnGamePause()
        {
            this.rigidBody.simulated = false;
        }

        void IGameResumeListener.OnGameResume()
        {
            this.rigidBody.simulated = true;
        }

        void IGameStartListener.OnGameStart()
        {
            this.rigidBody.simulated = true;
        }
    }
}