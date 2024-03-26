using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlateCounterVisual : MonoBehaviour
{
    [SerializeField] private PlateCounter plateCounter;
    [SerializeField] private Transform topPointPlateCounter;
    [SerializeField] private Transform plateVisualPrefab;

    private List<GameObject> plateList;
    private void Start()
    {
        plateList = new List<GameObject>();
        plateCounter.OnRemovePlate += PlateCounterOnOnRemovePlate;
        plateCounter.OnSpawnPlate += PlateCounterOnOnSpawnPlate;
    }

    private void PlateCounterOnOnSpawnPlate(object sender, EventArgs e)
    {
        Transform plateVisualTransform = Instantiate(plateVisualPrefab, topPointPlateCounter);

        float plateOffsetY = 0.1f;

        plateVisualTransform.localPosition = new Vector3(0f, plateOffsetY * plateList.Count, 0f);
        
        plateList.Add(plateVisualTransform.gameObject);
    }

    private void PlateCounterOnOnRemovePlate(object sender, EventArgs e)
    {
        GameObject plateVisualGameObject = plateList[plateList.Count - 1];
        plateList.Remove(plateVisualGameObject);
        Destroy(plateVisualGameObject);
        
    }

}
