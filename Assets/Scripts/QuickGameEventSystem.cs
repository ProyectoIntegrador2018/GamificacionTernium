using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickGameEventSystem : MonoBehaviour
{

    public static QuickGameEventSystem current;

    private void Awake() {
        current = this;
    }

    public event Action onTimeframeReached;
    public void OnTimeframeReached() {
        if (onTimeframeReached != null) {
            onTimeframeReached();
        }
    }

}
