using System;
using System.Collections;
using System.Collections.Generic;
using Dacodelaac.Core;
using UnityEngine;

public class PlateIconsUI : BaseMono
{
    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private Transform plateIconTemplate;

    private void Start()
    {
        plateKitchenObject.OnIngredientAdded += PlateKitchenObjectOnOnIngredientAdded;
        plateIconTemplate.gameObject.SetActive(false);
    }

    private void PlateKitchenObjectOnOnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e)
    {
        UpdateVisual();
    }

    void UpdateVisual()
    {
        foreach (Transform child in transform)
        {
            if(child == plateIconTemplate) continue;
            Destroy(child.gameObject);
        }
        foreach (var kitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList())
        {
            Transform plateIconTemplate = Instantiate(this.plateIconTemplate, transform);
            plateIconTemplate.gameObject.SetActive(true);
            plateIconTemplate.GetComponent<PlateIconSingleUI>().SetUpKitchenObjectSO(kitchenObjectSO);
        }
    }
}
