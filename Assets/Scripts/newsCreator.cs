using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using TMPro;

public class newsCreator : MonoBehaviour
{
    public TMP_InputField titleInput;
    public TMP_InputField descrInput;
    public GameObject succMess;
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
        newsObject.titulo = titleInput.text;
        newsObject.descripcion = descrInput.text;
        newsObject.fecha = dateString;
        news = ToastManager.getNews();
        news.Push(newsObject);
        ToastManager.updateNews(news);
        titleInput.text = "";
        descrInput.text = "";
        succMess.SetActive(true);
        StartCoroutine(RemoveAfterSeconds(2, succMess));
    }

    IEnumerator RemoveAfterSeconds(int seconds, GameObject obj)
        {
                yield return new WaitForSeconds(seconds);
                obj.SetActive(false);
        }

    public void returnToNews(){
        SceneManager.LoadScene("News");
    }

}
