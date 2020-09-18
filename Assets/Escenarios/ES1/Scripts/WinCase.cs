using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Clase para cuando el jugador gana o pierde
public class WinCase : MonoBehaviour {

    // Variables publicas
    public Text HighScore;
    public Text YourScore;
    public Sprite[] StarSprites;
    public Image Stars1UI;
    public Image Stars2UI;
    public int textOption;
    public Text Title;
    string Ignorar;

    GameObject ProximaMission;
    GameObject PM;
    Text SiguentePregunta;
    int Caso = GlobalVariables.Caso;

   
    GameObject Trofeo;

    private void Start() {
        //Debug.Log("llegue a script");
        Trofeo = GameObject.Find("Trophy");

        PM = Resources.Load<GameObject>("Prefabs/LoadingQuestion");
        ProximaMission = Instantiate(PM, new Vector2(0, 0), Quaternion.identity) as GameObject;//280 -450
        ProximaMission.transform.SetParent(GameObject.Find("Canvas").transform, false);
        SiguentePregunta = ProximaMission.GetComponentInChildren<Text>();
        ProximaMission.transform.localScale = new Vector3(1.1f, 1.1f, 1f);
        ProximaMission.SetActive(false);

        GlobalVariables.ElFinal = true;
        
        if(Trofeo != null)
        {
            Trofeo.SetActive(false);
        }
        //Toma el score guardado hasta el momento y lo archiva dependiendo de la escena win or lose que se encuentre
        string Escena = SceneManager.GetActiveScene().name;
        //Debug.Log(Escena);
        string highscore;
        if (Escena == "Win" || Escena == "Lose")
        {
            highscore = "HighScoreES1";
        } else {
            highscore = "HighScore" + Escena.Substring(0, 3);
        }
        //Debug.Log("Caso: "+Caso);
        //Debug.Log("Caso2: " + (Caso-1));


        if (GlobalVariables.score >= Database.getScore(GlobalVariables.Caso-1) )
        {
            //Debug.Log("Entre");
            //Debug.Log("Entre");
            Database.setScore(GlobalVariables.Caso, GlobalVariables.score);
            Database.saveData();
            //PlayerPrefs.SetInt(highscore, GlobalVariables.score);
        }
        
        textOption = StarNumber(GlobalVariables.score, 1);
        //Debug.Log(textOption);

        if (Escena == "Win"){
            Debug.Log(Escena);
            if (textOption == 0) {
                Title.text = "Completaste la misión pero deberías considerar seriamente en mejorar tu desempeño";
            } else if (textOption == 1) {
                Title.text = "→ Completaste la misión pero creemos que tu desempeño puede mejorar y conseguir más estrellas";
            } else if (textOption == 2) {
                Title.text = "Desempeño promedio, ¡intenta otra vez y consigue más estrellas!";
            } else if (textOption == 3) {
                Title.text = "Estás muy cerca de la perfección, todos cometemos errores, ¡inténtalo de nuevo!";
            } else if (textOption == 4) {
                Title.text = "Muy cerca de la perfección, busca apoyo de un supervisor para mejorar";
            } else if (textOption == 5) {
                Title.text = "¡Muy bien! ¡Ahora saber cómo reparar un rodillo!";
               //Database.setAchivement(1);
                // AchievementManager.GainAchievement(1);
            }
        }       
        else if (Escena == "ES2Win")
        {
            if (textOption == 0)
            {
                Title.text = "Completaste la misión pero deberías considerar seriamente en mejorar tu desempeño";
            }
            else if (textOption == 1)
            {
                Title.text = "→ Completaste la misión pero creemos que tu desempeño puede mejorar y conseguir más estrellas";
            }
            else if (textOption == 2)
            {
                Title.text = "Desempeño promedio, ¡intenta otra vez y consigue más estrellas!";
            }
            else if (textOption == 3)
            {
                Title.text = "Estás muy cerca de la perfección, todos cometemos errores, ¡inténtalo de nuevo!";
            }
            else if (textOption == 4)
            {
                Title.text = "Muy cerca de la perfección, busca apoyo de un supervisor para mejorar";
            }
            else if (textOption == 5)
            {
                Title.text = "¡Muy bien! ¡Ahora sabes como actuar cuando escuches una averia!";
                //Database.setAchivement(2);
                // AchievementManager.GainAchievement(2);
            }
        }
        else if (Escena == "ES3Win")
        {
            if (textOption == 0)
            {
                Title.text = "Completaste la misión pero deberías considerar seriamente en mejorar tu desempeño";
            }
            else if (textOption == 1)
            {
                Title.text = "→ Completaste la misión pero creemos que tu desempeño puede mejorar y conseguir más estrellas";
            }
            else if (textOption == 2)
            {
                Title.text = "Desempeño promedio, ¡intenta otra vez y consigue más estrellas!";
            }
            else if (textOption == 3)
            {
                Title.text = "Estás muy cerca de la perfección, todos cometemos errores, ¡inténtalo de nuevo!";
            }
            else if (textOption == 4)
            {
                Title.text = "Muy cerca de la perfección, busca apoyo de un supervisor para mejorar";
            }
            else if (textOption == 5)
            {
                Title.text = "¡Muy bien! ¡Ahora sabes como actuar cuando una bomba se sobre calenta!";
                //Database.setAchivement(3);
                //AchievementManager.GainAchievement(3);
            }
        }
        else if (Escena == "ES4Win")
        {
            if (textOption == 0)
            {
                Title.text = "Completaste la misión pero deberías considerar seriamente en mejorar tu desempeño";
            }
            else if (textOption == 1)
            {
                Title.text = "→ Completaste la misión pero creemos que tu desempeño puede mejorar y conseguir más estrellas";
            }
            else if (textOption == 2)
            {
                Title.text = "Desempeño promedio, ¡intenta otra vez y consigue más estrellas!";
            }
            else if (textOption == 3)
            {
                Title.text = "Estás muy cerca de la perfección, todos cometemos errores, ¡inténtalo de nuevo!";
            }
            else if (textOption == 4)
            {
                Title.text = "Muy cerca de la perfección, busca apoyo de un supervisor para mejorar";
            }
            else if (textOption == 5)
            {
                Title.text = "¡Muy bien! ¡Ahora sabes como actuar hay un fallo con sensores!";
                //Database.setAchivement(4);
                // AchievementManager.GainAchievement(4);
            }
        }
        else if (Escena == "ES5Win")
        {
            if (textOption == 0)
            {
                Title.text = "Completaste la misión pero tu desempeño no fue optímo. Guardia, aun puedes mejorar en la sobrecarga de un motor.";
            }
            else if (textOption == 1)
            {
                Title.text = "Completaste la misión pero tu desempeño puede mejorar. ¡Sigue aprendiendo!";
            }
            else if (textOption == 2)
            {
                Title.text = "Completaste la misión pero hubo mal desempeño, en caso de duda consulta los manuales ó tu superior por pasos a seguir!";
            }
            else if (textOption == 3)
            {
                Title.text = "Estas a la mitad del camino hacia un desempeño perfecto. ¡Revisa tus manuales!";
            }
            else if (textOption == 4)
            {
                Title.text = "¡Estas muy cerca de la perfección!, revisa tus pasos y consulta con superiores o manuales.";
            }
            else if (textOption == 5)
            {
                Title.text = "¡Muy bien! ¡Ahora sabes como actuar en caso de una sobre carga de motor!";
                //Database.setAchivement(5);
                // AchievementManager.GainAchievement(5);
            }
        }
        else if (Escena == "ES6Win")
        {
            if (textOption == 0)
            {
                Title.text = "Completaste la misión pero tu desempeño no fue optímo. Guardia, aun puedes mejorar en caso de avería por nivel de aceite.";
            }
            else if (textOption == 1)
            {
                Title.text = "Completaste la misión pero tu desempeño puede mejorar. ¡Sigue aprendiendo!";
            }
            else if (textOption == 2)
            {
                Title.text = "Completaste la misión pero hubo mal desempeño, en caso de duda consulta los manuales ó tu superior por pasos a seguir!";
            }
            else if (textOption == 3)
            {
                Title.text = "Estas a la mitad del camino hacia un desempeño perfecto. ¡Revisa tus manuales!";
            }
            else if (textOption == 4)
            {
                Title.text = "¡Estas muy cerca de la perfección!, revisa tus pasos y consulta con superiores o manuales.";
            }
            else if (textOption == 5)
            {
                Title.text = "¡Muy bien! ¡Ahora sabes como actuar cuando haya una avería de nivel de aceite!";
                //Database.setAchivement(6);
                // AchievementManager.GainAchievement(5);
            }
        }
        else if (Escena == "ES7Win")
        {
            if (textOption == 0)
            {
                Title.text = "Completaste la misión pero tu desempeño no fue optímo. Inspector, aun puedes mejorar en la redacción de un Aviso M6 .";
            }
            else if (textOption == 1)
            {
                Title.text = "Completaste la misión pero tu desempeño puede mejorar. ¡Sigue aprendiendo!";
            }
            else if (textOption == 2)
            {
                Title.text = "Completaste la misión pero hubo mal desempeño, en caso de duda consulta los manuales ó tu superior por pasos a seguir!";
            }
            else if (textOption == 3)
            {
                Title.text = "Estas a la mitad del camino hacia un desempeño perfecto. ¡Revisa tus manuales!";
            }
            else if (textOption == 4)
            {
                Title.text = "¡Estas muy cerca de la perfección!, revisa tus pasos y consulta con superiores o manuales.";
            }
            else if (textOption == 5)
            {
                Title.text = "¡Muy bien! ¡Ahora sabes como lidiar con un correctivo de emergencia PM10!";
                //Database.setAchivement(7);
                // AchievementManager.GainAchievement(5);
            }
        }
        else if (Escena == "ES8Win")
        {
            if (textOption == 0)
            {
                Title.text = "Completaste la misión pero tu desempeño no fue optímo. Inspector, aun puedes mejorar en la redacción de un Aviso M6 .";
            }
            else if (textOption == 1)
            {
                Title.text = "Completaste la misión pero tu desempeño puede mejorar. ¡Sigue aprendiendo!";
            }
            else if (textOption == 2)
            {
                Title.text = "Completaste la misión pero hubo mal desempeño, en caso de duda consulta los manuales ó tu superior por pasos a seguir!";
            }
            else if (textOption == 3)
            {
                Title.text = "Estas a la mitad del camino hacia un desempeño perfecto. ¡Revisa tus manuales!";
            }
            else if (textOption == 4)
            {
                Title.text = "¡Estas muy cerca de la perfección!, revisa tus pasos y consulta con superiores o manuales.";
            }
            else if (textOption == 5)
            {
                Title.text = "¡Muy bien! ¡Ahora sabes como lidiar con un correctivo de programado PM11!";
                //Database.setAchivement(8);
                // AchievementManager.GainAchievement(5);
            }
        }
        else if (Escena == "ES9Win")
        {
            if (textOption == 0)
            {
                Title.text = "Completaste la misión pero tu desempeño no fue optímo. Inspector, aun puedes mejorar en la redacción de un Aviso M6 .";
            }
            else if (textOption == 1)
            {
                Title.text = "Completaste la misión pero tu desempeño puede mejorar. ¡Sigue aprendiendo!";
            }
            else if (textOption == 2)
            {
                Title.text = "Completaste la misión pero hubo mal desempeño, en caso de duda consulta los manuales ó tu superior por pasos a seguir!";
            }
            else if (textOption == 3)
            {
                Title.text = "Estas a la mitad del camino hacia un desempeño perfecto. ¡Revisa tus manuales!";
            }
            else if (textOption == 4)
            {
                Title.text = "¡Estas muy cerca de la perfección!, revisa tus pasos y consulta con superiores o manuales.";
            }
            else if (textOption == 5)
            {
                Title.text = "¡Muy bien! ¡Ahora sabes que contiene un Aviso M3!";
                //Database.setAchivement(9);
                // AchievementManager.GainAchievement(5);
            }
        }
        else if (Escena == "ES10Win")
        {
            if (textOption == 0)
            {
                Title.text = "Completaste la misión pero tu desempeño no fue optímo. Inspector, aun puedes mejorar en la redacción de un Aviso M6 .";
            }
            else if (textOption == 1)
            {
                Title.text = "Completaste la misión pero tu desempeño puede mejorar. ¡Sigue aprendiendo!";
            }
            else if (textOption == 2)
            {
                Title.text = "Completaste la misión pero hubo mal desempeño, en caso de duda consulta los manuales ó tu superior por pasos a seguir!";
            }
            else if (textOption == 3)
            {
                Title.text = "Estas a la mitad del camino hacia un desempeño perfecto. ¡Revisa tus manuales!";
            }
            else if (textOption == 4)
            {
                Title.text = "¡Estas muy cerca de la perfección!, revisa tus pasos y consulta con superiores o manuales.";
            }
            else if (textOption == 5)
            {
                Title.text = "¡Muy bien! ¡Ahora sabes que contiene un Aviso M6!";
                //Database.setAchivement(10);
                // AchievementManager.GainAchievement(5);
            }
        }
        else if(Escena == "Lose" || Escena == "ES2Lose" || Escena == "ES3Lose" || Escena == "ES4Lose" || Escena == "ES5Lose" || Escena == "ES6Lose" || Escena == "ES7Lose" || Escena == "ES8Lose" || Escena == "ES9Lose" || Escena == "ES10Lose" )
        {
            if (textOption == 0)
            {
                Title.text = "Perdiste y tuviste un mal desempeño. Sabemos que lo puedes hacer mejor, intentalo de nuevo.";
            }
            else if (textOption == 1)
            {
                Title.text = "Perdiste y tu desempeño no fue el mejor. Vuelvelo a intentar para obtener más estrellas.";
            }
            else if (textOption == 2)
            {
                Title.text = "Perdiste pero tu desempeño fue promedio, ¡intenta mejorar tu desempeño para ganar en la próxima!";
            }
            else if (textOption == 3)
            {
                Title.text = "Perdiste pero tu desempeño fue bueno, ¡intentalo de nuevo y verás que ganarás!";
            }
            else if (textOption == 4)
            {
                Title.text = "Perdiste pero estás cerca de la perfección, busca apoyo de un supervisor para mejorar";
            }
            else if (textOption == 5)
            {
                Title.text = "";
            }
        }

        Stars1UI.sprite = StarSprites[textOption];
        Stars2UI.sprite = StarSprites[ StarNumber(Database.getScore(GlobalVariables.Caso-1) ,0) ] ;
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    // Funcion que regresa la cantidad de estrellas necesaria
    public int StarNumber(int points, int PV) {
        //Debug.Log("entre a estrellas");

        int n = 0;
        string Escena = SceneManager.GetActiveScene().name;
        string Escenario = GlobalVariables.Caso.ToString();
        int Maximo;
        double newPoints = 0;


        switch (Escenario)
        {
            case "0": Maximo = 1; break;
            //Los Escenarios
            case "1": Maximo = 800; break;
            case "2": Maximo = 900; break;
            case "3": Maximo = 500; break;
            case "4": Maximo = 900; break;
            case "5": Maximo = 600; break;
            case "6": Maximo = 800; break;
            case "7": Maximo = 800; break;
            case "8": Maximo = 500; break;
            case "9": Maximo = 300; break;
            case "10": Maximo = 500; break;

            default: Maximo = 1000000; break;
        }
        //Debug.Log("points " + points);
        //Debug.Log("maximo " + Maximo);
        newPoints = (double)points / (double)Maximo;
        //Debug.Log(newPoints);

        //Debug.Log(Maximo);
        //Debug.Log(points);
        //Debug.Log(newPoints);

        if (newPoints <= 0) {
            n = 0;
        } else if (0 < newPoints && newPoints <= .2) {
            n = 1;
        } else if (.2 < newPoints && newPoints <= .4) {
            n = 2;
        }  else if (.4 < newPoints && newPoints <= .6) {
            n = 3;
        }  else if (.6 < newPoints && newPoints <= .8) {
            n = 4;
        }  else if (.8 < newPoints && newPoints <= 1) {
            n = 5;
            if(newPoints == 1 && PV ==1)
            {
                Trofeo.SetActive(true);
                Database.setAchivement(GlobalVariables.Caso);
                Database.saveData();


                //Aqui hay que agregar a este wey
                HelpManager.AgregarAyuda(GlobalVariables.username, GlobalVariables.Caso.ToString());
                
            }
        }
        //Debug.Log("n " + n);
        return n;
    } 

    // Regresar al menu principal
    public void GoToMainMenu() {
        GlobalVariables.lives = 5;
        // TODO : Cuando tengamos la escena del menu
        GlobalVariables.score = 0;
        GlobalVariables.sumPos = -20;
        GlobalVariables.pairAnswerSlot.Clear();
        GlobalVariables.items.Clear();
        GlobalVariables.ExisteAyuda = false;
        GlobalVariables.VecesAyuda = 1;
        GlobalVariables.ElFinal = false;
        SceneManager.LoadScene("Menu");
    }

    // Reiniciar el juego
    public void ResetGame() {

        GlobalVariables.lives = 5;
        GlobalVariables.score = 0;
        GlobalVariables.sumPos = -20;
        GlobalVariables.pairAnswerSlot.Clear();
        GlobalVariables.items.Clear();
        GlobalVariables.ExisteAyuda = false;
        GlobalVariables.VecesAyuda = 1;
        GlobalVariables.ElFinal = false;
        string Escena = SceneManager.GetActiveScene().name;

        int Rand = Random.Range(1, 11);
        
        while (Rand == GlobalVariables.Caso)
        {
            Rand = Random.Range(1, 11);
        }

        //
        //Rand = 1;
        //
        GameMind.setStarted(Rand);
        GameMind.saveData();
        GlobalVariables.Caso = Rand;
        HelpManager.ExisteAyuda(Rand.ToString());
        //Debug.Log("Nuevo Caso");
        //Debug.Log(Rand);
        //Debug.Log(SiguentePregunta);

        ProximaMission.SetActive(true);

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

        StartCoroutine(EsperarMin(Rand));
        

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

    public void GoToAchievements()
    {
        SceneManager.LoadScene("Achivements");
    }

}
