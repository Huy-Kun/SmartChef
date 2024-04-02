using System;
using System.Collections;
using System.Collections.Generic;
using Dacodelaac.Core;
using UnityEngine;
using UnityEngine.Serialization;

public class PlateIconsUI : BaseMono
{
    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private Transform plateIconPrefab;

    private void Start()
    {
        plateKitchenObject.OnIngredientAdded += PlateKitchenObjectOnOnIngredientAdded;
    }

    private void PlateKitchenObjectOnOnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e)
    {
        UpdateVisual();
    }

    void UpdateVisual()
    {
        foreach (Transform child in transform)
            Destroy(child.gameObject);
        foreach (var kitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList())
        {
            Transform plateIconTemplate = Instantiate(this.plateIconPrefab, transform);
            plateIconTemplate.GetComponent<PlateIconSingleUI>().SetUpKitchenObjectSO(kitchenObjectSO);
        }
    }
}
