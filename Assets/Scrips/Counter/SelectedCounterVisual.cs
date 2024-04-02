using System;
using System.Collections;
using System.Collections.Generic;
using Dacodelaac.Core;
using UnityEngine;

public class SelectedCounterVisual : BaseMono
{
   [SerializeField] private BaseCounter baseCounter;
   [SerializeField] private GameObject[] visualGameObjectArray;

   private void Start()
   {
      Player.Instance.OnSelectedCounterChanged += PlayerOnSelectedCounterChanged;
   }

   private void PlayerOnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e)
   {
      if (e.selectedCounter == baseCounter)
      {
         Show();
      }
      else
      {
         Hide();
      }
   }

   private void Show()
   {
      foreach (var visualGameObject in visualGameObjectArray)
      {
         visualGameObject.SetActive(true);
      }
   }

   private void Hide()
   {
      foreach (var visualGameObject in visualGameObjectArray)
      {
         visualGameObject.SetActive(false);
      }
   }
}
