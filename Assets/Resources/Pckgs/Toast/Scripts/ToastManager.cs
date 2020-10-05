using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
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
}

public class ToastManager : MonoBehaviour
{
    public TextAsset newsText;
    public static News news;
    int i = 0;

    void Start(){
        // Repetir un procedimiento
        // Params: Nombre de la funcion, iniciar a los x segundos, repetir cada x segundos
        news = JsonUtility.FromJson<News>(newsText.text);  
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

    public static NewsItem[] getNews(){
        return news.newsList;
    }
}
