using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfoEventSystem : MonoBehaviour
{

    public static PlayerInfoEventSystem current;

    //clase singleton para manejar las llamadas de eventos de los scripts de la barra de experiencia y el nivel del usuario
    private void Awake() {
        current = this;
    }

    public event Action onExpBarFill;
    public event Action onFinishExpGain;
    public event Action onStartLevelUpAnimation;
    public event Action onFinishLevelUpAnimation;
    public void ExpBarFill() {
        if(onExpBarFill != null) {
            onExpBarFill();
        }
    }

    public void FinishExpGain() {
        if(onFinishExpGain != null) {
            onFinishExpGain();
        }
    }

    public void StartLevelUpAnimation() {
        if (onStartLevelUpAnimation != null) {
            onStartLevelUpAnimation();
        }
    }

    public void FinishLevelUpAnimation() {
        if (onFinishLevelUpAnimation != null) {
            onFinishLevelUpAnimation();
        }
    }

}
