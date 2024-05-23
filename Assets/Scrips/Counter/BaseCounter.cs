using System.Collections;
using System.Collections.Generic;
using Dacodelaac.Core;
using Dacodelaac.Events;
using UnityEngine;

public class BaseCounter : BaseMono, IKitchenObjectParent
{
    [SerializeField] private Transform counterTopPoint;
    [SerializeField] protected PlayAudioEvent playAudioEvent;
    
    private KitchenObject kitchenObject;

    public virtual void Interact(Player player)
    {
    }

    public virtual void InteractAlternate(Player player)
    {
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
    }

    public KitchenObject GetKitchenObject()
    {
        return this.kitchenObject;
    }

    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return counterTopPoint;
    }
}