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

    public static Mision[] misionsArr = new Mision[NUMMISIONS] {
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

    public void CreateMision(int id, string category, string title, string description, string descripcionMala, bool achived, int score) {

        GameObject mision;
        //Render either a resolved or not resolved (black Achivement)
        if (achived) {
            mision = (GameObject)Instantiate(MisionPrefab);
        }
        else {
            mision = (GameObject)Instantiate(MisionPrefabLose);
        }


        SetMisionInfo(id, category, mision, title, description, descripcionMala, achived, score);
    }
    public void SetMisionInfo(int id, string category, GameObject mision, string title, string description, string descripcionMala, bool achieved, int score) {
        mision.transform.SetParent(GameObject.Find(category).transform);
        //Transformation values chose by hand / experimentation
        mision.transform.localScale = new Vector3((float)0.73, (float)1.006147, 1);
        mision.transform.GetChild(1).GetComponent<Text>().text = title;
        if (achieved) {
            mision.transform.GetChild(2).GetComponent<Text>().text = description;
        }
        else {
            mision.transform.GetChild(2).GetComponent<Text>().text = descripcionMala;
        }

        mision.transform.GetChild(3).GetComponent<Text>().text = score.ToString();
        mision.GetComponent<Button>().onClick.AddListener(() => {
            HistorialEventSystem.current.MissionClick(description, score, id);
        });

    }

    // Start is called before the first frame update
    void Start(){
        for (int i = 0; i <NUMMISIONS; i++)
        {
            // misionsArr[i].Achieved = GameMind.getAchivement(i);
            if (Database.getStarted(i))
            {
                CreateMision(
                    i + 1, 
                    "Mision Container", 
                    misionsArr[i].Title, 
                    misionsArr[i].Description, 
                    misionsArr[i].DescriptionMala, 
                    misionsArr[i].Achieved = Database.getStarted(i), 
                    misionsArr[i].Score = Database.getScore(i)
                );
            }
        }
    }

    public static void AchiveMision(int index)
    {
        misionsArr[index].Achieved = true;
    }

}
