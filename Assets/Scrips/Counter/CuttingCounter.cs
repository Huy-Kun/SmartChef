using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Dacodelaac.Events;
using UnityEngine;

public class CuttingCounter : BaseCounter, IHasProgress
{
    public event EventHandler OnCut;
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
    

    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;
    [SerializeField] private PlayAudioEvent playAudioEvent;
    [SerializeField] private AudioClip[] cutAudios;

    private int cuttingProgress;

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    
                    cuttingProgress = 0;
                    
                    CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeFromInput(GetKitchenObject().GetKitchenObjectSO());
            
                    OnProgressChanged?.Invoke(this, new  IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax
                    });
                }
            }
            else
            {
            }
        }
        else
        {
            if (!player.HasKitchenObject())
            {
                if (cuttingProgress == 0f)
                {
                    GetKitchenObject().SetKitchenObjectParent(player);
                }
            }
            else
            {
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();
                        
                        OnProgressChanged?.Invoke(this, new  IHasProgress.OnProgressChangedEventArgs
                        {
                            progressNormalized = 0f
                        });
                    }
                }
            }
        }
    }

    public override void InteractAlternate(Player player)
    {
        if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO()))
        {
            cuttingProgress++;
            
            playAudioEvent.RaiseRandom(cutAudios);

            CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeFromInput(GetKitchenObject().GetKitchenObjectSO());
            
            OnCut?.Invoke(this, EventArgs.Empty);
            
            OnProgressChanged?.Invoke(this, new  IHasProgress.OnProgressChangedEventArgs
            {
                progressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax
            });

            if (cuttingProgress >= cuttingRecipeSO.cuttingProgressMax)
            {
                cuttingProgress = 0;
                
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