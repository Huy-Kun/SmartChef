using System.Collections;
using System.Collections.Generic;
using Dacodelaac.UI.Popups;
using UnityEngine;

namespace SurvivorRoguelike.UI
{
    public class GamePlayPopup : BasePopup
    {
        public override void Initialize()
        {
            base.Initialize();
        }

        public void OnPauseClicked()
        {
            Controller.Show<GamePausePopup>(true, BasePopupController.ShowAction.PauseCurrent);
        }
    }
}
