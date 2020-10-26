using FirebaseWebGL.Examples.Utils;
using FirebaseWebGL.Scripts.FirebaseBridge;
using FirebaseWebGL.Scripts.Objects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;  
using System.Text.RegularExpressions;  
using Newtonsoft.Json;

[System.Serializable]
public struct ListaMisiones
{
    public List<Misiones> misiones;
}

[System.Serializable]
public struct Misiones {
    public int id;
    public string nombre;
    public string descripcion;
    public int puntuacionMax;
    public List<Preguntas> preguntas;
}

[System.Serializable]
public struct Preguntas {
    public double id;
    public string tipo;
    public string pregunta;
    public string[] opciones;
    public int[] vidasPerdidas;
    public string correctoTexto;
    public string[] falloTexto;
    public int respuestaCorrecta;
    public int puntos;
    public string sigEscenaCorrecto;
    public string[] sigEscenasError;
}

public class User    {
        public int id { get; set; } 
        public string tipo { get; set; } 
        public string username { get; set; } 
        public string password { get; set; } 
        public bool tutorial { get; set; } 
        public string turno { get; set; } 
        public int? expMax { get; set; } 
        public int? expMin { get; set; } 
        public int? expCurrent { get; set; } 
        public string avatarImg { get; set; } 
        public int? nivelJugador { get; set; } 
        public List<int> niveles { get; set; } 
        public List<bool> achivements { get; set; } 
        public List<bool> started { get; set; } 
    }

public class Users
{
    //employees is case sensitive and must match the string "employees" in the JSON.
    public List<User> users {get; set; }
    
    /*public void Push(User x) {
        int len = users.Count;
        User[] newUsers = new User[len+1];
        for(int i=0; i<len;i++){
            newUsers[i] = users[i];
        }
        newUsers[len] = x;
        users = newUsers;
    }*/
}


public class Database : MonoBehaviour
{
    public TextAsset jsonFile;
    public TextAsset jsonMisiones;
    static Users userBase;
    public static ListaMisiones missionList;
    public static string path;
    

    // Start is called before the first frame update
    void Start()
    {
        if (Application.isEditor)
        {
            string jsonContents = jsonFile.ToString();
            //Debug.Log(jsonContents);
            userBase = JsonConvert.DeserializeObject<Users>(jsonContents);

            /*
            //Asumire que es este
            path = Application.persistentDataPath + "/database.json";
            //Debug.Log("Fabi Aqui");
            //Debug.Log(path);

            //Este estara mal


            //Debug.Log(path);
            if (File.Exists(path)) {
                var myTextAsset = File.ReadAllText(Application.persistentDataPath + "/database.json"); 
                //Debug.Log("hola" + myTextAsset);
                userBase = JsonConvert.DeserializeObject<Users>(myTextAsset);
                //userBase = JsonUtility.FromJson<Users>(myTextAsset);
            }
            else
            {
                userBase = JsonConvert.DeserializeObject<Users>(jsonContents);
                //userBase = JsonUtility.FromJson<Users>(jsonFile.text);
                //Si no existe se crea en local para siempre accesar desde el path 
                saveData();
            }*/

            //foreach(User user in userBase.users){
            //    print(user.id);
            //}  
        }
        else{
            GetUsers();
        }
        
        missionList = JsonUtility.FromJson<ListaMisiones>(jsonMisiones.text);

    }

    public void GetUsers() =>
            FirebaseDatabase.GetUsers(gameObject.name, "UseData", "DisplayErrorObject");

    public void UseData(string data)
    {
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
            
            userBase = JsonConvert.DeserializeObject<Users>(formateada);
    }

    public static bool isAdmin(int userId) {
        if (userBase.users[userId].tipo == "admin") {
            return true;
        }
        return false;
    }
    public static string getTextoCorrecto(int missionIndex, int questionIndex) {
        return missionList.misiones[missionIndex].preguntas[questionIndex].correctoTexto;
    }

    public static string[] getSigEscenasError(int missionIndex, int questionIndex) {
        return missionList.misiones[missionIndex].preguntas[questionIndex].sigEscenasError;
    }

    public static string getSigEscenaCorrecto(int missionIndex, int questionIndex) {
        return missionList.misiones[missionIndex].preguntas[questionIndex].sigEscenaCorrecto;
    }

    public static int getPuntosPregunta(int missionIndex, int questionIndex) {
        return missionList.misiones[missionIndex].preguntas[questionIndex].puntos;
    }

    public static int getOpcionCorrecta(int missionIndex, int questionIndex) {
        return missionList.misiones[missionIndex].preguntas[questionIndex].respuestaCorrecta;
    }

    public static string[] getFalloTexto(int missionIndex, int questionIndex) {
        return missionList.misiones[missionIndex].preguntas[questionIndex].falloTexto;
    }

    public static int[] getVidasPerdidas(int missionIndex, int questionIndex) {
        return missionList.misiones[missionIndex].preguntas[questionIndex].vidasPerdidas;
    }

    public static string getPregunta(int missionIndex, int questionIndex) {
        return missionList.misiones[missionIndex].preguntas[questionIndex].pregunta;
    }

    public static string[] getOpcionesPregunta(int missionIndex, int questionIndex) {
        return missionList.misiones[missionIndex].preguntas[questionIndex].opciones;
    }

    public static string[] getAllMissionNames() {
        int n = missionList.misiones.Count;
        string[] namesList = new string[n];

        for (int i = 0; i < n; i++) {
            namesList[i] = missionList.misiones[i].nombre;
        }

        return namesList;
    }

