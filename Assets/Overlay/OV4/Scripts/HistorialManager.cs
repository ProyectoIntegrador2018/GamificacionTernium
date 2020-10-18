using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HistorialManager : MonoBehaviour
{

    public int numMissions;

    public GameObject MisionPrefab;
    public GameObject MisionPrefabLose;

    public Button btnReplay;

    public static Mision[] misionsArr;

    private string[] missionNames;
    private string[] missionDescriptions; 
    

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

        numMissions = Database.getAmountOfMissions();
        missionNames = Database.getAllMissionNames();
        missionDescriptions = Database.getAllMissionDescriptions();
        misionsArr = new Mision[numMissions];

        for(int i = 0; i < numMissions; i++) {
            misionsArr[i] = new Mision(missionNames[i], missionDescriptions[i], "???", false, 0);
        }

        for (int i = 0; i < numMissions; i++)
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
