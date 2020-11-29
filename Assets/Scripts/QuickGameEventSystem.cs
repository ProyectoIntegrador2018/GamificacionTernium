using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickGameEventSystem : MonoBehaviour
{

    public static QuickGameEventSystem current;

    //clase singleton para manejar las llamadas de eventos de los scripts de las vidas y el timer del juego rapido
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
