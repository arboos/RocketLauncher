using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UIController : MonoBehaviour
{
   
   private VisualElement _gameplayContainer;
    
   private Label _currentDistanceText;


   private void Start()
   {
      var root = GetComponent<UIDocument>().rootVisualElement;

      _currentDistanceText = root.Q<Label>("CurrentDistance");
   }

   private void Update()
   {
      if (MissileController.Instance.launched)
      {
         _currentDistanceText.text = (GameManager.Instance.Distance / 10).ToString() + "m";
      }
   }
}
