using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq; //Esta libreria es muy util
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement; //Necesaria para cambiar de escena
using UnityEngine.UI; //Necesaria para poner UI
using TMPro; //Da mas opciones de edicion al texto normal y sale con mejor resolucion

// Este script sirve para crear una nuevo evento dados un titulo y una descripcion
// La fecha la toma automaticamente del sistema y se la asigna al objeto
// Tambien se encarga de mostrar el aviso de que no puede crearse un evento vacio
// Esta pantalla solo esta disponible para el admin
// Creado por el equipo 2

public class newsCreator : MonoBehaviour
{
    public TMP_InputField titleInput;
    public TMP_InputField descrInput;
    public GameObject succMess;
    public GameObject warnMess;
    public Button createButton;
    private static News news;
    public static string path;

    // Start is called before the first frame update
    void Start()
    {
        // Se puede comentar esta linea
        path = Application.persistentDataPath + "/events.json";
    }

    public void createEvent(){
        //Obtener fecha del sistema
        string dateString = DateTime.Now.ToString("g");

        // Si el titulo esta vacio mostrar el cuadro de aviso
        if(checkInfo(titleInput.text)){
            // Mostrar el cuadro de aviso
            warnMess.SetActive(true);
            // Empezar corrutina para el cuadro de aviso
            StartCoroutine(RemoveAfterSeconds(2, warnMess));
        }
        else{
            // Creando un objeto de la clase NewsItem
            NewsItem newsObject = new NewsItem();
            // Obteniendo la lista actual de eventos
            news = ToastManager.getNews();
            // Generando id con una funcion pseudorandom de un rango considerable para bajar las probs de error al borrar
            newsObject.id = UnityEngine.Random.Range(0, 10000);
            // Obteniendo info de text fields
            newsObject.titulo = titleInput.text;
            newsObject.descripcion = descrInput.text;
            newsObject.fecha = dateString;
            // Agregando el nuevo objeto evento
            news.Push(newsObject);
            // Mandando la lista actualizada para que se actualice en el manager
            ToastManager.updateNews(news);
            //Limpiando inputs
            titleInput.text = "";
            descrInput.text = "";
            // Dando feedback al usuario
            succMess.SetActive(true);
            // Evitando errores haciendo inactivo el boton
            createButton.interactable = false;
            // Iniciar una corrutina para borrar el mensaje de exito despues de 2 segundos
            StartCoroutine(RemoveAfterSeconds(2, succMess));
        }
    }

    // Revisando si el texto del titulo no es vacio
    bool checkInfo(string inputText){
        if(inputText == ""){
            return true;
        }
        return false;
    }

    // Funcion para contabilizar 2 segundos y luego ejecutar el resto del codigo
    // Borra el objeto que se le manda como segundo argumento
    IEnumerator RemoveAfterSeconds(int seconds, GameObject obj)
        {
                yield return new WaitForSeconds(seconds);
                obj.SetActive(false);
                // Evitando errores haciendo inactivo el boton
                createButton.interactable = true;
        }

    //Funcion que esta en el boton de atras para regresar a la lista de eventos
    public void returnToNews(){
        SceneManager.LoadScene("News");
    }

}
