using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Top10EventSystem : MonoBehaviour
{

    public static Top10EventSystem current;

    //clase singleton para manejar las llamadas de eventos de los scripts de top 10
    private void Awake() {
        current = this;
    }

    public event Action<string, int> onMissionClick;
    public event Action onTotalClick;
    public void MissionClick(string missionName, int missionIndex) {
        if (onMissionClick != null) {
            onMissionClick(missionName, missionIndex);
        }
    }

    public void TotalClick() {
        if (onTotalClick != null) {
            onTotalClick();
        }
    }

}
