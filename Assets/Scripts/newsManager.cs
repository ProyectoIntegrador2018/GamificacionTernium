using FirebaseWebGL.Examples.Utils;
using FirebaseWebGL.Scripts.FirebaseBridge;
using FirebaseWebGL.Scripts.Objects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Text;  
using System.Text.RegularExpressions;  
using Newtonsoft.Json;

public class newsManager : MonoBehaviour
{
    public GameObject newsPrefab;
    private Text[] textArray;   
    static Root myDeserializedClass;
    static private List<News> newsList;

    // Start is called before the first frame update
    void Start()
    {
        if (Application.platform == RuntimePlatform.WebGLPlayer){
           GetEvents();
            // Long string, like the one when calling Firebase 
            //string content = "{\"new0\" : {\"titulo\": \"¡Evento de bienvenida!\",\"descripcion\": \"Ven al edificio A el 25/Ago/2020 y da inicio a un excelente semestre\",\"fecha\": \"20/08/2020 12:00 am\"},\"new1\" : {\"titulo\": \"¡Evento de doble experiencia!\",\"descripcion\": \"Gana el doble de experiencia del 20/08/2020 al 22/08/2020\",\"fecha\": \"19/08/2020 12:00 am\"},\"new2\" : {\"titulo\": \"Sesión de mantenimiento\",\"descripcion\": \"Esta es una descripcion, tal vez\",\"fecha\": \"21/08/2020 12:00 am\"},\"new3\" : {\"titulo\": \"Aviso de actualización\",\"descripcion\": \"Probablemente esta es una descripcion\",\"fecha\": \"26/08/2020 12:00 am\"},\"new4\": {\"titulo\": \"Aviso para turno matutino\",\"descripcion\": \"Podría ser una descripción\",\"fecha\": \"27/08/2020 12:00 am\"}}";
        }  
        else{
            newsList = ToastManager.getNews();
            createNewsList();
        }
        
    }

    public void GetEvents() =>
            FirebaseDatabase.GetEvents(gameObject.name, "UpdateEventList", "DisplayErrorObject");

    public void UpdateEventList(string data)
        {
            //outputText.color = outputText.color == Color.green ? Color.blue : Color.green;
            //outputText.text = data;
            //Debug.Log(data);

            // Create a pattern for a word that starts with letter "M"  
            string pattern = @"\{([^\}]+)\}";  
            // Create a Regex  
            Regex rg = new Regex(pattern);

            data = "[" + data.Substring(1, data.Length - 2) + "]";
            // Get all matches  
            MatchCollection matchedChunks = rg.Matches(data);  
            // Print all matched authors  
            string formateada = "{ \"news\" : [";
            for (int count = 0; count < matchedChunks.Count; count++){
                formateada += matchedChunks[count].Value;
                if (!(count + 1 >= matchedChunks.Count)){
                    formateada += ",";
                }
            }
            formateada += "]}";
            
            //Debug.Log(formateada);
            
            myDeserializedClass = JsonConvert.DeserializeObject<Root>(formateada);
            newsList = myDeserializedClass.news;
            createNewsList();
            
            //foreach(News item in myDeserializedClass.news){
            //    print(item.titulo);
            //}  
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

    public void returnToMenu(){
        SceneManager.LoadScene("Menu");
    }

    public void DisplayErrorObject(string error)
        {
            var parsedError = StringSerializationAPI.Deserialize(typeof(FirebaseError), error) as FirebaseError;
            DisplayError(parsedError.message);
        }
    
    public void DisplayError(string error)
        {
            Debug.LogError(error);
        }

    // Update is called once per frame
    void Update()
    {
        
    }
}
