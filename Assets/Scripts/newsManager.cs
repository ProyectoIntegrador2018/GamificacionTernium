using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class newsManager : MonoBehaviour
{
    public itemBehavior objScript;
    public GameObject newsPrefab;
    public GameObject newsAdminPrefab;
    public GameObject crearBtn;
    private Text[] textArray;   
    private NewsItem[] news;
    public static string path;

    // Start is called before the first frame update
    void Start()
    {
        path = Application.persistentDataPath + "/events.json";
        //news = ToastManager.getNews();
        var myTextAsset = File.ReadAllText(Application.persistentDataPath + "/events.json"); 
            
        News deserealized = JsonUtility.FromJson<News>(myTextAsset);
        news = deserealized.newsList;
        
        

        if (!Database.isAdmin(GlobalVariables.usernameId)) {
            crearBtn.SetActive(false);
            foreach(NewsItem newsItem in news){
            createNews(newsItem.titulo, newsItem.descripcion, newsItem.fecha);
            }
        }
        else {
            crearBtn.SetActive(true);
            foreach(NewsItem newsItem in news){
            createNewsAdmin(newsItem.titulo, newsItem.descripcion, newsItem.fecha, newsItem.id);
            }
        }
    }

    void createNews(string title, string descr, string date){
        GameObject newsObject;
        newsObject = (GameObject)Instantiate(newsPrefab);
        newsObject.transform.SetParent(GameObject.FindWithTag("newsList").transform);
        newsObject.transform.localScale = new Vector3((float)0.82, (float)1.006147, 1);
        textArray = newsObject.GetComponentsInChildren<Text>();
        textArray[0].text = title;
        textArray[1].text = descr;
        textArray[2].text = date;
    }

    void createNewsAdmin(string title, string descr, string date, int id){
        GameObject newsObject;
        newsObject = (GameObject)Instantiate(newsAdminPrefab);
        newsObject.transform.SetParent(GameObject.FindWithTag("newsList").transform);
        newsObject.transform.localScale = new Vector3((float)0.73, (float)1.006147, 1);
        objScript = newsObject.GetComponentInChildren<itemBehavior>();
        objScript.setID(id);
        textArray = newsObject.GetComponentsInChildren<Text>();
        textArray[0].text = title;
        textArray[1].text = descr;
        textArray[2].text = date;
    }

    public void returnToMainMenu(){
        SceneManager.LoadScene("Menu");
    }

    public void goToCreateNews(){
        SceneManager.LoadScene("createNews");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
