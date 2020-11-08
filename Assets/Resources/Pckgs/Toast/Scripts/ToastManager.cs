using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.IO;
using UnityEngine;

[System.Serializable]
public class NewsItem{
    public int id;
    public string titulo;
    public string descripcion;
    public string fecha;
}

[System.Serializable]
public class News{
    public NewsItem[] newsList;

    public void Push(NewsItem x) {
        int len = newsList.Length;
        NewsItem[] auxList = new NewsItem[len+1];
        for(int i=0; i<len;i++){
            auxList[i] = newsList[i];
        }
        auxList[len] = x;
        newsList = auxList;
    }

    public void Delete(int index){
        var copiedList = new List<NewsItem>(newsList);
        copiedList.RemoveAt(index);
        newsList = copiedList.ToArray();
    }

    public void Patch(int index, NewsItem y){
        newsList[index] = y;
    }
}

public class ToastManager : MonoBehaviour
{
    public TextAsset newsText;
    public static News news;
    int i = 0;
    public static string path;

    void Start(){
        path = Application.persistentDataPath + "/events.json";
        if (File.Exists(path)) {
            var myTextAsset = File.ReadAllText(Application.persistentDataPath + "/events.json"); 
            
            news = JsonUtility.FromJson<News>(myTextAsset);
        }
        else
        {
            news = JsonUtility.FromJson<News>(newsText.text);
            foreach(NewsItem item in news.newsList){
                Debug.Log(item.titulo);
            }
            //Si no existe se crea en local para siempre accesar desde el path 
            updateNews(news);
        }
        // Repetir un procedimiento
        // Params: Nombre de la funcion, iniciar a los x segundos, repetir cada x segundos 
        InvokeRepeating("makeToast", 2.0f, 4.0f);
    }

    void makeToast(){
        if(i >= news.newsList.Length){
            i = 0;
            Toast.Instance.Show(news.newsList[i].titulo, 3.0f);
        }
        else{
            Toast.Instance.Show(news.newsList[i].titulo, 3.0f);
        }
        i++;        
    }

    public static void updateNews(News param){
        string jsonData = JsonUtility.ToJson (param, true);
        File.WriteAllText(path, jsonData);
    }

    public static News getNews(){
        return news;
    }
}