    public static string[] getAllMissionDescriptions() {
        int n = missionList.misiones.Count;
        string[] descriptionsList = new string[n];

        for (int i = 0; i < n; i++) {
            descriptionsList[i] = missionList.misiones[i].descripcion;
        }

        return descriptionsList;
    }

    public static string getMissionName(int index) {
        return missionList.misiones[index].nombre;
    }

    public static int getAmountOfMissions() {
        return missionList.misiones.Count;
    }

    public static int login(string username, string pass) {
        // Debug.Log("aver si encontramos algo " + userBase.users[1].username);
        foreach (User user in userBase.users) {
            if(user.username == username && user.password == pass) {
                // Debug.Log("aver si encontramos algo ");
                return user.id;
            }
        }
        return -1;
    }

    public static List<User> GetNonAdminUsers() {

        List<User> aux = new List<User>();

        for(int i = 0; i < userBase.users.Count; i++) {
            if (!isAdmin(i)) {
                aux.Add(userBase.users[i]);
            }
        }

        return aux;
    }

    public static List<User> ObtainUsers() {
        return userBase.users;
    }

    public static int[] getExpBarData() {
        if(userBase.users[GlobalVariables.usernameId].expMax.HasValue && userBase.users[GlobalVariables.usernameId].expMin.HasValue && userBase.users[GlobalVariables.usernameId].expCurrent.HasValue){
            int[] aux = {userBase.users[GlobalVariables.usernameId].expMax.Value, userBase.users[GlobalVariables.usernameId].expMin.Value, userBase.users[GlobalVariables.usernameId].expCurrent.Value};
            return aux;
        }
        else{
            int[] aux = {-1, -1, -1};
            return aux;
        }
        
    }

    public static void setExpBarData(int expMax, int expMin, int expCurrent) {
        userBase.users[GlobalVariables.usernameId].expMax = expMax;
        userBase.users[GlobalVariables.usernameId].expMin = expMin;
        userBase.users[GlobalVariables.usernameId].expCurrent = expCurrent;
    }

    public static void setAvatar(string avatarName)
    {
        userBase.users[GlobalVariables.usernameId].avatarImg = avatarName;
    }

    public static int getNivelJugador() {
        int level;
        if(userBase.users[GlobalVariables.usernameId].nivelJugador.HasValue){
            level = userBase.users[GlobalVariables.usernameId].nivelJugador.Value;
            return level;
        }
        else{
            return -1;
        }
    }

    public static void setNivelJugador(int nivelJugador) {
        userBase.users[GlobalVariables.usernameId].nivelJugador = nivelJugador;
    }

    // Check if the user has an achivmenet on "i" scene
    public static bool getAchivement(int i) {
        return userBase.users[GlobalVariables.usernameId].achivements[i];
    }
    
    public static void setAchivement(int i) {
        userBase.users[GlobalVariables.usernameId].achivements[i-1] = true;
    }

    // Checks if the user has STARTED the "i" scene
    public static bool getStarted(int i)
    {
        return userBase.users[GlobalVariables.usernameId].started[i];
    }

    public static void setStarted(int i)
    {
        //Debug.Log("1Numero out of range: " + i);
        userBase.users[GlobalVariables.usernameId].started[i-1] = true;
    }

    public static int getScore(int i) {
        //Debug.Log("2Numero out of range: " + i);
        return userBase.users[GlobalVariables.usernameId].niveles[i];
    }
    
    public static void setScore(int i, int s) {
        //Debug.Log("Set Score");
        //Debug.Log("Caso " + i + " Score " + s);
        //Debug.Log("Antes");
        //Debug.Log(userBase.users[GlobalVariables.usernameId].niveles[i - 1]);
        userBase.users[GlobalVariables.usernameId].niveles[i-1] = s;
        //Debug.Log("Despues");
        //Debug.Log(userBase.users[GlobalVariables.usernameId].niveles[i - 1]);
    }

    public static string getTurno(){
        return userBase.users[GlobalVariables.usernameId].turno; 
    }

    public static bool getTutorial() {
        return userBase.users[GlobalVariables.usernameId].tutorial;
    }

    public static void setTutorial() {
        userBase.users[GlobalVariables.usernameId].tutorial = false;
    }
    
    /*
    public static void makeUser(string name, string password) {
        foreach (User user in userBase.users) {
            if(user.username == name) {
                Debug.Log("Este usuario ya existe en la base de datos");
                return;
            }
        }
        createUser(name, password);
        Debug.Log("Usuario creado y guardado correctamente");

    }*/

    /*public static void createUser(string name, string password) {
        User nUser = new User();
        nUser.id = userBase.users[userBase.users.Count - 1].id + 1;
        nUser.username = name;
        nUser.password = password;
        nUser.tutorial = true;
        nUser.turno = "Matutino";
        nUser.expMax = 500;
        nUser.expMin = 0;
        nUser.avatarImg = "Default";
        nUser.expCurrent = 0;
        nUser.nivelJugador = 1;
        int[] niv = {0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
        nUser.niveles = niv;
        bool[] ach = {false, false, false, false, false, false, false, false, false, false, false };
        nUser.achivements = ach;
        nUser.started = ach;
        //userBase.Push(nUser);
    }*/

    public static string getAvatar()
    {
        return userBase.users[GlobalVariables.usernameId].avatarImg;
    }

    public static int getCurrentAchivements(){
        int current=0;
        for (int ach=0; ach<userBase.users[GlobalVariables.usernameId].achivements.Count; ach++) {
            if(userBase.users[GlobalVariables.usernameId].achivements[ach]){
                current++;
            }
        }
        return current;
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

    public static void saveData(){
        string jsonData = JsonConvert.SerializeObject(userBase);
        File.WriteAllText(path, jsonData);
        //File.WriteAllText(path2, jsonData);
    }

    
}