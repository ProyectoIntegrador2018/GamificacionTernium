using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    public Button Jugar;
    public Button Historial;
    public Button Trofeos;
    public Button Salir;
    public GameObject ProximaMission;
    public Text SiguentePregunta;
    public Text mensajeBienvenida;
    public static Users userBase;
    public int cont = 0;
    bool FirstClick = true;

    // Start is called before the first frame update
    void Start()
    {
        //Jugar.enabled = true;
        FirstClick = true;
        mensajeBienvenida = GetComponent<Text>();
        //GlobalVariables.Caso = 0;
    }
    // Update is called once per frame
    void Update(){

    }

    private void OnEnable()
    {

        if (mensajeBienvenida != null) {
            if (Database.getCurrentAchivements() == 0) {
            mensajeBienvenida.text = "¡Bonito día, " + GlobalVariables.username + "! Aún no has completado misiones a la perfección, ¡Intentalo, son 10 en total!";
            }
            else if (Database.getCurrentAchivements() == 1) {
                mensajeBienvenida.text = "¡Bonito día, " + GlobalVariables.username + "! Has completado a la perfección " + Database.getCurrentAchivements().ToString() + " misión de 10";
            }
            else if (Database.getCurrentAchivements() < 11 && Database.getCurrentAchivements() > 1) {
                mensajeBienvenida.text = "¡Bonito día, " + GlobalVariables.username + "! Has completado a la perfección " + Database.getCurrentAchivements().ToString() + " misiones de 10";
            } 
            else {
                mensajeBienvenida.text = "¡Bonito día, " + GlobalVariables.username + "!";
            }
        }
        
        // mensajeBienvenida.text = "Bonito día, " + GlobalVariables.username + "! Te faltan ganar" + GlobalVariables.getTrophies().ToString() + " de 10 trofeos";

        // El checar que los botones sean presionados, y que pasa si lo son
        Jugar.onClick.AddListener(delegate {
            //Jugar.enabled = false;
            if(FirstClick)
            {
                FirstClick = false;
                if (GameMind.getTutorial() == true)
                {
                    int i = 1;
                    SiguentePregunta.text = "Mision 1: Reparar el rodillo dañado";
                    ProximaMission.SetActive(true);
                    StartCoroutine(EsperarMin(0));
                    GameMind.setStarted(i);
                    //GameMind.saveData();
                    HelpManager.ExisteAyuda(i.ToString());
                    GlobalVariables.Caso = i;
                    
                }
                else
                {
                    JugarMision();
                }
            }

            
        });
        //Historial.onClick.AddListener(delegate { CambiarScene(""); });
        Trofeos.onClick.AddListener(delegate { CambiarScene("Achivements"); });
        Historial.onClick.AddListener(delegate {CambiarScene("Historial");});
        Salir.onClick.AddListener(delegate { CambiarScene("No"); });
    }

    void CambiarScene(string Cambio)
    {
        if(Cambio == "No")
        {
            GlobalVariables.username = null;
            SceneManager.LoadScene("login");
        }
        else
        {
            SceneManager.LoadScene(Cambio);
        }
    }

    public void JugarMision()
    {
        int Rand = Random.Range(1, 11);

        //-------------------------------------------------------------------------------
        //Aqui pueden modificarle para llegar a un Caso especial 
        
        //Rand = 9;

        //-------------------------------------------------------------------------------
        //Ok, estas listo leecto?, porque nos pidieron que hicieramos un fix, que tomaria mucho rework a la hora de conectar
        //asi que estoy a punto de aventarme lo mas clandestino del mundo

        
 	
        //Set mision as Started
        GameMind.setStarted(Rand);
        GameMind.saveData();
        HelpManager.ExisteAyuda(Rand.ToString());
        GlobalVariables.Caso = Rand;

        switch (Rand)
        {
            case 1: SiguentePregunta.text = "Mision 1: Reparar el rodillo dañado"; break;
            case 2: SiguentePregunta.text = "Mision 2: Inspeccionar avería de Acoplamiento"; break;
            case 3: SiguentePregunta.text = "Mision 3: Prevenir el sobrecalentamiento"; break;
            case 4: SiguentePregunta.text = "Mision 4: Inspeccionar los sensores de proximidad"; break;
            case 5: SiguentePregunta.text = "Mision 5: Inspeccionar sobrecarga de motor"; break;
            case 6: SiguentePregunta.text = "Mision 6: Inspeccionar niveles de aceite"; break;
            case 7: SiguentePregunta.text = "Mision 7: La emergencia PM10 "; break;
            case 8: SiguentePregunta.text = "Mision 8: El PM11 programado PM11"; break;
            case 9: SiguentePregunta.text = "Mision 9: Contestar aviso M3"; break;
            case 10: SiguentePregunta.text= "Mision 10:Contestar aviso M6"; break;

            default:
                break;
        }
        StartCoroutine(EsperarMin(Rand));
        ProximaMission.SetActive(true);

    }

    IEnumerator EsperarMin(int Escenario)
    {
        //Debug.Log(Escenario);
        //Print the time of when the function is first called.
        //Debug.Log("Started Coroutine at timestamp : " + Time.time);

        //yield on a new YieldInstruction that waits for 5 seconds.

       
        yield return new WaitForSeconds(5);

        switch (Escenario)
        {
            case 0: SceneManager.LoadScene("Instrucciones-1"); break;
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


    public void BtnInstrucciones()
    {
        SceneManager.LoadScene("P1");
    }

    public void BtnHistorial(int NumeroDCaso)
    {
        string caso = "ES" + NumeroDCaso + "P1";
        SceneManager.LoadScene(caso);
    }

}

