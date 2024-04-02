using System;
using System.Collections;
using System.Collections.Generic;
using Dacodelaac.Core;
using UnityEngine;
using Random = UnityEngine.Random;

public class DeliveryManager : BaseMono
{
    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompleted;
    public static DeliveryManager Instance { get; private set; }

    [SerializeField] private RecipeSOList recipeSOList;

    private List<RecipeSO> waitingRecipeSOList;
    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 4f;
    private int waitingRecipeMax = 4;
    private int ordersDelivered;
    private int ordersFailed;

    private void Awake()
    {
        Instance = this;
        waitingRecipeSOList = new List<RecipeSO>();
        ordersDelivered = 0;
        ordersFailed = 0;
    }

    public override void Tick()
    {
        base.Tick();
        spawnRecipeTimer += Time.deltaTime;
        if (spawnRecipeTimer >= spawnRecipeTimerMax)
        {
            spawnRecipeTimer = 0f;
            if (waitingRecipeSOList.Count < waitingRecipeMax && !KitchenGameManager.Instance.IsGameOver())
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
                    // Debug.Log("Delivery success!");
                    ordersDelivered++;
                    waitingRecipeSOList.RemoveAt(i);
                    OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                    return;
                }
            }
        }

        // Debug.Log("Delivery failed");
        ordersFailed++;
    }

    public List<RecipeSO> GetWaitingRecipeList()
    {
        return waitingRecipeSOList;
    }

    public int GetOrdersDeliveredPoint()
    {
        return ordersDelivered * 10;
    }

    public int GetOrdersFailedPoint()
    {
        return ordersFailed * 10;
    }

    public int GetTotalPoint()
    {
        return GetOrdersDeliveredPoint() - GetOrdersFailedPoint();
    }
}