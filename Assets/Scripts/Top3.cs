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
    //Necesita refactor
    //public Image teamImage;
    //public Sprite greenTeam;
    //public Sprite pinkTeam;
    //public Sprite blueTeam;
    public TMP_Text teamText;
    List<Team> teamList = new List<Team>();

    /*
    // Update is called once per frame
    void Update()
    {
        
    }*/

    
    public void getScores(){
        List<User> aux = users.ToList();
        var groupedResult = from s in aux
                            group s by s.equipo;

        foreach (var teamGroup in groupedResult)
        {
            var currGroup = teamGroup.Key; //Each group has a key 
            var currSum = 0;
            foreach(User u in teamGroup){ // Each group has inner collection
                currSum += u.niveles.Sum();
            }
            teamList.Add(new Team {name = currGroup, score= currSum});
        }
        /*foreach(Team t in teamList){
            Debug.Log(t.name + " : " + t.score);
        }*/
    }

    public void showScores(){

        getScores();

        int maxScore = teamList.Max(s => s.score);

        var best = from s in teamList
            where s.score == maxScore
            select s;
        
        foreach(var scr in best){
            teamText.text = scr.name;
        }
          
    }

    


    // Start is called before the first frame update
    void Start()
    {
        users = Database.GetNonAdminUsers();
        showScores();
    }
    


}
