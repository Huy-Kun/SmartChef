using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using Dacodelaac.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryManagerSingleUI : BaseMono
{
    [SerializeField] private TextMeshProUGUI recipeName;
    [SerializeField] private Transform ingredientContainer;
    [SerializeField] private Transform ingredientIcon;
    [SerializeField] private Image timer;

    public RecipeSO RecipeSO => _recipeSO;
    
    private RecipeSO _recipeSO;
    private float _recipeExistTimer;
    private void Start()
    {
        ingredientIcon.gameObject.SetActive(false);
    }

    public override void Tick()
    {
        base.Tick();
        if (_recipeSO != null)
        {
            _recipeExistTimer -= Time.deltaTime;
            if (_recipeExistTimer < 0f)
                DestroySelf();
            timer.fillAmount = _recipeExistTimer / _recipeSO.existTime;
        }
    }

    public void SetUpRecipeSO(RecipeSO recipeSO)
    {
        _recipeSO = recipeSO;
        _recipeExistTimer = recipeSO.existTime;
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
    
    public void DestroySelf()
    {
        DeliveryManager.Instance.DiscardRecipeFromWaitingList(_recipeSO);
        Destroy(gameObject);
    }
}
