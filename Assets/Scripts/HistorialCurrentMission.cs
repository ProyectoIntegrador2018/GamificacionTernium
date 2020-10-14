using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HistorialCurrentMission : MonoBehaviour
{

    public GameObject MisionDescription;

    public GameObject MisionScore;

    public Sprite[] StarSprites;
    public Image Stars1UI;

    public Button btnReplay;

    void Start()
    {
        HistorialEventSystem.current.onMissionClick += OnMissionClick;
    }
    private void OnDestroy() {
        HistorialEventSystem.current.onMissionClick -= OnMissionClick;
    }

    private void OnMissionClick(string description, int score, int escenario) {
        MisionDescription.transform.GetComponent<Text>().text = description;
        MisionScore.transform.GetComponent<Text>().text = score.ToString();
        Stars1UI.sprite = StarSprites[StarNumber(score, escenario)];
        //print(description + " " + score);
        if (escenario != -1) {
            SetReplayBtn(escenario);
        }
    }

    public void SetReplayBtn(int index) {
        btnReplay.gameObject.SetActive(true);
        btnReplay.onClick.AddListener(() => {
            HistorialEventSystem.current.ReplayClick(index);
        });
    }

    public int StarNumber(int points, int escenario) {

        int n = 0;
        int Maximo;
        double newPoints = 0;

        switch (escenario) {
            case 0: Maximo = 1; break;
            //Los Escenarios
            case 1: Maximo = 800; break;
            case 2: Maximo = 900; break;
            case 3: Maximo = 500; break;
            case 4: Maximo = 900; break;
            case 5: Maximo = 600; break;
            case 6: Maximo = 800; break;
            case 7: Maximo = 800; break;
            case 8: Maximo = 500; break;
            case 9: Maximo = 300; break;
            case 10: Maximo = 500; break;

            default: Maximo = 1000000; break;
        }
        //Debug.Log("Escenario " + Escenario);
        //Debug.Log("points " + points);
        //Debug.Log("maximo " + Maximo);
        newPoints = (double)points / (double)Maximo;
        //Debug.Log(newPoints);

        if (newPoints <= 0) {
            n = 0;
        }
        else if (0 < newPoints && newPoints <= .2) {
            n = 1;
        }
        else if (.2 < newPoints && newPoints <= .4) {
            n = 2;
        }
        else if (.4 < newPoints && newPoints <= .6) {
            n = 3;
        }
        else if (.6 < newPoints && newPoints <= .8) {
            n = 4;
        }
        else if (.8 < newPoints && newPoints <= 1) {
            n = 5;
        }
        return n;
    }

}
