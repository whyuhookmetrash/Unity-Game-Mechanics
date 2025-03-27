using System;
using TMPro;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    [RequireComponent(typeof(TMP_Text))]
    public sealed class GameStarterView : MonoBehaviour,
        IInitializable
    {
        private TMP_Text textMeshPro;

        void IInitializable.Initialize()
        {
            this.textMeshPro = this.gameObject.GetComponent<TMP_Text>();
        }

        public void Active(bool active)
        {
            this.gameObject.SetActive(active);
        } 

        public void SetText(String text)
        {
            this.textMeshPro.text = text;
        }
    }
}