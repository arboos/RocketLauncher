using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecordLine : MonoBehaviour
{
    [SerializeField] private Transform DynamicPart;
    public TextMeshProUGUI DistanceText;

    private void Update()
    {
        if(MissileController.Instance.launched) DynamicPart.localPosition = new Vector2(0, MissileController.Instance.transform.position.y);
    }
}
