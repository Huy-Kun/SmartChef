using System;
using System.Collections;
using System.Collections.Generic;
using Dacodelaac.Core;
using UnityEngine;

public class PlateCompleteVisual : BaseMono
{
    [Serializable]
    public struct KitchenObjectSO_GameObject
    {
        public KitchenObjectSO kitchenObjectSO;
        public GameObject gameObject;
    }

    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private List<KitchenObjectSO_GameObject> kitchenObjectSOGameObjectList;

    private void Start()
    {
        plateKitchenObject.OnIngredientAdded += PlateKitchenObjectOnOnIngredientAdded;
        foreach (var kitchenObjectGameObject in kitchenObjectSOGameObjectList)
            kitchenObjectGameObject.gameObject.SetActive(false);
    }

    private void PlateKitchenObjectOnOnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e)
    {
        foreach (var kitchenObjectGameObject in kitchenObjectSOGameObjectList)
            if (kitchenObjectGameObject.kitchenObjectSO == e.kitchenObjectSO)
            {
                kitchenObjectGameObject.gameObject.SetActive(true);
            }
    }
}