using System;
using System.Collections;
using System.Collections.Generic;
using Dacodelaac.Core;
using UnityEngine;

public class DeliveryManagerUI : BaseMono
{
    [SerializeField] private Transform containter;
    [SerializeField] private Transform recipeTemplate;

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeCompleted += InstanceOnOnRecipeCompleted;
        DeliveryManager.Instance.OnRecipeSpawned += InstanceOnOnRecipeSpawned;
        recipeTemplate.gameObject.SetActive(false);
    }

    private void InstanceOnOnRecipeSpawned(object sender, EventArgs e)
    {
        UpdateVisual();
    }

    private void InstanceOnOnRecipeCompleted(object sender, EventArgs e)
    {
        UpdateVisual();
    }

    void UpdateVisual()
    {
        foreach (Transform child in containter)
        {
            if(child == recipeTemplate) continue;
            Destroy(child.gameObject);
        }

        foreach (var recipeSO in DeliveryManager.Instance.GetWaitingRecipeList())
        {
            Transform recipeTemplate = Instantiate(this.recipeTemplate, containter);
            recipeTemplate.gameObject.SetActive(true);
            recipeTemplate.gameObject.GetComponent<DeliveryManagerSingleUI>().SetUpRecipeSO(recipeSO);
        }
    }

}
