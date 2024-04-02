using Dacodelaac.Core;
using UnityEngine;
using UnityEngine.UI;

public class PlateIconSingleUI : BaseMono
{
    [SerializeField] private Image image;
    public void SetUpKitchenObjectSO(KitchenObjectSO kitchenObjectSO)
    {
        image.sprite = kitchenObjectSO.sprite;
    }
}
