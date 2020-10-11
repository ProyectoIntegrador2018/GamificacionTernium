using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Top10UserScores : MonoBehaviour
{

    public Text userscoreTextPrefab;
    private User[] users;
    private Userscore[][] userscores;

    private struct Userscore {
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

    void displayMissionTopScores(string missionName, int missionIndex) {
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
        userscoreText.text = missionName;
        for (int i = 0; i < 10 && i < userscores[missionIndex].Length; i++) {
            userscoreText = Instantiate(userscoreTextPrefab);
            userscoreText.transform.SetParent(GameObject.Find("TopPlayers").transform);
            userscoreText.transform.localScale = new Vector2(1, 1);
            //Offset para cada vez colocar mas abajo los jugadores con su puntuacion
            userscoreText.transform.localPosition = new Vector2(0, 345 - 70 * (i + 1));
            userscoreText.text = userscores[missionIndex][i].username + " - " + userscores[missionIndex][i].score;
        }
    }

    void Start()
    {
        Top10EventSystem.current.onMissionClick += OnMissionClick;
        users = Database.GetUsers();
        //El numero de niveles es igual al numero de misiones
        userscores = new Userscore[users[0].niveles.Length][];
    }

    private void OnMissionClick(string missionName, int missionIndex) {
        displayMissionTopScores(missionName, missionIndex);
    }

    private void OnDestroy() {
        Top10EventSystem.current.onMissionClick -= OnMissionClick;
    }
}
