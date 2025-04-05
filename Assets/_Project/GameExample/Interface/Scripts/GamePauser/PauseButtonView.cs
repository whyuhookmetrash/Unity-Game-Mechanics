using System;
using TMPro;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    [RequireComponent(typeof(TMP_Text))]
    public sealed class PauseButtonView : MonoBehaviour,
        IInitializable
    {
        private TMP_Text buttonText;

        void IInitializable.Initialize()
        {
            this.buttonText = this.gameObject.GetComponent<TMP_Text>();
        }

        public void SetText(String text)
        {
            this.buttonText.text = text;
        }
    }
}