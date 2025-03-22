using System;
using TMPro;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class PauseButtonView : GameMonoBehaviour
    {

        [SerializeField]
        private TMP_Text buttonText;

        public void SetText(String text)
        {
            this.buttonText.text = text;
        }

    }

}