using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HistorialEventSystem : MonoBehaviour
{
    public static HistorialEventSystem current;

    private void Awake() {
        current = this;
    }

    public event Action<string, int, int> onMissionClick;
    public void MissionClick(string description, int score, int escenario) {
        if(onMissionClick != null) {
            onMissionClick(description, score, escenario);
        }
    }

}
