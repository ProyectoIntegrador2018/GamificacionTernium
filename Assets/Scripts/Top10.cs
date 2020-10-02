using UnityEngine.UI;
using UnityEngine;
using System.Linq;

public class Top10 : MonoBehaviour
{

    public TextAsset MissionNamesTextFile;
    public GameObject missionPrefab;
    public Text userscoreTextPrefab;
    private Missions missions;
    private User[] users;
    private Userscore[][] userscores;

    private class Missions {

        public string[] MissionNames;

        Missions() {
            MissionNames = new string[] { };
        }
    }

    private struct Userscore{
        public string username;
        public string score;

        public Userscore(string username, string score) {
            this.username = username;
            this.score = score;
        }

    }

    Userscore[] loadMissionScores(int missionIndex) {
        Userscore[] aux = new Userscore[users.Length];
        int i = 0;
        foreach (User user in users) {
            aux[i] = new Userscore(user.username, user.niveles[missionIndex].ToString());
            i++;
        }
        aux = aux.OrderByDescending(x => x.score).ToArray();
        return aux;
    }

    void displayTopScores(int missionIndex) {
        if (userscores[missionIndex] == null) {
            userscores[missionIndex] = loadMissionScores(missionIndex);
        }
        foreach (Transform topscore in GameObject.Find("TopPlayers").transform) {
            Destroy(topscore.gameObject);
        }

        Text userscoreText = Instantiate(userscoreTextPrefab);
        userscoreText.transform.SetParent(GameObject.Find("TopPlayers").transform);
        userscoreText.transform.localScale = new Vector2(1, 1);
        userscoreText.transform.localPosition = new Vector2(0, 345);
        userscoreText.fontStyle = FontStyle.BoldAndItalic;
        userscoreText.text = missions.MissionNames[missionIndex];
        for (int i = 0; i < 10 && i < userscores[missionIndex].Length; i++) {
            userscoreText = Instantiate(userscoreTextPrefab);
            userscoreText.transform.SetParent(GameObject.Find("TopPlayers").transform);
            userscoreText.transform.localScale = new Vector2(1, 1);
            //Offset para cada vez colocar mas abajo los jugadores con su puntuacion
            userscoreText.transform.localPosition = new Vector2(0, 345 - 70 * (i + 1));
            userscoreText.text = userscores[missionIndex][i].username + " - " + userscores[missionIndex][i].score;
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
            int capturedValue = i;
            mission.GetComponent<Button>().onClick.AddListener(() => {
                displayTopScores(capturedValue);
            });
            i++;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        missions = JsonUtility.FromJson<Missions>(MissionNamesTextFile.text);
        users = Database.GetUsers();
        userscores = new Userscore[missions.MissionNames.Length][];
        instantiateMissionList();

    }
}
