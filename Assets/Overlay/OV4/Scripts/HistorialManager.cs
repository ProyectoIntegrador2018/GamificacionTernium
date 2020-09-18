using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HistorialManager : MonoBehaviour
{

    public const int NUMMISIONS = 10;

    public GameObject MisionPrefab;
    public GameObject MisionPrefabLose;

    public GameObject MisionDescription;

    public GameObject MisionScore;

    public Sprite[] StarSprites;
    public Image Stars1UI;
    public Text SiguentePregunta;
    public GameObject ProximaMission;

    public Button btnReplay;


    public class Mision
    {
        public Mision()
        {
            Title = "";
            Description = "";
            DescriptionMala = "";
            Achieved = false;
            Score = -1;
        }

        public Mision(string title, string description, string descripcionMala, bool achived, int score)
        {
            Title = title;
            Description = description;
            DescriptionMala = descripcionMala;
            Achieved = achived;
            Score = score;
        }

        public string Title { get; }
        public string Description { get; }
        public string DescriptionMala { get; }
        public bool Achieved;
        public int Score;
    }

    public static Mision[] misionsArr = new Mision[NUMMISIONS]
{
    new Mision("¡Avería en el Rodillo!","Repara correctamente un rodillo antiderrapante.", "???", false, 0),
    new Mision("¡Avería en Acoplamiento!", "Repara correctamente una avería de acoplamiento.", "???", false, 0),
    new Mision("El Sobrecalentamiento en Bomba.", "Juego de Sobrecalentamiento de Bombas", "???", false, 0),
    new Mision("El Sensor de Proximidad genera demoras.", "Escenario donde el sensor de proximidad genera demoras.", "???", false, 0),
    new Mision("¡Sobrecarga de Motor hace demoras!", "Escenario donde la sobrecarga de un motor genera demoras!", "???", false, 0),
    new Mision("¡Avería: Nivel de Aceite!", "Escenario de una sobrecarga de aceite!", "???", false, 0),
    new Mision("¡Emergencia PM10, Guardia!", "¡Escenario de emergencia PM10!", "???", false, 0),
    new Mision("Correctivo programado PM11, Inspector.", "Correctivo programado PM11 para Inspector.", "???", false, 0),
    new Mision("Aviso M3, Inspector", "Aviso M3 Para el inspector!", "???", false, 0),
    new Mision("Aviso M6, Inspector", "Aviso M6 Para el inspector!", "???", false, 0),
};


    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i <NUMMISIONS; i++)
        {
            // misionsArr[i].Achieved = GameMind.getAchivement(i);
            if (Database.getStarted(i))
            {
                CreateMision(i, "Mision Container", misionsArr[i].Title, misionsArr[i].Description, misionsArr[i].DescriptionMala, misionsArr[i].Achieved = Database.getStarted(i), misionsArr[i].Score = Database.getScore(i));
            }
        }
        //Debug.Log(GlobalVariables.usernameId);
    }

    // Update is called once per frame
    void Update()
    {
        SetInfo();
    }

    public void CreateMision(int id, string category, string title, string description, string descripcionMala, bool achived, int score)
    {

        GameObject mision;
        //Render either a resolved or not resolved (black Achivement)
        if (achived)
        {
            mision = (GameObject)Instantiate(MisionPrefab);
        }
        else
        {
            mision = (GameObject)Instantiate(MisionPrefabLose);
        }


        SetMisionInfo(id, category, mision, title, description, descripcionMala, achived, score);
    }

    public void SetMisionInfo(int id, string category, GameObject mision, string title, string description, string descripcionMala, bool achieved, int score)
    {
        mision.transform.SetParent(GameObject.Find(category).transform);
        //Transformation values chose by hand / experimentation
        mision.transform.localScale = new Vector3((float)0.73, (float)1.006147, 1);
        mision.transform.GetChild(1).GetComponent<Text>().text = title;
        if (achieved)
        {
            mision.transform.GetChild(2).GetComponent<Text>().text = description;
        }
        else
        {
            mision.transform.GetChild(2).GetComponent<Text>().text = descripcionMala;
        }

        mision.transform.GetChild(3).GetComponent<Text>().text = score.ToString();

    }

    public static void AchiveMision(int index)
    {
        misionsArr[index].Achieved = true;
    }

    public void SetInfo()
    {
        MisionDescription.transform.GetComponent<Text>().text = MisionFunction.Description;
        MisionScore.transform.GetComponent<Text>().text = MisionFunction.Score;
        Stars1UI.sprite = StarSprites[StarNumber(int.Parse(MisionFunction.Score), MisionFunction.Description)];
        if (MisionFunction.Escenario != -1)
        {
            SetReplayBtn(MisionFunction.Escenario);
        }
    }

    public void SetReplayBtn(int index)
    {
        btnReplay.gameObject.SetActive(true);
        btnReplay.onClick.AddListener(delegate { JugarMisionIndex(index); });
    }

    public int StarNumber(int points, string Descr)
    {
        int Escenario = 0;

        for (int i = 0; i < NUMMISIONS; i++)
        {
            // misionsArr[i].Achieved = GameMind.getAchivement(i);
            if (misionsArr[i].Description == Descr || misionsArr[i].DescriptionMala == Descr)
            {
                Escenario = i+1;
            }
        }

        int n = 0;
        int Maximo;
        double newPoints = 0;

        switch (Escenario)
        {
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
        Debug.Log("Escenario " + Escenario);
        Debug.Log("points " + points);
        Debug.Log("maximo " + Maximo);
        newPoints = (double)points / (double)Maximo;
        Debug.Log(newPoints);

        if (newPoints <= 0)
        {
            n = 0;
        }
        else if (0 < newPoints && newPoints <= .2)
        {
            n = 1;
        }
        else if (.2 < newPoints && newPoints <= .4)
        {
            n = 2;
        }
        else if (.4 < newPoints && newPoints <= .6)
        {
            n = 3;
        }
        else if (.6 < newPoints && newPoints <= .8)
        {
            n = 4;
        }
        else if (.8 < newPoints && newPoints <= 1)
        {
            n = 5;
        }
        return n;
    }

    IEnumerator EsperarMin(int Escenario)
    {
        //Print the time of when the function is first called.
        //Debug.Log("Started Coroutine at timestamp : " + Time.time);

        //yield on a new YieldInstruction that waits for 5 seconds.

        //ProximaMission.SetActive(true);


        yield return new WaitForSeconds(0);

        switch (Escenario)
        {
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


    public void JugarMisionIndex(int index)
    {

        StartCoroutine(EsperarMin(index));

        GlobalVariables.Caso = index;
        HelpManager.ExisteAyuda(index.ToString());
        /* TODO: Add animation to start of selection of case
        switch (index)
        {
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
        */

    }



    public void GoToMainMenu()
    {
        GlobalVariables.lives = 5;
        // TODO : Cuando tengamos la escena del menu
        GlobalVariables.score = 0;
        GlobalVariables.pairAnswerSlot.Clear();
        GlobalVariables.items.Clear();
        SceneManager.LoadScene("Menu"); //TODO: Add an option to go back
    }

}
