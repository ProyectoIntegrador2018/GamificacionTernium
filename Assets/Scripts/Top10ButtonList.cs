﻿using UnityEngine.UI;
using UnityEngine;
using System.Linq;

public class Top10ButtonList : MonoBehaviour
{

    public GameObject missionPrefab;
    private string[] missions; 

    //la funcion crea una boton nuevo para la lista
    GameObject createButtonForList(string buttonText) {

        GameObject mission;

        mission = Instantiate(missionPrefab);
        mission.transform.SetParent(GameObject.Find("Mision Container").transform);
        mission.transform.localScale = new Vector2((float)0.73, (float)1.006147);
        mission.transform.GetChild(1).GetComponent<Text>().text = buttonText;

        return mission;
    }

    //la funcion se encarga de crear un boton para los totales y un boton para todas las misiones
    void instantiateMissionList() {
        GameObject mission;
        int i = 0;

        mission = createButtonForList("Puntuaciones totales");
        mission.name = "Totales";
        mission.GetComponent<Button>().onClick.AddListener(() => {
            Top10EventSystem.current.TotalClick();
        });

        foreach (string missionName in missions) {
            mission = createButtonForList(missionName);
            mission.name = i.ToString();
            //Se necesita caputrar el valor para que el onClickListener sea dinamico
            int capturedIndex = i;
            string capturedName = missionName; 
            mission.GetComponent<Button>().onClick.AddListener(() => {
                Top10EventSystem.current.MissionClick(capturedName, capturedIndex);
            });
            i++;
        }
    }

    void Start()
    {
        missions = Database.getAllMissionNames();
        instantiateMissionList();
    }
}
