using System;
using System.Collections;
using System.Collections.Generic;
using Dacodelaac.Core;
using UnityEngine;
using UnityEngine.Serialization;

public class DeliveryManagerUI : BaseMono
{
    [SerializeField] private Transform containter;
    [SerializeField] private Transform recipePrefab;

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSpawned += InstanceOnOnRecipeSpawned;
        DeliveryManager.Instance.OnRecipeCompleted += InstanceOnOnRecipeCompleted;
    }

    private void InstanceOnOnRecipeCompleted(object sender, DeliveryManager.OnRecipeCompletedEventArgs e)
    {
        foreach (Transform child in containter)
        {
            if (child.GetComponent<DeliveryManagerSingleUI>().RecipeSO == e.recipeSO)
            {
                Destroy(child.gameObject);
                return;
            }
        }
    }

    private void InstanceOnOnRecipeSpawned(object sender, DeliveryManager.OnRecipeSpawnedEventArgs e)
    {
        Transform recipeTemplate = Instantiate(this.recipePrefab, containter);
        recipeTemplate.gameObject.GetComponent<DeliveryManagerSingleUI>().SetUpRecipeSO(e.recipeSO);
    }
    

}
