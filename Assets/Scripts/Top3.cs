using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Top3 : MonoBehaviour
{
    static private User[] users;
    int morningTurn, noonTurn, nightTurn;
    public Text firstText;
    public Text secondText;
    public Text thirdText;
    public Image turnImage;
    public Sprite morningImg;
    public Sprite noonImg;
    public Sprite nightImg;
    public Text turnText;

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

        SortedList<int,string> turnScores = new SortedList<int,string>()
                                    {
                                        {morningScore, "Matutino"},
                                        {noonScore, "Vespertino"},
                                        {nightScore, "Nocturno"}
                                    };
        foreach(KeyValuePair<int, string> kvp in turnScores){
            if(thirdText.text == "Turno"){
                thirdText.text = kvp.Value;
            }
            else if(secondText.text == "Turno"){
                secondText.text = kvp.Value;
            }
            else if(firstText.text == "Turno"){
                firstText.text = kvp.Value;
            }
        }

        showTurn();
            
    }

    public void showTurn(){
        if(GlobalVariables.turno == "Matutino"){
            turnImage.sprite = morningImg;
            turnText.text = "Matutino";
        }
        else if(GlobalVariables.turno == "Vespertino"){
            turnImage.sprite = noonImg;
            turnText.text = "Vespertino";
        }
        else if(GlobalVariables.turno == "Nocturno"){
            turnImage.sprite = nightImg;
            turnText.text = "Nocturno";
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        users = Database.GetUsers();
        showScores();
    }
    


}
