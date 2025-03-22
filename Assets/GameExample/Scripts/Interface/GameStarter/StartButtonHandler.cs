using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class StartButtonHandler : GameMonoBehaviour
    {
        public event Action OnButtonClick;
        public void OnClick()
        {
            OnButtonClick?.Invoke();
            this.gameObject.SetActive(false);
        }

    }

} 