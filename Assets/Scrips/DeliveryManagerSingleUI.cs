using System;
using System.Collections;
using System.Collections.Generic;
using Dacodelaac.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryManagerSingleUI : BaseMono
{
    [SerializeField] private TextMeshProUGUI recipeName;
    [SerializeField] private Transform ingredientContainer;
    [SerializeField] private Transform ingredientIcon;

    private void Start()
    {
        ingredientIcon.gameObject.SetActive(false);
    }

    public void SetUpRecipeSO(RecipeSO recipeSO)
    {
        foreach (Transform child in ingredientContainer)
        {
            if(child == ingredientIcon) continue;
            Destroy(child.gameObject);
        }

        recipeName.text = recipeSO.recipeName;

        foreach (var kitchenObjectSO in recipeSO.KitchenObjectSOList)
        {
            Transform ingredientIcon = Instantiate(this.ingredientIcon, ingredientContainer);
            ingredientIcon.gameObject.SetActive(true);
            ingredientIcon.gameObject.GetComponent<Image>().sprite = kitchenObjectSO.sprite;
        }
    }
}
