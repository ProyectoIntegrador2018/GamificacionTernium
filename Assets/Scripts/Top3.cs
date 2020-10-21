using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using System.Linq;

public class Turn{

    public string name {get; set;}
    public int score {get; set;}
}

public class Top3 : MonoBehaviour
{
    static private User[] users;
    int morningTurn, noonTurn, nightTurn;
    public Image turnImage;
    public Sprite morningImg;
    public Sprite noonImg;
    public Sprite nightImg;
    public TMP_Text turnText;

    /*
    // Update is called once per frame
    void Update()
    {
        
    }*/

    
    public static int getMorningScore(){
        int morningTurn = 0;
        foreach (User user in users) {
            if(user.turno == "Matutino"){
                for(int i = 0; i < user.niveles.Length; i++){
                    //Debug.Log(user.niveles[i]);
                    morningTurn += user.niveles[i];
                }
            }
        }
        return morningTurn;
    }

    public static int getNoonScore(){
        int noonTurn = 0;
        foreach (User user in users) {
            if(user.turno == "Vespertino"){
                for(int i = 0; i < user.niveles.Length; i++){
                    noonTurn += user.niveles[i];
                }
            }
        }
        return noonTurn;
    } 

    public static int getNightScore(){
        int nightTurn = 0;
        foreach (User user in users) {
            if(user.turno == "Nocturno"){
                for(int i = 0; i < user.niveles.Length; i++){
                    nightTurn += user.niveles[i];
                }
            }
        }
        return nightTurn;
    }

    public void showScores(){
        int morningScore = getMorningScore();
        int noonScore =  getNoonScore();
        int nightScore = getNightScore();

        var scoreList = new List<Turn>(){
            new Turn(){name = "Matutino", score = morningScore},
            new Turn(){name= "Vespertino", score = noonScore},
            new Turn(){name = "Nocturno", score = nightScore}
        };

        int maxScore = scoreList.Max(s => s.score);

        var best = from s in scoreList
            where s.score == maxScore
            select s;

        foreach(var scr in best){
            turnText.text = scr.name;
            if(scr.name == "Matutino"){
                turnImage.sprite = morningImg;
            }
            else if(scr.name == "Vespertino"){
                turnImage.sprite = noonImg;
            }
            else if(scr.name == "Nocturno"){
                turnImage.sprite = nightImg;
            }
        }

        if (!Database.isAdmin(GlobalVariables.usernameId)) {
            showTurn();
        }
        else {
            turnImage.enabled = false;
            turnText.enabled = false;
        }
            
    }

    


    // Start is called before the first frame update
    void Start()
    {
        users = Database.GetUsers();
        showScores();
    }
    


}
