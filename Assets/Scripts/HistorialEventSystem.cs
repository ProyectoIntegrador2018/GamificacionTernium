using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HistorialEventSystem : MonoBehaviour
{
    public static HistorialEventSystem current;

    //clase singleton para manejar las llamadas de eventos de los scripts del historial de misiones
    private void Awake() {
        current = this;
    }

    public event Action<string, int, int> onMissionClick;
    public event Action<int> onReplayClick;
    public void MissionClick(string description, int score, int escenario) {
        if(onMissionClick != null) {
            onMissionClick(description, score, escenario);
        }
    }
    public void ReplayClick(int index) {
        if(onReplayClick != null) {
            onReplayClick(index);
        }
    }
}
