using System.Collections;
using System.Collections.Generic;
using Dacodelaac.Core;
using UnityEngine;

[CreateAssetMenu()]
public class BurningRecipeSO : BaseSO
{
    public KitchenObjectSO inputKitchenObject;
    public KitchenObjectSO outputKitchenObject;
    public int burningProgressMax;
}
