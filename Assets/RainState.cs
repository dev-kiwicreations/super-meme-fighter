using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainState : MonoBehaviour
{
    private void Awake()
    {
       RainManager.Instance.ActivateRain();
    }
    private void OnDestroy()
    {
        RainManager.Instance.DeactivateRain();
    }
}
