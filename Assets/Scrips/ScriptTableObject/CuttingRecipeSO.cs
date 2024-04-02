using System.Collections;
using System.Collections.Generic;
using Dacodelaac.Core;
using UnityEngine;

[CreateAssetMenu()]
public class CuttingRecipeSO : BaseSO
{
    public KitchenObjectSO inputKitchenObject;
    public KitchenObjectSO outputKitchenObject;
    public int cuttingProgressMax;
}
