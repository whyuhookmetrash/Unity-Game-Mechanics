using System;
using TMPro;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class GameStarterView : GameMonoBehaviour
    {
        [SerializeField]
        private TMP_Text textMeshPro;
        
        public void Active(bool active)
        {
            this.gameObject.SetActive(active);
        } 
        public void SetText(String text)
        {
            textMeshPro.text = text;
        }

    }

}