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

    public Text SiguentePregunta;

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
        btnReplay.onClick.AddListener(delegate { JugarMisionIndex(index); });
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

    IEnumerator EsperarMin(int Escenario) {
        //Print the time of when the function is first called.
        //Debug.Log("Started Coroutine at timestamp : " + Time.time);

        //yield on a new YieldInstruction that waits for 5 seconds.

        //ProximaMission.SetActive(true);


        yield return new WaitForSeconds(5);

        switch (Escenario) {
            case 1: SceneManager.LoadScene("P1"); break;
            case 2: SceneManager.LoadScene("ES2P1"); break;
            case 3: SceneManager.LoadScene("ES3P1"); break;
            case 4: SceneManager.LoadScene("ES4P1"); break;
            case 5: SceneManager.LoadScene("ES5P1"); break;
            case 6: SceneManager.LoadScene("ES6P1"); break;
            case 7: SceneManager.LoadScene("ES7P1"); break;
            case 8: SceneManager.LoadScene("ES8P1"); break;
            case 9: SceneManager.LoadScene("ES9P1"); break;
            case 10: SceneManager.LoadScene("ES10P1"); break;

            default:
                break;
        }

        //After we have waited 5 seconds print the time again.
        //Debug.Log("Finished Coroutine at timestamp : " + Time.time);
    }


    public void JugarMisionIndex(int index) {

        StartCoroutine(EsperarMin(index));

        GlobalVariables.Caso = index;
        HelpManager.ExisteAyuda(index.ToString());
        // TODO: Add animation to start of selection of case
        switch (index) {
            case 1: SiguentePregunta.text = "Mision 1: Reparar el rodillo dañado"; break;
            case 2: SiguentePregunta.text = "Mision 2: Inspeccionar avería de Acoplamiento"; break;
            case 3: SiguentePregunta.text = "Mision 3: Prevenir el sobrecalentamiento"; break;
            case 4: SiguentePregunta.text = "Mision 4: Inspeccionar los sensores de proximidad"; break;
            case 5: SiguentePregunta.text = "Mision 5: Inspeccionar sobrecarga de motor"; break;
            case 6: SiguentePregunta.text = "Mision 6: Inspeccionar niveles de aceite, "; break;
            case 7: SiguentePregunta.text = "Mision 7: La emergencia PM10 "; break;
            case 8: SiguentePregunta.text = "Mision 8: El PM11 programado PM11"; break;
            case 9: SiguentePregunta.text = "Mision 9: Contestar aviso M3"; break;
            case 10: SiguentePregunta.text = "Mision 10:Contestar aviso M6"; break;

            default:
                break;
        }

    }

}
