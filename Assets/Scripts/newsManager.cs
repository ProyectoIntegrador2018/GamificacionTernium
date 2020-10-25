using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class newsManager : MonoBehaviour
{
    public GameObject newsPrefab;
    private Text[] textArray;   
    private static List<News> newsList;

    // Start is called before the first frame update
    void Start()
    {
        newsList = ToastManager.getNews();
        createNewsList();
    }

    void createNewsList(){
        foreach(News item in newsList){
            createNews(item.titulo, item.descripcion, item.fecha);
        }
    }

    void createNews(string title, string descr, string date){
        GameObject newsObject;
        newsObject = (GameObject)Instantiate(newsPrefab);
        newsObject.transform.SetParent(GameObject.FindWithTag("newsList").transform);
        newsObject.transform.localScale = new Vector3((float)0.73, (float)1.006147, 1);
        textArray = newsObject.GetComponentsInChildren<Text>();
        textArray[0].text = title;
        textArray[1].text = descr;
        textArray[2].text = date;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
