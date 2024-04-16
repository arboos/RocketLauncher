using System;
using System.Collections;
using System.Collections.Generic;
using InstantGamesBridge;
using InstantGamesBridge.Modules.Storage;
using Unity.VisualScripting;
using UnityEngine;

public class RecordManager : MonoBehaviour
{
    public static RecordManager Instance { get; private set; }

    public RecordLine BestRecordLine;
    public RecordLine PreviousLaunchLine;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
}
