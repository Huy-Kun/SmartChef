using Dacodelaac.UI.Popups;
using Dacodelaac.Utils;
using SurvivorRoguelike.UI;

public class GamePopupController : BasePopupController
{
    public override void Initialize()
    {
        base.Initialize();
        Show<GamePlayPopup>(false);
    }

    public void ShowGameCompletePopup()
    {
        Show<GameCompletePopup>(true, ShowAction.PauseCurrent);
    }
    
}