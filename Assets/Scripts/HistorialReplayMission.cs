using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HistorialReplayMission : MonoBehaviour
{

    public Text SiguentePregunta;
    public GameObject ProximaMission;

    //se suscribe a los eventos de la clase singleton
    private void Start() {
        HistorialEventSystem.current.onReplayClick += JugarMisionIndex;
    }

    //se des-suscribe a los eventos de la clase singleton
    private void OnDestroy() {
        HistorialEventSystem.current.onReplayClick -= JugarMisionIndex;
    }

    //corutina para inicar la mision deseada despues del tiempo que se desea esperar
    IEnumerator EsperarMin(int Escenario) {
        //Print the time of when the function is first called.
        //Debug.Log("Started Coroutine at timestamp : " + Time.time);

        //yield on a new YieldInstruction that waits for 5 seconds.

        ProximaMission.SetActive(true);


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


    //funcion para iniciar la mision deseada
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
            case 10: SiguentePregunta.text = "Mision 10: Contestar aviso M6"; break;

            default:
                break;
        }

    }
}
