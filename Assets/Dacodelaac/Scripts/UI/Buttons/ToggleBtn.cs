using System;
using Dacodelaac.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SurvivorRoguelike.Common
{
    public class ToggleBtn : BaseMono
    {
        [SerializeField] private Button button;
        [SerializeField] private Image bgActive;
        [SerializeField] private Image bgDisable;
        [SerializeField] private TMP_Text txtActive;
        [SerializeField] private TMP_Text txtDisable;

        public bool isActive = false;
        public string idButton;
        private Action<string> callback;

        public void Start()
        {
            button.onClick.AddListener(OnButtonClick);
        }
        
        private void OnButtonClick()
        {
            callback?.Invoke(idButton);
        }

        public void InitButton(string id, Action<string> cb)
        {
            this.idButton = id;
            callback = cb;
        }

        public void OnChangeStatus(bool isActive)
        {
            this.isActive = isActive;
            
            if (txtActive != null) txtActive.gameObject.SetActive(isActive);
            if (txtDisable != null) txtDisable.gameObject.SetActive(!isActive);
            if (bgActive != null) bgActive.gameObject.SetActive(isActive);
            if (bgDisable != null) bgDisable.gameObject.SetActive(!isActive);
        }

        private void OnDestroy()
        {
            callback = null;
        }
    }
}