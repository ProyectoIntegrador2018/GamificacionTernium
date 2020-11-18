﻿using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Clase donde las funciones son publicas a lo largo de las escenas - vidas y puntaje -
public class GameMind : MonoBehaviour {

    // Instancia de la clase
    public static GameMind Instance;
    GameObject MenuPausa;
    GameObject MPausa;
    public Button pause;
    //GameObject BtnAyuda;
    //GameObject BA;
    //Button BtnID;
    public bool Pausado = false;
    //Text Label;
    //Image backImg;
    string Escena;

    MonoBehaviour[] comps;
    // Start is called before the first frame update

    void Start()
    {
        //*****Todo este codigo comentado es para hacer el boton de pedir ayuda funcionar,
        //*****como no funciona consistentemente se decidio comentarlo
        //*****Necesita reimplementarse todo este codigo

        //-----Empieza aqui------
        //Escena = SceneManager.GetActiveScene().name;

        //BtnAyuda = Resources.Load<GameObject>("Prefabs/BtnPAyuda");
        MenuPausa = Resources.Load<GameObject>("Prefabs/Menu");
        //(GameObject)Resources.Load("Assets/Menu/Prefabs/Menu", typeof(GameObject)); ;
        comps = GetComponents<MonoBehaviour>();

        /*if(Escena == "Achivements" ||
            Escena == "AvatarCustomization" ||
                Escena == "createNews" ||
                    Escena == "Historial" ||
                        Escena == "Instrucciones-1" ||
                            Escena == "Instrucciones-2" ||
                                Escena == "Instrucciones-3" ||
                                    Escena == "Instrucciones-4" ||
                                        Escena == "login" ||
                                            Escena == "LoseQuickMode" ||
                                                Escena == "Menu" ||
                                                    Escena == "News" ||
                                                        Escena == "Settings" ||
                                                            Escena == "Top10" ||
                                                                Escena == "UserCreation" ||
                                                                    Escena == "Ayudar" ||
                                                                        Escena == "Lose" ||
                                                                            Escena == "Win" ||
                                                                                Escena == "Instrucciones-1" ||
                                                                                    Escena.Contains("Win") ||
                                                                                        Escena.Contains("Lose")){
        Debug.Log("Nada que mostrar");
        }
        else{
            BA = Instantiate(BtnAyuda, new Vector2(-830, 240), Quaternion.identity) as GameObject;//280 -450
            BA.transform.SetParent(GameObject.Find("Canvas").transform, false);
            //BtnID = BA.GetComponent<Button>();
            Debug.Log(Escena);
        //bool ExisteAyuda = GlobalVariables.ExisteAyuda;
        //bool Aparezco = true;

        if(GlobalVariables.Caso == 0 || GlobalVariables.ElFinal == true)
        {
            Aparezco = false;
        }

        if (ExisteAyuda && GlobalVariables.VecesAyuda == 1 && Aparezco)
        {
            //string ayuda = HelpManager.Ask4Help();
            //Debug.Log(ayuda);
            
        }
        else
        {
            BA.SetActive(false);
        }*/

        
    }

    // Update is called once per frame
    void Update()
    {

        pause?.onClick.AddListener(delegate { Pausar(); });

        if ( Input.GetKeyDown(KeyCode.Escape)){ Pausar(); }

        /*BtnID.onClick.AddListener(delegate {

            //int ExisteBTN = ;
            //Debug.Log(GameObject.Find("Canvas").GetComponent("BtnMangment") as BtnMangment);
            if (GameObject.Find("Canvas").GetComponent("BtnMangment") as BtnMangment != null)
            {
                
                BtnMangment.Help();
            }
            else
            {
                QuestionManager.Help();
            }
            StartCoroutine(DisplayMessage());
            GlobalVariables.VecesAyuda = 0; });*/

    }


   

  

