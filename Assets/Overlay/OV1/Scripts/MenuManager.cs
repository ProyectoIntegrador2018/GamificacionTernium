﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

// Clase para manejar todo lo que tenga que ver con la escena de "Menu"
// Creada por el equipo 1, modificada por el equipo 2

public class MenuManager : MonoBehaviour
{
    public GameObject jugarBtn;
    public GameObject historialBtn;
    public GameObject trofeosBtn;
    public GameObject salirBtn;
    public GameObject eventsBtn;
    public GameObject ProximaMission;
    public GameObject toast;
    public GameObject avatarBtn;
    //Necesita refactor
    //public Image teamImage;
    //public Sprite pinkImg;
    //public Sprite greenImg;
    //public Sprite blueImg;
    public Text SiguentePregunta;
    public Text mensajeBienvenida;
    public static Users userBase;
    public int cont = 0;
    bool FirstClick = true;
    bool adminCheck = false;

    public GameObject GameModesMenu;
    public Button JugarMisiones;
    public Button JugarModoRapido;
    public static bool quickMode = false;
    //public GameObject podium;
    //public GameObject turno;

    // Start is called before the first frame update
    void Start() {

        //Jugar.enabled = true;
        FirstClick = true;
        mensajeBienvenida = GetComponent<Text>();
        //GlobalVariables.Caso = 0;
        
        //Si el usuario es admin
        if (adminCheck) {
            trofeosBtn.SetActive(false);
            historialBtn.SetActive(false);
            avatarBtn.SetActive(false);
            toast.SetActive(false);
            eventsBtn.SetActive(true);
            adminCheck = true;
        }
    }
    // Update is called once per frame
    void Update(){

    }

    private void OnEnable()
    {
        // Condicion para mostrar el mensaje de bienvenida
        if (Database.isAdmin(GlobalVariables.usernameId)) {
            adminCheck = true;
        }
        if (mensajeBienvenida != null) {
            if(adminCheck){
                mensajeBienvenida.text = "¡Bonito día, " + GlobalVariables.username + "!";
            }
            else if(Database.getCurrentAchivements() == 0) {
            mensajeBienvenida.text = "¡Bonito día, " + GlobalVariables.username + " de la " + GlobalVariables.equipo + "! Aún no has completado misiones a la perfección, ¡Intentalo, son 10 en total!";
            }
            else if (Database.getCurrentAchivements() == 1) {
                mensajeBienvenida.text = "¡Bonito día, " + GlobalVariables.username + " de la " + GlobalVariables.equipo + "! Has completado a la perfección " + Database.getCurrentAchivements().ToString() + " misión de 10";
            }
            else if (Database.getCurrentAchivements() < 11 && Database.getCurrentAchivements() > 1) {
                mensajeBienvenida.text = "¡Bonito día, " + GlobalVariables.username + " de la " + GlobalVariables.equipo + "! Has completado a la perfección " + Database.getCurrentAchivements().ToString() + " misiones de 10";
            } 
            else {
                mensajeBienvenida.text = "¡Bonito día, " + GlobalVariables.username + " de la " + GlobalVariables.equipo + "!";
            }
        }
        
        // mensajeBienvenida.text = "Bonito día, " + GlobalVariables.username + "! Te faltan ganar" + GlobalVariables.getTrophies().ToString() + " de 10 trofeos";
    }

    public void jugarClicked(){
        //Presionar boton de jugar para abrir menu de modos de juego
            GameModesMenu.SetActive(true);
    }

    public void hideGameMode(){
        GameModesMenu.SetActive(false);
    }

    public void jugarClasico(){
        //Jugar.enabled = false;
            toast.SetActive(false);
            if (FirstClick)
            {
                FirstClick = false;
           
                if (GameMind.getTutorial() == true)
                {
                    //podium.SetActive = false;
                    //turno.SetActive = false;
                    int i = 1;
                    SiguentePregunta.text = "Mision 1: Reparar el rodillo dañado";
                    ProximaMission.SetActive(true);
                    StartCoroutine(EsperarMin(0));
                    if (!Database.isAdmin(GlobalVariables.usernameId))
                    {
                        GameMind.setStarted(i);
                    }
                    //GameMind.saveData();
                    HelpManager.ExisteAyuda(i.ToString());
                    GlobalVariables.Caso = i;

                }
                else
                {
                    quickMode = false;
                    JugarMision();

                }
            }
    }

    public void jugarRapido(){
            //Jugar.enabled = false;
            toast.SetActive(false);
            quickMode = true;
            GlobalVariables.lives = 5;
            JugarMision();
    }

    public void trofeosClicked(){
        CambiarScene("Achivements");
    }

    public void historialClicked(){
        CambiarScene("Historial");
    }

    public void salirClicked(){
        CambiarScene("No");
    }

    public void eventsClicked(){
        CambiarScene("News");
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
        //Atte. Equipo 1

        //Set mision as Started
        if (!Database.isAdmin(GlobalVariables.usernameId)) {
            GameMind.setStarted(Rand);
            GameMind.saveData();
        }
        HelpManager.ExisteAyuda(Rand.ToString());
        GlobalVariables.Caso = Rand;

        if (!quickMode)
        {
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
                case 10: SiguentePregunta.text = "Mision 10:Contestar aviso M6"; break;

                default:
                    break;
            }
        }
        else
        {
            SiguentePregunta.text = "Modo rapido: Juega hasta agotar tus intentos!!";
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

    //Necesita refactor
    /*
    public void showTeam(){
        if(GlobalVariables.equipo == "Rosa"){
            teamImage.sprite = pinkImg;
        }
        else if(GlobalVariables.equipo == "Verde"){
            teamImage.sprite = greenImg;
        }
        else if(GlobalVariables.equipo == "Azul"){
            teamImage.sprite = blueImg;
        }
    }*/

}

