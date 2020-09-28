using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Top3 : MonoBehaviour
{
    public Button activate;

    // Start is called before the first frame update
    void Start()
    {
        activate.onClick.AddListener(delegate {GameMind.getMScore();});
    }
    /*
    // Update is called once per frame
    void Update()
    {
        
    }*/

    public static void showScores(){
        /*SortedList<int,string> turnScores = new SortedList<int,string>()
                                    {
                                        {Database.getMorningScore(), "matutino"},
                                        {Database.getNoonScore(), "vespertino"},
                                        {Database.getNightScore(), "nocturno"}
                                    };

        foreach(KeyValuePair<int, string> kvp in turnScores)
			Debug.Log(kvp.Key + " " + kvp.Value );
            */
    }

    


}
