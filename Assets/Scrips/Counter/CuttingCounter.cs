using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    public event EventHandler OnCut;
    public event EventHandler<OnProgressChangedEventArgs> OnProgressChanged;
    
    public class OnProgressChangedEventArgs : EventArgs
    {
        public float cuttingProgress;
    }

    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;

    private int cuttingProgress;

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                player.GetKitchenObject().SetKitchenObjectParent(this);
                cuttingProgress = 0;
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
            cuttingProgress++;

            CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeFromInput(GetKitchenObject().GetKitchenObjectSO());
            
            OnCut?.Invoke(this, EventArgs.Empty);
            
            OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs
            {
                cuttingProgress = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax
            });

            if (cuttingProgress >= cuttingRecipeSO.cuttingProgressMax)
            {
                KitchenObjectSO outputKitchenObjectSO = GetOutputFromInput(GetKitchenObject().GetKitchenObjectSO());

                GetKitchenObject().DestroySelf();

                KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);
            }
        }
    }

    private bool HasRecipeWithInput(KitchenObjectSO kitchenObjectSO)
    {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeFromInput(kitchenObjectSO);
        return cuttingRecipeSO != null;
    }

    private KitchenObjectSO GetOutputFromInput(KitchenObjectSO kitchenObjectSO)
    {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeFromInput(kitchenObjectSO);
        if (cuttingRecipeSO != null)
            return cuttingRecipeSO.outputKitchenObject;
        else return null;
    }

    private CuttingRecipeSO GetCuttingRecipeFromInput(KitchenObjectSO kitchenObjectSO)
    {
        foreach (var cuttingRecipeSO in cuttingRecipeSOArray)
            if (cuttingRecipeSO.inputKitchenObject == kitchenObjectSO)
            {
                return cuttingRecipeSO;
            }

        return null;
    }
}