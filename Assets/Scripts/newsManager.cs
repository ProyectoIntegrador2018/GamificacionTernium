using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

// Script para mostrar la lista de eventos que estan en la base de datos local
// Creada por el equipo 2

public class newsManager : MonoBehaviour
{
    public itemBehavior objScript;
    public GameObject newsPrefab;
    public GameObject newsAdminPrefab;
    public GameObject crearBtn;
    private Text[] textArray;   
    private News newsStruct;
    private NewsItem[] news;
    public static string path;

    // Start is called before the first frame update
    void Start()
    {
        // Direccion para guardar el archivo json de los eventos
        path = Application.persistentDataPath + "/events.json";
        //news = ToastManager.getNews(); -----> Este no se puede usar porque no esta actualizado
        // Tal vez convendria usar una funcion que cargue los eventos, y llamarla en el mismo ToastManager, aqui y en el newsCreator
        // Queda pendiente lo de arriba
        // Funciona asi, pero sería una optimizacion
        var myTextAsset = File.ReadAllText(Application.persistentDataPath + "/events.json"); 

        // Deserializando, investigar sobre JsonUtility, viene por Default con Unity    
        News deserealized = JsonUtility.FromJson<News>(myTextAsset);
        // Extrayendo la lista de acuerdo a la estructura de datos
        news = deserealized.newsList;

        // Haciendo cambios a la UI si usuario es admin o no
        // No admin
        if (!Database.isAdmin(GlobalVariables.usernameId)) {
            crearBtn.SetActive(false);
            // Por cada registro de evento que exista, correr esta funcion que crea un prefab y lo suma a la lista
            foreach(NewsItem newsItem in news){
                createNews(newsItem.titulo, newsItem.descripcion, newsItem.fecha);
            }
        }
        // Admin
        else {
            crearBtn.SetActive(true);
             // Por cada registro de evento que exista, correr esta funcion que crea un prefab y lo suma a la lista
            foreach(NewsItem newsItem in news){
                createNewsAdmin(newsItem.titulo, newsItem.descripcion, newsItem.fecha, newsItem.id);
            }
        }
    }

    void createNews(string title, string descr, string date){
        GameObject newsObject;
        // Instanciar el prefab de un item de evento, se llama "newsItem"
        // Esta en Resources/Prefabs
        newsObject = (GameObject)Instantiate(newsPrefab);
        // Agregar este objeto debajo de la jerarquia del objeto de scrollList
        newsObject.transform.SetParent(GameObject.FindWithTag("newsList").transform);
        // Posicionar el objeto anterior, tomando como referencia el transform del parent, prueba y error :c
        newsObject.transform.localScale = new Vector3((float)0.82, (float)1.006147, 1);
        // Obtener todos los objetos dentro de la jerarquia que son text
        textArray = newsObject.GetComponentsInChildren<Text>();
        // Asignar el valor a cada uno
        textArray[0].text = title;
        textArray[1].text = descr;
        textArray[2].text = date;
    }

    void createNewsAdmin(string title, string descr, string date, int id){
        GameObject newsObject;
        // Instanciar el prefab de un item de evento, se llama "adminnewsItem"
        // Esta en Resources/Prefabs
        newsObject = (GameObject)Instantiate(newsAdminPrefab);
        // Agregar este objeto debajo de la jerarquia del objeto de scrollList
        newsObject.transform.SetParent(GameObject.FindWithTag("newsList").transform);
        // Posicionar el objeto anterior, tomando como referencia el transform del parent, prueba y error :c
        newsObject.transform.localScale = new Vector3((float)0.73, (float)1.006147, 1);
        // Obteniendo referencia al script que ya incluye el prefab para variables de cada uno
        objScript = newsObject.GetComponentInChildren<itemBehavior>();
        // Asignar id al objeto creado, ya viene desde la DB
        objScript.setID(id);
        // Obtener todos los objetos dentro de la jerarquia que son text
        textArray = newsObject.GetComponentsInChildren<Text>();
        // Asignar el valor a cada uno
        textArray[0].text = title;
        textArray[1].text = descr;
        textArray[2].text = date;
    }

    // Funcion para regresar al menu principal
    public void returnToMainMenu(){
        SceneManager.LoadScene("Menu");
    }

    // Funcion para ir a la pantalla para crear eventos, solo disponible para el admin
    public void goToCreateNews(){
        SceneManager.LoadScene("createNews");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
