using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{

    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;
    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
            else
            {
                
            }
        }
        else
        {
            if (!player.HasKitchenObject())
            {
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }

    public override void InteractAlternate(Player player)
    {
        if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO()))
        {
            KitchenObjectSO outputKitchenObjectSO = GetOutputFromInput(GetKitchenObject().GetKitchenObjectSO());
            
            GetKitchenObject().DestroySelf();

            KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);
        }
    }

    private bool HasRecipeWithInput(KitchenObjectSO kitchenObjectSO)
    {
        foreach (var cuttingRecipeSO in cuttingRecipeSOArray)
            if(cuttingRecipeSO.inputKitchenObject == kitchenObjectSO)
            {
                return true;
            }

        return false;
    }

    private KitchenObjectSO GetOutputFromInput(KitchenObjectSO kitchenObjectSO)
    {
        foreach (var cuttingRecipeSO in cuttingRecipeSOArray)
            if(cuttingRecipeSO.inputKitchenObject == kitchenObjectSO)
            {
                return cuttingRecipeSO.outputKitchenObject;
            }

        return null;
    }
}
