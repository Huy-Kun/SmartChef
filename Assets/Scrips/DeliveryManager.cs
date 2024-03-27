using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DeliveryManager : MonoBehaviour
{
    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompleted;
    public static DeliveryManager Instance { get; private set; }

    [SerializeField] private RecipeSOList recipeSOList;

    private List<RecipeSO> waitingRecipeSOList;
    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 4f;
    private int waitingRecipeMax = 4;

    private void Awake()
    {
        Instance = this;
        waitingRecipeSOList = new List<RecipeSO>();
    }

    private void Update()
    {
        spawnRecipeTimer += Time.deltaTime;
        if (spawnRecipeTimer >= spawnRecipeTimerMax)
        {
            spawnRecipeTimer = 0f;
            if (waitingRecipeSOList.Count < waitingRecipeMax)
            {
                RecipeSO waitingRecipeSO =
                    recipeSOList.recipeSOList[Random.Range(0, recipeSOList.recipeSOList.Count - 1)];
                waitingRecipeSOList.Add(waitingRecipeSO);
                OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public void DeliveryRecipe(PlateKitchenObject plateKitchenObject)
    {
        var plateKitchenObjectSOList = plateKitchenObject.GetKitchenObjectSOList();
        for (int i = 0; i < waitingRecipeSOList.Count; i++)
        {
            var recipeKitchenObjectSOList = waitingRecipeSOList[i].KitchenObjectSOList;

            if (recipeKitchenObjectSOList.Count == plateKitchenObjectSOList.Count)
            {
                bool recipeMatchPlateContent = true;
                foreach (var recipeKitchenObjectSO in recipeKitchenObjectSOList)
                {
                    bool ingredientFound = false;
                    foreach (var plateKitchenObjectSO in plateKitchenObjectSOList)
                        if (recipeKitchenObjectSO == plateKitchenObjectSO)
                        {
                            ingredientFound = true;
                            break;
                        }

                    if (ingredientFound == false)
                    {
                        recipeMatchPlateContent = false;
                        break;
                    }
                }

                if (recipeMatchPlateContent == true)
                {
                    Debug.Log("Delivery success!");
                    waitingRecipeSOList.RemoveAt(i);
                    OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                    return;
                }
            }
        }

        Debug.Log("Delivery failed");
    }

    public List<RecipeSO> GetWaitingRecipeList()
    {
        return waitingRecipeSOList;
    }
}