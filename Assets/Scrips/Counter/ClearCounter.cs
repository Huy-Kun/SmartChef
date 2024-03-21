using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private Transform topCounter;
    public void Interact()
    {
        Transform kitchenObject = Instantiate(this.kitchenObjectSO.prefab, topCounter);
        kitchenObject.localPosition = Vector3.zero;
        
        Debug.Log(kitchenObject.transform.GetComponent<KitchenObject>().GetKitchenObjectSO().objectName);
    }
}