    public void Pausar()
    {
        if (SceneManager.GetActiveScene().name == "login" || SceneManager.GetActiveScene().name == "Menu" || SceneManager.GetActiveScene().name == "AvatarCustomization"||
          SceneManager.GetActiveScene().name == "Historial" || SceneManager.GetActiveScene().name == "Top10" || SceneManager.GetActiveScene().name == "News" ||
          SceneManager.GetActiveScene().name == "Achivements")
        {                                                 
            Pausado = false;                              
                                                          
        }                                                 
        else                                              
        {
            if (!Pausado)
            {
                //Debug.Log("LE PIco ESC a la v");
                MPausa = Instantiate(MenuPausa, new Vector2(0, 0), Quaternion.identity) as GameObject;
                MPausa.transform.SetParent(GameObject.Find("Canvas").transform, false);
                Pausado = true;
            }
            else
            {
                Destroy(MPausa);
                Pausado = false;
            }
        }

    }

  

    // Cuando el objeto donde esta el script, es "enabled" comienza a checar esto
    private void OnEnable()
    {
       

    }

    /*IEnumerator DisplayMessage()
    {
        backImg.enabled = true;
        Label.enabled = true;
        Color Og = Label.color;
        Color Oi = backImg.color;

        for (float t = 0.01f; t < 16; t += Time.deltaTime)
        {
            Label.color = Color.Lerp(Og, Color.clear, Mathf.Min(1, t / 4));
            backImg.color = Color.Lerp(Oi, Color.clear, Mathf.Min(1, t / 4));
            yield return null;
        }

        BA.SetActive(false);

    }*/

    // Función para quitar vidas
    public static void takeAwayLive(int l) {
        GlobalVariables.lives = GlobalVariables.lives-l;
        //Debug.Log("Vidas " + GlobalVariables.lives);
    }

    //Metodo para actualizar la experiencia que se dara al final de una mision
    public static void updateAccumulatedExp(int exp) {
        if (exp > 0) {
            GlobalVariables.accumulatedExp += exp;
        }
        
    }

    // Función para sumar o restar puntos
    public static void addPoints(int n) {

    	GlobalVariables.score = GlobalVariables.score+n;
        //De momento la experiencia solo es la mitad de los puntos que se da por cada pregunta pero se puede cambiar la formula facilmente
        updateAccumulatedExp(n/2);

    }

    // Función para agregar al usuario
    public static void logOn(string u, string p) {
        // Debug.Log("a ver " + Database.login(u,p));
        int id=Database.login(u,p);
        /*if(u=="TERGAMI" && p=="TERNIUM20") {
            SceneManager.LoadScene("createUser");
        }*/
        if(id!=-1) {
            GlobalVariables.username = u;
            GlobalVariables.usernameId = id;
            GlobalVariables.equipo = getEquipo();
            GlobalVariables.currentQuickGameLives = Database.getCurrentLives(id);
            SceneManager.LoadScene("Menu");
        }
      
        // Debug.Log("this user has " + Database.getCurrentAchivements() + "achivmenets");
        // Database.makeUser("test11","test11");
    }

     // Función para hacer logoff al usuario
    public static void logOff() {
        GlobalVariables.username = null;
    	Debug.Log("usuario " + GlobalVariables.username);
    }

     // Función para des usuario
    public static bool getAchivement(int achivementId) {
        return Database.getAchivement(achivementId);
    }

    public static void setAchivement(int achivementId) {
        Database.setAchivement(achivementId);
    }

    public static bool getStarted(int achivementId)
    {
        return Database.getStarted(achivementId);
    }

    public static void setStarted(int achivementId)
    {
        Database.setStarted(achivementId);
    }

    public static int getScore(int scoreId) {
       return  Database.getScore(scoreId);

    }

    public static void setScore(int scoreId, int score) {
        Database.setScore(scoreId, score);

    }

    public static string getEquipo(){
        return Database.getEquipo();
    }

    public static bool getTutorial() {
        return Database.getTutorial();
    }

    public static void setTutorial() {
        Database.setTutorial();
    }

    public static void saveData()
    {
        Database.saveData();
    }

}
