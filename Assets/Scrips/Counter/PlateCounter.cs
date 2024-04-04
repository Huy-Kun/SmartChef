using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCounter : BaseCounter
{

    public event EventHandler OnSpawnPlate;
    public event EventHandler OnRemovePlate;
    
    [SerializeField] private KitchenObjectSO plateKitchenObjectSO;

    private float spawnPlateTimer;
    private float spawnPlateTimerMax = 4f;
    private int plateSpawnedAmount;
    private int plateSpawnedAmountMax = 4;

    private void Start()
    {
        plateSpawnedAmount = 0;
    }


    public override void Tick()
    {
        base.Tick();
        spawnPlateTimer = spawnPlateTimer + Time.deltaTime;
        if (spawnPlateTimer >= spawnPlateTimerMax)
        {
            spawnPlateTimer = 0f;
            if (plateSpawnedAmount < plateSpawnedAmountMax)
            {
                plateSpawnedAmount++;
                OnSpawnPlate?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            if (!player.HasKitchenObject())
            {
                if (plateSpawnedAmount > 0)
                {
                    KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, player);
                    plateSpawnedAmount--;
                    OnRemovePlate?.Invoke(this, EventArgs.Empty);
                }
            }
        }
    }
}
