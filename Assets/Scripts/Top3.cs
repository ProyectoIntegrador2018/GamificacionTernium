using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using System.Linq;

public class Team{

    public string name {get; set;}
    public int score {get; set;}
}

public class Top3 : MonoBehaviour
{
    static private User[] users;
    public Image teamImage;
    public Sprite greenTeam;
    public Sprite pinkTeam;
    public Sprite blueTeam;
    public TMP_Text teamText;

    /*
    // Update is called once per frame
    void Update()
    {
        
    }*/

    
    public static List<int> getScores(){
        int greenScore = 0;
        int pinkScore = 0;
        int blueScore = 0;
        foreach (User user in users) {
            if(user.equipo == "verde"){
                for(int i = 0; i < user.niveles.Length; i++){
                    //Debug.Log(user.niveles[i]);
                    greenScore += user.niveles[i];
                }
            }
            if(user.equipo == "rosa"){
                for(int i = 0; i < user.niveles.Length; i++){
                    //Debug.Log(user.niveles[i]);
                    pinkScore += user.niveles[i];
                }
            }
            if(user.equipo == "azul"){
                for(int i = 0; i < user.niveles.Length; i++){
                    //Debug.Log(user.niveles[i]);
                    blueScore += user.niveles[i];
                }
            }
        }
        List<int> colorList = new List<int>();
        colorList.Add(greenScore);
        colorList.Add(pinkScore);
        colorList.Add(blueScore);
        return colorList;
    }

    public void showScores(){
        List<int> sreList = getScores();

        var scoreList = new List<Team>(){
            new Team(){name = "Verde", score = sreList[0]},
            new Team(){name= "Rosa", score = sreList[1]},
            new Team(){name = "Azul", score = sreList[2]}
        };

        int maxScore = scoreList.Max(s => s.score);

        var best = from s in scoreList
            where s.score == maxScore
            select s;

        foreach(var scr in best){
            teamText.text = scr.name;
            if(scr.name == "Rosa"){
                teamImage.sprite = pinkTeam;
            }
            else if(scr.name == "Verde"){
                teamImage.sprite = greenTeam;
            }
            else if(scr.name == "Azul"){
                teamImage.sprite = blueTeam;
            }
        }
            
    }

    


    // Start is called before the first frame update
    void Start()
    {
        users = Database.GetNonAdminUsers();
        showScores();
    }
    


}
