using Packages.Rider.Editor.UnitTesting;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Top10 : MonoBehaviour
{

    public TextAsset MissionNamesTextFile;
    public GameObject missionPrefab;
    private Missions missions;

    private class Missions {

        public string[] MissionNames;

        Missions() {
            MissionNames = new string[] { };
        }
    }

    void instantiateMissionList() {
        GameObject mission;

        foreach (string missionName in missions.MissionNames) {
            mission = Instantiate(missionPrefab);
            mission.transform.SetParent(GameObject.Find("Mision Container").transform);
            mission.transform.localScale = new Vector2((float)0.73, (float)1.006147);
            mission.transform.GetChild(1).GetComponent<Text>().text = missionName;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        missions = JsonUtility.FromJson<Missions>(MissionNamesTextFile.text);
        instantiateMissionList();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
