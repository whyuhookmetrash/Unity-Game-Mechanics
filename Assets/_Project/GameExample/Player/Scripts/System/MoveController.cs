using System;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public sealed class MoveController :
        IInitializable,
        IDisposable
    {
        private readonly LazyInject<Player> player;
        private readonly IInputService inputService;

        public MoveController(LazyInject<Player> player, IInputService inputService)
        {
            this.player = player;
            this.inputService = inputService;
        }

        void IInitializable.Initialize()
        {
            this.inputService.OnMoveInput += this.player.Value.MoveComponent.ChangeDirection;
        }

        void IDisposable.Dispose()
        {
            this.inputService.OnMoveInput -= this.player.Value.MoveComponent.ChangeDirection;
        }
    }
}