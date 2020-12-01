using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.IO;
using UnityEngine;
using System.Linq;

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

    public void Delete(int id){
        var copiedList = new List<NewsItem>(newsList);
        //copiedList.RemoveAt(index);
        copiedList.RemoveAll(s => s.id == id);
        newsList = copiedList.ToArray();
    }

    public void Patch(int index, NewsItem y){
        newsList[index] = y;
    }

    public int Size(){
        return newsList.Length;
    }
}

// Clase para manejar los pop ups de noticias de la pantalla de menu
// Creada por el equipo 2
// Basada en el paquete de Toast, documentacion en la carpeta de Resources/Pckgs/Toast

public class ToastManager : MonoBehaviour
{
    public TextAsset newsText;
    public static News news;
    int i = 0;
    public static string path;

    void Start(){
        // Para obtener la info de eventos de la DB
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

    // Funcion para mostrar el pop up
    void makeToast(){ 
        //Debug.Log(news.newsList.Length);
        if (news.newsList == null || news.newsList.Length == 0){
            CancelInvoke("makeToast");
        }
        else{
            if(i >= news.newsList.Length){
                i = 0;
                Toast.Instance.Show(news.newsList[i].titulo, 3.0f);
            }
            else{
                Toast.Instance.Show(news.newsList[i].titulo, 3.0f);
            }
            i++; 
        }
    }

    // Funcion para guardar un nuevo text file de los eventos, hacer persistentes los cambios

    public static void updateNews(News param){
        string jsonData = JsonUtility.ToJson (param, true);
        File.WriteAllText(path, jsonData);
    }

    public static News getNews(){
        return news;
    }
}
