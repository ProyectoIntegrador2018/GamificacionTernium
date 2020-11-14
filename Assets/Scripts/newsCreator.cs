using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class newsCreator : MonoBehaviour
{
    public TMP_InputField titleInput;
    public TMP_InputField descrInput;
    public GameObject succMess;
    public Button createButton;
    private static News news;
    public static string path;

    // Start is called before the first frame update
    void Start()
    {
        path = Application.persistentDataPath + "/events.json";
    }

    public void createEvent(){
        string dateString = DateTime.Now.ToString("g");

        NewsItem newsObject = new NewsItem();
        news = ToastManager.getNews();
        newsObject.id = UnityEngine.Random.Range(0, 50);
        newsObject.titulo = titleInput.text;
        newsObject.descripcion = descrInput.text;
        newsObject.fecha = dateString;
        
        news.Push(newsObject);
        ToastManager.updateNews(news);
        titleInput.text = "";
        descrInput.text = "";
        succMess.SetActive(true);
        createButton.interactable = false;
        StartCoroutine(RemoveAfterSeconds(2, succMess));
    }

    IEnumerator RemoveAfterSeconds(int seconds, GameObject obj)
        {
                yield return new WaitForSeconds(seconds);
                obj.SetActive(false);
                createButton.interactable = true;
        }

    public void returnToNews(){
        SceneManager.LoadScene("News");
    }

}
