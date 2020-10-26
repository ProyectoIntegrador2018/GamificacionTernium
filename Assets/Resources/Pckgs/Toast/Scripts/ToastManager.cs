using FirebaseWebGL.Examples.Utils;
using FirebaseWebGL.Scripts.FirebaseBridge;
using FirebaseWebGL.Scripts.Objects;
using System;  
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Text;  
using System.Text.RegularExpressions;  
using UnityEngine;
using Newtonsoft.Json;

 public class News    {
        public string titulo { get; set; } 
        public string descripcion { get; set; } 
        public string fecha { get; set; } 
    }

    public class Root    {
        public List<News> news { get; set; } 
    }


public class ToastManager : MonoBehaviour
{
    public TextAsset newsFile;
    static Root myDeserializedClass;
    int i = 0;

    void Start(){
        if (Application.isEditor){
            string fileContents = newsFile.ToString();
            myDeserializedClass = JsonConvert.DeserializeObject<Root>(fileContents);
            InvokeRepeating("makeToast", 2.0f, 4.0f);
        }  
        else{
            GetEvents();
            // Long string, like the one when calling Firebase 
            //string content = "{\"new0\" : {\"titulo\": \"¡Evento de bienvenida!\",\"descripcion\": \"Ven al edificio A el 25/Ago/2020 y da inicio a un excelente semestre\",\"fecha\": \"20/08/2020 12:00 am\"},\"new1\" : {\"titulo\": \"¡Evento de doble experiencia!\",\"descripcion\": \"Gana el doble de experiencia del 20/08/2020 al 22/08/2020\",\"fecha\": \"19/08/2020 12:00 am\"},\"new2\" : {\"titulo\": \"Sesión de mantenimiento\",\"descripcion\": \"Esta es una descripcion, tal vez\",\"fecha\": \"21/08/2020 12:00 am\"},\"new3\" : {\"titulo\": \"Aviso de actualización\",\"descripcion\": \"Probablemente esta es una descripcion\",\"fecha\": \"26/08/2020 12:00 am\"},\"new4\": {\"titulo\": \"Aviso para turno matutino\",\"descripcion\": \"Podría ser una descripción\",\"fecha\": \"27/08/2020 12:00 am\"}}";
        }
        
        
    }

    public void GetEvents() =>
            FirebaseDatabase.GetEvents(gameObject.name, "UseData", "DisplayErrorObject");

    
    public void UseData(string data)
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
            
            
            InvokeRepeating("makeToast", 2.0f, 4.0f);
            //foreach(News item in myDeserializedClass.news){
            //    print(item.titulo);
            //}  
        }

        // Params: Nombre de la funcion, iniciar a los x segundos, repetir cada x segundos
    void makeToast(){
        if(i >= myDeserializedClass.news.Count){
            i = 0;
            Toast.Instance.Show(myDeserializedClass.news[i].titulo, 3.0f);
        }
        else{
            
            Toast.Instance.Show(myDeserializedClass.news[i].titulo, 3.0f);
        }
        i++;      
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



    public static List<News> getNews(){
        return myDeserializedClass.news;
    }
}
