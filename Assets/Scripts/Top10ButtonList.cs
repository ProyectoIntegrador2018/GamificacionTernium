using UnityEngine.UI;
using UnityEngine;
using System.Linq;

public class Top10ButtonList : MonoBehaviour
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
        int i = 0;

        foreach (string missionName in missions.MissionNames) {
            mission = Instantiate(missionPrefab);
            mission.name = i.ToString();
            mission.transform.SetParent(GameObject.Find("Mision Container").transform);
            mission.transform.localScale = new Vector2((float)0.73, (float)1.006147);
            mission.transform.GetChild(1).GetComponent<Text>().text = missionName;
            //Se necesita caputrar el valor para que el onClickListener sea dinamico
            int capturedIndex = i;
            string capturedName = missions.MissionNames[i]; 
            mission.GetComponent<Button>().onClick.AddListener(() => {
                Top10EventSystem.current.MissionClick(capturedName, capturedIndex);
            });
            i++;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        missions = JsonUtility.FromJson<Missions>(MissionNamesTextFile.text);
        instantiateMissionList();

    }
}
