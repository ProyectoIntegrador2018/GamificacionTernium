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
    public event Action onEnableCheck;
    public void OnTimeframeReached() {
        if (onTimeframeReached != null) {
            onTimeframeReached();
        }
    }
    public void OnEnableCheck() {
        if (onEnableCheck != null) {
            onEnableCheck();
        }
    }

}
